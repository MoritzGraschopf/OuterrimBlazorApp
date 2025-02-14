"use client"

import { zodResolver } from "@hookform/resolvers/zod"
import { useForm } from "react-hook-form"
import { z } from "zod"
import { Button } from "@/components/ui/button"
import {
    Form,
    FormControl,
    FormDescription,
    FormField,
    FormItem,
    FormLabel,
    FormMessage,
} from "@/components/ui/form"
import { Input } from "@/components/ui/input"

// public class AircraftDto
// {
//     public int Fuel { get; set; }
//     public int Speed { get; set; }
//     public int Altitude { get; set; }
//     [StringLength(200)] public string Name { get; set; } = "";
//     public int SpecificationId { get; set; }
// }

const formSchema = z.object({
    fuel: z.number(),
    speed: z.number(),
    altitude: z.number(),
    name: z.string().max(200),
    specificationId: z.number()
})

export function FormAircraft() {
    // 1. Define your form.
    const form = useForm<z.infer<typeof formSchema>>({
        resolver: zodResolver(formSchema),
        defaultValues: {
            fuel: 0,
            speed: 0,
            altitude: 0,
            name: "",
            specificationId: 0
        },
    })

    // 2. Define a submit handler.
    function onSubmit(values: z.infer<typeof formSchema>) {
        // Do something with the form values.
        // ✅ This will be type-safe and validated.
        console.log(values)
    }

    return (
        <Form {...form}>
            <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-4">
                <FormField
                    control={form.control}
                    name="fuel"
                    render={({ field }) => (
                        <FormItem>
                            <FormLabel>Fuel</FormLabel>
                            <FormControl>
                                <Input placeholder="Fuel" {...field} />
                            </FormControl>
                            <FormMessage />
                        </FormItem>
                    )}
                />
                <Button type="submit">Submit</Button>
            </form>
        </Form>
    )
}