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

const selectItems = [
    {
        name: "Aircraft",
        value: "aircrafts",
    },
    {
        name: "Aircraft Crew",
        value: "aircraftcrew",
    },
    {
        name: "Aircraft Specification",
        value: "aircraftspecifications",
    },
    {
        name: "Compartment",
        value: "compartments",
    },
    {
        name: "Crime Syndicate",
        value: "crimesyndicates",
    },
    {
        name: "Machinery",
        value: "machineries",
    },
    {
        name: "Mercenary",
        value: "mercenaries",
    },
    {
        name: "Mercenary Reputation",
        value: "mercenaryreputations",
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
                <TableComponent name={selectItems.find(si => si.value === currentValue)?.name!} link={`https://localhost:7236/api/${currentValue}`}/>
            </div>
        </>
    )
}