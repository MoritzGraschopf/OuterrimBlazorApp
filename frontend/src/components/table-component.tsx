"use client"

import {
    Table,
    TableBody,
    TableCaption,
    TableCell,
    TableFooter,
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

type response  = {
    id: string,
    [key: string]: any
}

export default function TableComponent({ name, link } : { name: string, link: string }) {
    const [responseItem, setResponseItem] = useState<response[] | null>(null);

    useEffect(() => {
        fetch(link, {
            headers: {"Content-Type": "application/json"},
        }).then(r => r.json())
            .then(data => {
                setResponseItem(data)
            })

    }, []);

    return responseItem === null ? (<></>) : (
        <>
            <Table>
                <TableCaption>{name}</TableCaption>
                <TableHeader>
                    <TableRow>
                        {Object.entries(responseItem[0]).map(([key, value]) => {
                            return value === null ? null : (
                                <TableHead key={key}>{key[0].toUpperCase() + key.slice(1)}</TableHead>
                            )
                        })}
                        <TableHead className="text-right">d</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {responseItem.map((item) => {
                        return (
                            <TableRow key={item.id}>
                                {Object.entries(item).map(([key, value]) => {
                                    return value === null ? null : (
                                        <TableCell key={key}>{value}</TableCell>
                                    )
                                })}
                                <TableCell className="text-right ">
                                    <AlertDialog>
                                        <AlertDialogTrigger asChild>
                                            <Button variant="outline" className="text-red-500">
                                                <Trash2 />
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
                                                <AlertDialogAction>Continue</AlertDialogAction>
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