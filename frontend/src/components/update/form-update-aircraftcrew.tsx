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
import {Calendar as CalendarIcon, PencilLine} from "lucide-react"
import React, { useEffect } from "react"
import { useToast } from "@/hooks/use-toast"
import {Popover, PopoverContent, PopoverTrigger} from "@/components/ui/popover";
import {cn} from "@/lib/utils";
import {addDays, format} from "date-fns";
import {Calendar} from "@/components/ui/calendar";

export default function FormUpdateAircraftcrew({
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
    const [date, setDate] = React.useState<Date | undefined>(currentEntity.joinedAt)
    const [changedDate, setChangedDate] = React.useState(false)

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

        values = {
            aircraftId: values.aircraftId,
            mercenaryId: values.mercenaryId,
            joinedAt: changedDate ? addDays(date!, 1) : date,
        }
        console.log(values)
        console.log(changedDate)

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
                                <FormField
                                    control={form.control}
                                    name="JoinedAt" // Typensicherheit durch Zod-Schema
                                    render={() => (
                                        <FormItem>
                                            <FormLabel>JoinedAt</FormLabel>
                                            <FormControl>
                                                <Popover>
                                                    <PopoverTrigger asChild>
                                                        <Button
                                                            variant={"outline"}
                                                            className={cn(
                                                                "w-full justify-start text-left font-normal",
                                                                !date && "text-muted-foreground"
                                                            )}
                                                        >
                                                            <CalendarIcon className="mr-2 h-4 w-4" />
                                                            {date ? format(date, "PPP") : <span>Pick a date</span>}
                                                        </Button>
                                                    </PopoverTrigger>
                                                    <PopoverContent className="w-auto p-0">
                                                        <Calendar
                                                            mode="single"
                                                            selected={date}
                                                            onSelect={(bobens) => {
                                                                setChangedDate(true)
                                                                setDate(bobens)
                                                            }}
                                                            initialFocus
                                                        />
                                                    </PopoverContent>
                                                </Popover>
                                            </FormControl>
                                            <FormMessage/>
                                        </FormItem>
                                    )}
                                />
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
