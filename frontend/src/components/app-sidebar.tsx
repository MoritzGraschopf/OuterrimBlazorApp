import * as React from "react"

import {
    Sidebar,
    SidebarContent, SidebarGroup, SidebarGroupContent,
    SidebarHeader,
    SidebarMenu,
    SidebarMenuButton,
    SidebarMenuItem,
    SidebarRail,
} from "@/components/ui/sidebar"
import Link from "next/link";
import {BookOpenIcon, HouseIcon, PackagePlusIcon, PackageX, Upload} from "lucide-react";
import {Avatar, AvatarFallback, AvatarImage} from "@/components/ui/avatar";

// This is sample data.
const data = {
    navMain: [
        {
            title: "Home",
            url: "/",
            icon: HouseIcon
        },
        {
            title: "Create",
            url: "/create",
            icon: PackagePlusIcon
        },
        {
            title: "Read",
            url: "/read",
            icon: BookOpenIcon
        },
        {
            title: "Update",
            url: "/update",
            icon: Upload
        },
    ]
}

export function AppSidebar({...props}: React.ComponentProps<typeof Sidebar>) {
    return (
        <Sidebar {...props}>
            <SidebarHeader>
                <SidebarMenu>
                    <SidebarMenuItem className="flex gap-2 items-center p-1">
                        <Avatar className="h-8 w-8 rounded-lg">
                            <AvatarImage src="https://i.ytimg.com/vi/qH07aMO-ENk/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLB3TU9ULoTv_vhQDZdgrkYmX1bFPw" alt="blazor" />
                            <AvatarFallback><strong>B</strong></AvatarFallback>
                        </Avatar>
                        <div className="grid flex-1 text-left text-sm leading-tight">
                            <span className="truncate font-semibold">Outer Rim Web App</span>
                        </div>
                    </SidebarMenuItem>
                </SidebarMenu>
            </SidebarHeader>
            <SidebarContent>
                <SidebarGroup>
                    <SidebarGroupContent>
                        <SidebarMenu>
                            {data.navMain.map((item) => (
                                <SidebarMenuItem key={item.title}>
                                    <SidebarMenuButton asChild>
                                        <Link href={item.url}>
                                            <item.icon/>
                                            <span>{item.title}</span>
                                        </Link>
                                    </SidebarMenuButton>
                                </SidebarMenuItem>
                            ))}
                        </SidebarMenu>
                    </SidebarGroupContent>
                </SidebarGroup>
            </SidebarContent>
            <SidebarRail/>
        </Sidebar>
    )
}
