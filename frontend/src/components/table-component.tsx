"use client"

import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableHead,
    TableHeader,
    TableRow,
} from "@/components/ui/table"
import {Button} from "@/components/ui/button";
import {
    AlertDialog,
    AlertDialogAction,
    AlertDialogCancel,
    AlertDialogContent,
    AlertDialogDescription,
    AlertDialogFooter,
    AlertDialogHeader,
    AlertDialogTitle,
    AlertDialogTrigger,
} from "@/components/ui/alert-dialog"
import {useEffect, useState} from "react";
import {Trash2} from "lucide-react";
import {FormCreate} from "@/components/create/form-create";
import {ZodObject} from "zod";
import {FormCreateAircraftCrew} from "@/components/create/form-create-aircraftcrew";
import FormUpdate from "@/components/update/form-update";
import FormUpdateAircraftcrew from "@/components/update/form-update-aircraftcrew";

type response = {
    id: number,
    [key: string]: any
}

const regex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$/

export default function TableComponent({name, link, formSchema}: {
    name: string,
    link: string,
    formSchema: ZodObject<any>
}) {
    const [responseItem, setResponseItem] = useState<response[] | null>(null);

    useEffect(() => {
        fetchData();
    }, [link]);

    const checkRegex = (value: any) => {
        if (regex.test(value)) value = value.split("T")[0]
        return value
    }

    const fetchData = () => {
        fetch(link, {
            headers: { "Content-Type": "application/json" },
        })
            .then((r) => {
                if (!r.ok) {
                    throw new Error(`Error: ${r.status} ${r.statusText}`);
                }
                return r.json();
            })
            .then((data) => {
                if (!data || (Array.isArray(data) && data.length === 0)) {
                    console.warn("No data found.");
                    setResponseItem(null); // Oder setResponseItem([]), falls es eine Liste sein sollte
                } else {
                    setResponseItem(data);
                }
                console.log("name",formSchema.shape)
            })
            .catch((err) => {
                console.error("Fetch error:", err);
                setResponseItem(null); // Setzt die Daten zurück, um Fehler in der UI zu vermeiden
            });
    };

    const deleteResponseItem = (id: number) => {
        fetch(link + "/" + id, {
            method: "DELETE",
            headers: {"Content-Type": "application/json"},
        }).then(() => fetchData())
            .catch(err => console.log(err));
    }
    const deleteCrewResponseItem = (id1: number, id2: number) => {
        // https://localhost:7236/api/aircraftcrew?aircraftId=4&mercenaryId=4
        fetch(link + `?aircraftId=${id1}&mercenaryId=${id2}`, {
            method: "DELETE",
            headers: {"Content-Type": "application/json"},
        }).then(() => fetchData())
            .catch(err => console.log(err));
    }
    const deleteReputationResponseItem = (id1: number, id2: number) => {
        // https://localhost:7236/api/mercenaryreputations?syndicateId=4&mercenaryId=4
        fetch(link + `?syndicateId=${id1}&mercenaryId=${id2}`, {
            method: "DELETE",
            headers: {"Content-Type": "application/json"},
        }).then(() => fetchData())
            .catch(err => console.log(err));
    }

    return (
        <Table>
            <TableCaption>{name}</TableCaption>
            <TableHeader>
                <TableRow>
                    {Object.entries(formSchema.shape).map(([key]) => (
                        <TableHead key={key}>{key[0].toUpperCase() + key.slice(1)}</TableHead>
                    ))}
                    <TableHead className="text-right"></TableHead>
                </TableRow>
            </TableHeader>
            <TableBody>
                {/* Falls responseItem leer ist, zeigen wir eine leere Zeile */}
                {responseItem && responseItem.length > 0 ? (
                    responseItem.map((item) => (
                        <TableRow key={JSON.stringify(item)}>
                            {Object.entries(item).map(([key, value]) => (
                                value === null ? null : (
                                    <TableCell key={`${item.id}-${key}`}>{checkRegex(value)}</TableCell>
                                )
                            ))}
                            <TableCell className="flex w-full justify-end items-center gap-2">
                                {name === "Aircraft Crew" ? (
                                    <FormUpdateAircraftcrew fetchDataAction={fetchData} formSchema={formSchema} link={link} entityName={name} currentEntity={item}/>
                                ) : (
                                    <FormUpdate fetchDataAction={fetchData} formSchema={formSchema} link={link + `/${item.id}`} entityName={name} currentEntity={item}/>
                                )}
                                <AlertDialog>
                                    <AlertDialogTrigger asChild>
                                        <Button variant="outline" className="text-red-500" size="icon">
                                            <Trash2/>
                                        </Button>
                                    </AlertDialogTrigger>
                                    <AlertDialogContent>
                                        <AlertDialogHeader>
                                            <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
                                            <AlertDialogDescription>
                                                This action cannot be undone. This will permanently delete selected
                                                Entity and remove it from our servers.
                                            </AlertDialogDescription>
                                        </AlertDialogHeader>
                                        <AlertDialogFooter>
                                            <AlertDialogCancel>Cancel</AlertDialogCancel>
                                            <AlertDialogAction asChild>
                                                <Button onClick={() => {
                                                    if (name === "Aircraft Crew") deleteCrewResponseItem(item.aircraftId, item.mercenaryId);
                                                    else if (name === "Mercenary Reputation") deleteReputationResponseItem(item.syndicateId, item.mercenaryId)
                                                    else deleteResponseItem(item.id)
                                                }} variant="destructive" className="bg-destructive text-destructive-foreground shadow-sm hover:bg-destructive/90">
                                                    Continue
                                                </Button>
                                            </AlertDialogAction>
                                        </AlertDialogFooter>
                                    </AlertDialogContent>
                                </AlertDialog>
                            </TableCell>
                        </TableRow>
                    ))
                ) : (
                    <TableRow>
                        <TableCell colSpan={Object.keys(formSchema.shape).length + 1} className="text-center text-gray-500">
                            No data available
                        </TableCell>
                    </TableRow>
                )}
                {/* Erstellen-Button immer anzeigen */}
                <TableRow>
                    <TableCell colSpan={Object.keys(formSchema.shape).length + 1} className="text-center">
                        {name === "Aircraft Crew" ? (
                            <FormCreateAircraftCrew fetchDataAction={fetchData} formSchema={formSchema} link={link} entityName={name}/>
                        ) : (
                            <FormCreate fetchDataAction={fetchData} formSchema={formSchema} link={link} entityName={name}/>
                        )}
                    </TableCell>
                </TableRow>
            </TableBody>
        </Table>
    );

}