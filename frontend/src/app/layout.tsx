import type {Metadata} from "next";
import {Geist, Geist_Mono} from "next/font/google";
import "./globals.css";
import {AppSidebar} from "@/components/app-sidebar";
import {SidebarInset, SidebarProvider} from "@/components/ui/sidebar";
import { Toaster } from "@/components/ui/toaster"



const geistSans = Geist({
    variable: "--font-geist-sans",
    subsets: ["latin"],
});

const geistMono = Geist_Mono({
    variable: "--font-geist-mono",
    subsets: ["latin"],
});

export const metadata: Metadata = {
    title: "Outer Rim Web App",
    description: "Bobens Bobens Bobens Bobens Bobens Bobens Bobens Bobens Bobens Bobens Bobens",
};

export default function RootLayout({
                                       children,
                                   }: Readonly<{
    children: React.ReactNode;
}>) {
    return (
        <html lang="en">
        <body
            className={`${geistSans.variable} ${geistMono.variable} antialiased`}
        >
        <SidebarProvider>
            <AppSidebar/>
            <SidebarInset>
                {children}
            </SidebarInset>
            <Toaster />
        </SidebarProvider>
        </body>
        </html>
    );
}
