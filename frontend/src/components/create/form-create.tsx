"use client"

import {zodResolver} from "@hookform/resolvers/zod"
import {useForm} from "react-hook-form"
import {z, ZodObject} from "zod"
import {Button} from "@/components/ui/button"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import {Input} from "@/components/ui/input"
import {
    Drawer,
    DrawerClose,
    DrawerContent, DrawerDescription,
    DrawerFooter,
    DrawerHeader,
    DrawerTitle,
    DrawerTrigger
} from "@/components/ui/drawer";
import {
    Select, SelectContent, SelectItem, SelectTrigger, SelectValue
} from "@/components/ui/select";
import {CirclePlus} from "lucide-react";
import React, {useEffect} from "react";
import {useToast} from "@/hooks/use-toast";

// public class AircraftDto
// {
//     public int Fuel { get; set; }
//     public int Speed { get; set; }
//     public int Altitude { get; set; }
//     [StringLength(200)] public string Name { get; set; } = "";
//     public int SpecificationId { get; set; }
// }

export function FormCreate({fetchDataAction, formSchema, link, entityName}: {
    fetchDataAction: () => void,
    formSchema: ZodObject<any>,
    link: string,
    entityName: string,
}) {
    const {toast} = useToast()
    const [open, setOpen] = React.useState(false)

    // 1. Define your form.
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: Object.fromEntries(
            Object.entries(formSchema.shape).map(([key, value]) => {
                if (value instanceof z.ZodNumber) return [key, 0] // Default für Zahlen
                if (value instanceof z.ZodString) return [key, ""] // Default für Strings
                return [key, null] // Fallback für andere Typen
            })
        )
    })

    useEffect(() => {
        form.reset(
            Object.fromEntries(
                Object.entries(formSchema.shape).map(([key, value]) => {
                    if (value instanceof z.ZodNumber) return [key, 0] // Default für Zahlen
                    if (value instanceof z.ZodString) return [key, ""] // Default für Strings
                    return [key, null] // Fallback für andere Typen
                })
            )
        )
    }, [formSchema, form]);

    // 2. Define a submit handler.
    const idFields: { [key: string]: string[] } = {
        aircrafts: ['specificationId'],  // Hier ist das richtige ID-Feld "specificationId" für Aircraft
        aircraftcrew: ['aircraftId', 'mercenaryId'],
        aircraftspecifications: ['id'],
        compartments: ['aircraftId'],
        crimesyndicates: ['id'],
        machineries: ['compartmentId'],
        mercenaries: ['id'],
        mercenaryreputations: ['syndicateId', 'mercenaryId'],
    };

    function onSubmit(values: z.infer<typeof formSchema>) {
        try {
            // Bestimme die richtigen ID-Felder basierend auf dem aktuellen `link`
            const currentEntity = link.split('/').pop(); // Extrahiere den Entitätsnamen aus dem Link
            const fields = idFields[currentEntity || ''] || []; // Hole die zugehörigen ID-Felder

            if (fields.length === 0) {
                console.log('Keine ID-Felder für diese Entität definiert!');
            }

            // Extrahiere die IDs aus dem `values`-Objekt
            const idValues = fields.map(field => {
                if (values[field] === undefined || values[field] === null) {
                    console.log(`Feld "${field}" fehlt im values-Objekt!`);
                }
                return values[field];
            }).filter(id => id !== undefined && id !== null);

            const idDisplay = idValues.length ? idValues.join(' & ') : 'unknown';

            if (idValues.length === 0) {
                toast({
                    title: "An error occurred.",
                    description: `No related entity found with IDs: ${idDisplay}`,
                    variant: "destructive"
                });
            }

            fetch(link, {
                method: "POST",
                body: JSON.stringify(values),
                headers: {"Content-Type": "application/json"},
            })
                .then(res => {
                    if (res.status === 500) {
                        throw new Error(`No related entity found with IDs: ${idDisplay}`);
                    }
                    return res.json();
                })
                .then(data => {
                    console.log(data);
                    fetchDataAction();
                    setOpen(false);
                })
                .catch(err => {
                    toast({
                        title: "An error occurred.",
                        description: err.message,
                        variant: "destructive"
                    });
                });
        } catch (error) {
            toast({
                title: "An error occurred.",
                description: (error as Error).message,
                variant: "destructive"
            });
        }
    }


    return (
        <Drawer open={open} onOpenChange={setOpen}>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
                    <DrawerTrigger asChild>
                        <Button variant="outline" size="icon">
                            <CirclePlus/>
                        </Button>
                    </DrawerTrigger>
                    <DrawerContent>
                        <div className="mx-auto w-full max-w-sm mt-2">
                            <DrawerHeader>
                                <DrawerTitle>Create new {entityName}</DrawerTitle>
                                <DrawerDescription className="sr-only">Bobens</DrawerDescription>
                            </DrawerHeader>
                            <div className="px-4 flex flex-col gap-2">
                                {Object.entries(formSchema.shape).map(([key, value]) => {
                                    if (key === "id") return null
                                    return (
                                        <FormField
                                            key={key}
                                            control={form.control}
                                            name={key as any} // Typensicherheit durch Zod-Schema
                                            render={({field}) => (
                                                <FormItem>
                                                    <FormLabel>{key.charAt(0).toUpperCase() + key.slice(1)}</FormLabel>
                                                    <FormControl>
                                                        {key === "machineryType" ? ( // Falls es "type" ist, Dropdown verwenden
                                                            <Select onValueChange={field.onChange}
                                                                    defaultValue={field.value ?? ""}>
                                                                <SelectTrigger>
                                                                    <SelectValue placeholder="Select a type"/>
                                                                </SelectTrigger>
                                                                <SelectContent>
                                                                    <SelectItem value="EnergySystem">Energy
                                                                        System</SelectItem>
                                                                    <SelectItem value="EnviromentalSystem">Environmental
                                                                        System</SelectItem>
                                                                    <SelectItem value="Weapon">Weapon</SelectItem>
                                                                </SelectContent>
                                                            </Select>
                                                        ) : (
                                                            <Input
                                                                type={value instanceof z.ZodNumber ? "number" : "text"}
                                                                value={field.value ?? ""} // 🔥 Null in "" umwandeln
                                                                onChange={field.onChange}
                                                            />
                                                        )}
                                                    </FormControl>
                                                    <FormMessage/>
                                                </FormItem>
                                            )}
                                        />
                                    )
                                })}
                            </div>
                            <DrawerFooter>
                                <Button type="submit" onClick={() => form.handleSubmit(onSubmit)()}>Submit</Button>
                                <DrawerClose asChild>
                                    <Button variant="secondary">Cancel</Button>
                                </DrawerClose>
                            </DrawerFooter>
                        </div>
                    </DrawerContent>
                </form>
            </Form>
        </Drawer>

    )
}