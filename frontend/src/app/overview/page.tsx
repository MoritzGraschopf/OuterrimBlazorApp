"use client"

import {Button} from "@/components/ui/button";
import Link from "next/link";
import {
    Select,
    SelectContent, SelectGroup,
    SelectItem, SelectLabel,
    SelectTrigger,
    SelectValue,
} from "@/components/ui/select"
import {useState} from "react";
import TableComponent from "@/components/table-component";
import {z} from "zod";

const selectItems = [
    {
        name: "Aircraft",
        value: "aircrafts",
        schema: z.object({
            fuel: z.coerce.number(),  // Wandelt Input-String automatisch in eine Zahl um
            speed: z.coerce.number(),
            altitude: z.coerce.number(),
            name: z.string().max(200),
            specificationId: z.coerce.number()
        })
    },
    {
        name: "Aircraft Crew",
        value: "aircraftcrew",
        schema: z.object({
            aircraftId: z.coerce.number(),  // Wandelt Input-String automatisch in eine Zahl um
            mercenaryId: z.coerce.number(),
        })
    },
    {
        name: "Aircraft Specification",
        value: "aircraftspecifications",
        schema: z.object({
            structure: z.coerce.number(),  // Wandelt Input-String automatisch in eine Zahl um
            fuelTankCapacity: z.coerce.number(),
            minSpeed: z.coerce.number(),
            maxSpeed: z.coerce.number(),
            maxAltitude: z.coerce.number(),
            specificationCode: z.string().max(45)
        })
    },
    {
        name: "Compartment",
        value: "compartments",
        schema: z.object({
            aircraftId: z.coerce.number(),  // Wandelt Input-String automatisch in eine Zahl um
        })
    },
    {
        name: "Crime Syndicate",
        value: "crimesyndicates",
        schema: z.object({
            name: z.string().max(120),  // Wandelt Input-String automatisch in eine Zahl um
            location: z.string().max(200),
        })
    },
    {
        name: "Machinery",
        value: "machineries",
        schema: z.object({
            label: z.string().max(45),  // Wandelt Input-String automatisch in eine Zahl um
            function: z.string(),
            compartmentId: z.coerce.number(),
            machineryType: z.enum(["EnergySystem", "EnviromentalSystem", "Weapon"]),
        })
    },
    {
        name: "Mercenary",
        value: "mercenaries",
        schema: z.object({
            firstName: z.string(),  // Wandelt Input-String automatisch in eine Zahl um
            lastName: z.string(),
            bodySkills: z.coerce.number(),
            combatSkills: z.coerce.number(),
        })
    },
    {
        name: "Mercenary Reputation",
        value: "mercenaryreputations",
        schema: z.object({
            syndicateId: z.coerce.number(),  // Wandelt Input-String automatisch in eine Zahl um
            mercenaryId: z.coerce.number(),
            reputationChange: z.string().max(45),
        })
    },
]

export default function Page() {
    const [currentValue, setCurrentValue] = useState(selectItems[0].value);

    return (
        <>
            <header className="sticky flex items-center justify-between px-4 py-2 border-b">
                <Select value={currentValue} onValueChange={setCurrentValue}>
                    <SelectTrigger className="w-[250px]">
                        <span className="text-gray-400">Entity:</span>
                        <SelectValue className="font-semibold"/>
                    </SelectTrigger>
                    <SelectContent>
                        <SelectGroup>
                            <SelectLabel>Entities</SelectLabel>
                            {selectItems.map((item) => (
                                <SelectItem key={item.value} value={item.value}>
                                    {item.name}
                                </SelectItem>
                            ))}
                        </SelectGroup>
                    </SelectContent>
                </Select>
                <Button asChild variant="link">
                    <Link
                        href="https://www.theguardian.com/lifeandstyle/2025/jan/22/skibidi-sigma-and-slay-the-most-popular-kids-slang-and-what-it-means"
                        target="_blank">
                        Skibidi
                    </Link>
                </Button>
            </header>
            <div className="m-4">
                <TableComponent name={selectItems.find(si => si.value === currentValue)?.name!} link={`https://localhost:7236/api/${currentValue}`} formSchema={selectItems.find(si => si.value === currentValue)?.schema!}/>
            </div>
        </>
    )
}