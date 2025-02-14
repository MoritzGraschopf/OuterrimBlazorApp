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
import {CirclePlus} from "lucide-react";
import React, {useEffect} from "react";
import {useToast} from "@/hooks/use-toast";

import { Calendar } from "@/components/ui/calendar"
import {
    Popover,
    PopoverContent,
    PopoverTrigger,
} from "@/components/ui/popover"
import { cn } from "@/lib/utils"
import { format } from "date-fns"
import { Calendar as CalendarIcon } from "lucide-react"


export function FormCreateAircraftCrew({fetchDataAction, formSchema, link, entityName}: {
    fetchDataAction: () => void,
    formSchema: ZodObject<any>,
    link: string,
    entityName: string,
}) {
    const {toast} = useToast()
    const [open, setOpen] = React.useState(false)
    const [date, setDate] = React.useState<Date>()

    // 1. Define your form.
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            aircraftId: 0,
            mercenaryId: 0,
            joinedAt: date,
        }
    })

    // 2. Define a submit handler.
    function onSubmit(values: z.infer<typeof formSchema>) {
        const idField = Object.keys(values).find(key => key.toLowerCase().includes("id")) || "unknown ID";

        values = {
            aircraftId: values.aircraftId,
            mercenaryId: values.mercenaryId,
            joinedAt: date,
        }

        fetch(link, {
            method: "POST",
            body: JSON.stringify(values),
            headers: {"Content-Type": "application/json"},
        })
            .then(res => {
                if (res.status === 500) throw new Error(`No related entity found with ID: ${values[idField] || "unknown"}`);
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
                                <FormField
                                    control={form.control}
                                    name="aircraftId" // Typensicherheit durch Zod-Schema
                                    render={({field}) => (
                                        <FormItem>
                                            <FormLabel>AircraftId</FormLabel>
                                            <FormControl>
                                                <Input
                                                    type="number"
                                                    // 🔥 Null in "" umwandeln
                                                    onChange={field.onChange}
                                                />
                                            </FormControl>
                                            <FormMessage/>
                                        </FormItem>
                                    )}
                                />
                                <FormField
                                    control={form.control}
                                    name="mercenaryId" // Typensicherheit durch Zod-Schema
                                    render={({field}) => (
                                        <FormItem>
                                            <FormLabel>MercenaryId</FormLabel>
                                            <FormControl>
                                                <Input
                                                    type="number"
                                                    // 🔥 Null in "" umwandeln
                                                    onChange={field.onChange}
                                                />
                                            </FormControl>
                                            <FormMessage/>
                                        </FormItem>
                                    )}
                                />
                                <FormField
                                    control={form.control}
                                    name="JoinedAt" // Typensicherheit durch Zod-Schema
                                    render={({field}) => (
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
                                                            onSelect={setDate}
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