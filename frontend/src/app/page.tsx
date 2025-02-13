"use client"

import {useEffect, useState} from "react";

type response = {
    id: number,
    fuel: number,
    speed: number,
    altitude: number,
    name: string,
    specificationId: number,
}

export default function Home() {
    const [responseItem, setResponseItem] = useState<response[] | null>(null);

    return (
        <div>

        </div>
    );
}
