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

type response = {
    id: number,
    [key: string]: any
}

const regex = /^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}$/

export default function TableComponent({name, link}: { name: string, link: string }) {
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
            headers: {"Content-Type": "application/json"},
        })
            .then((r) => r.json())
            .then((data) => {
                setResponseItem(data);
            })
            .catch((err) => console.log(err));
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

    return responseItem === null ? (<></>) : (
        <>
            <Table>
                <TableCaption>{name}</TableCaption>
                <TableHeader>
                    <TableRow>
                        {Object.entries(responseItem[0]).map(([key, value]) => {
                            return value === null ? null : (
                                <TableHead key={Math.random()}>{key[0].toUpperCase() + key.slice(1)}</TableHead>
                            )
                        })}
                        <TableHead className="text-right"></TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {responseItem.map((item) => {
                        return (
                            <TableRow key={Math.random()}>
                                {Object.entries(item).map(([_, value]) => {
                                    return value === null ? null : (
                                        <TableCell key={Math.random()}>{checkRegex(value)}</TableCell>
                                    )
                                })}
                                <TableCell className="text-right ">
                                    <AlertDialog>
                                        <AlertDialogTrigger asChild>
                                            <Button variant="outline" className="text-red-500">
                                                <Trash2/>
                                            </Button>
                                        </AlertDialogTrigger>
                                        <AlertDialogContent>
                                            <AlertDialogHeader>
                                                <AlertDialogTitle>Are you absolutely sure?</AlertDialogTitle>
                                                <AlertDialogDescription>
                                                    This action cannot be undone. This will permanently delete your
                                                    account and remove your data from our servers.
                                                </AlertDialogDescription>
                                            </AlertDialogHeader>
                                            <AlertDialogFooter>
                                                <AlertDialogCancel>Cancel</AlertDialogCancel>
                                                <AlertDialogAction
                                                    onClick={() => {
                                                        if (name === "Aircraft Crew") deleteCrewResponseItem(item.aircraftId, item.mercenaryId);
                                                        else if (name === "Mercenary Reputation") deleteReputationResponseItem(item.syndicateId, item.mercenaryId)
                                                        else deleteResponseItem(item.id)
                                                    }}>Continue</AlertDialogAction>
                                            </AlertDialogFooter>
                                        </AlertDialogContent>
                                    </AlertDialog>
                                </TableCell>
                            </TableRow>
                        )
                    })}
                </TableBody>
            </Table>
        </>
    )
}