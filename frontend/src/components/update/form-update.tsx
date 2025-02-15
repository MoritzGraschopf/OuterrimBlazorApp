
"use client"

import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { z, ZodObject } from "zod"
import { Button } from "@/components/ui/button"
import {
    Form,
    FormControl,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { Input } from "@/components/ui/input"
import {
    Drawer,
    DrawerClose,
    DrawerContent,
    DrawerDescription,
    DrawerFooter,
    DrawerHeader,
    DrawerTitle,
    DrawerTrigger,
} from "@/components/ui/drawer"
import { PencilLine } from "lucide-react"
import React, { useEffect } from "react"
import { useToast } from "@/hooks/use-toast"

export default function FormUpdate({
                                                   fetchDataAction,
                                                   formSchema,
                                                   link,
                                                   entityName,
                                                   currentEntity,
                                               }: {
    fetchDataAction: () => void
    formSchema: ZodObject<any>
    link: string
    entityName: string
    currentEntity: any
}) {
    const { toast } = useToast()
    const [open, setOpen] = React.useState(false)

    // 1. Define your form with defaultValues based on currentEntity
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: Object.fromEntries(
            Object.entries(formSchema.shape).map(([key]) => {
                return [key, currentEntity[key]]
            })
        ),
    })

    useEffect(() => {
        form.reset(
            Object.fromEntries(
                Object.entries(formSchema.shape).map(([key]) => {
                    return [key, currentEntity[key]]
                })
            )
        )
    }, [formSchema, currentEntity, form])

    // 2. Define the idFields mapping
    const idFields: { [key: string]: string[] } = {
        aircrafts: ["specificationId"], // Für Aircrafts nehmen wir "specificationId" an
        aircraftcrew: ["aircraftId", "mercenaryId"],
        aircraftspecifications: ["id"],
        compartments: ["aircraftId"],
        crimesyndicates: ["id"],
        machineries: ["id"],
        mercenaries: ["id"],
        mercenaryreputations: ["syndicateId", "mercenaryId"],
    }

    // 3. Define a submit handler that uses idFields in the error message
    function onSubmit(values: z.infer<typeof formSchema>) {
        // Bestimme anhand des Links den Entitätsnamen (z. B. "aircrafts", "aircraftcrew", etc.)
        const currentEntityKey = link.split("/").pop() || ""
        const fields = idFields[currentEntityKey] || []
        const idValues = fields
            .map((field) => values[field])
            .filter((val) => val !== undefined && val !== null)
        const idDisplay = idValues.length ? idValues.join(" & ") : "unknown"

        fetch(link, {
            method: "PUT",
            body: JSON.stringify(values),
            headers: { "Content-Type": "application/json" },
        })
            .then((res) => {
                if (res.status === 500) {
                    throw new Error(`No related entity found with IDs: ${idDisplay}`)
                }
                return res.json()
            })
            .then((data) => {
                console.log(data)
                fetchDataAction()
                setOpen(false)
            })
            .catch((err) => {
                toast({
                    title: "An error occurred.",
                    description: err.message,
                    variant: "destructive",
                })
            })
    }

    return (
        <Drawer open={open} onOpenChange={setOpen}>
            <Form {...form}>
                <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
                    <DrawerTrigger asChild>
                        <Button variant="outline" size="icon">
                            <PencilLine />
                        </Button>
                    </DrawerTrigger>
                    <DrawerContent>
                        <div className="mx-auto w-full max-w-sm mt-2">
                            <DrawerHeader>
                                <DrawerTitle>Update {entityName}</DrawerTitle>
                                <DrawerDescription className="sr-only">Update details</DrawerDescription>
                            </DrawerHeader>
                            <div className="px-4 flex flex-col gap-2">
                                {Object.entries(formSchema.shape).map(([key, value]) => {
                                    return key === "machineryType" ? null : (
                                        <FormField
                                            key={key}
                                            control={form.control}
                                            name={key as any} // Typensicherheit durch Zod-Schema
                                            render={({ field }) => (
                                                <FormControl>
                                                    <FormItem>
                                                        <FormLabel>{key.charAt(0).toUpperCase() + key.slice(1)}</FormLabel>
                                                        <Input
                                                            type={value instanceof z.ZodNumber ? "number" : "text"}
                                                            value={field.value ?? ""} // 🔥 Null in "" umwandeln
                                                            onChange={field.onChange}
                                                        />
                                                        <FormMessage/>
                                                    </FormItem>
                                                </FormControl>
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

