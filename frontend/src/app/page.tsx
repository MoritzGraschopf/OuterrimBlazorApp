import { Card, CardHeader, CardTitle, CardContent } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import Image from "next/image";

export default function Home() {
    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100 text-gray-900 p-6">
            <div className="flex flex-col md:flex-row gap-6 max-w-4xl w-full">
                <Card className="flex-1 p-6 shadow-md border border-gray-200">
                    <CardHeader>
                        <CardTitle className="text-3xl font-bold text-blue-600">
                            Standart Skill - Skybase Song
                        </CardTitle>
                    </CardHeader>
                    <CardContent>
                        <p className="text-lg text-gray-700 whitespace-pre-line">
                            Ich öffne die Augen und beginne den Tag
                            Natürlich mit einer Runde Battle Royale
                            Battle Royale mache ich zu meiner Hauptaufgabe
                            Meine Mutter nervt mit Hausaufgaben
                            Sie sagt, ich wäre süchtig - Sie hat Recht
                            Um vom Thema abzulenken, mache ich mich drüber witzig
                            Check, Check, komme mit 'nem Jetpack
                            Er hat keine Chance, höre jeden seiner Steps durch mein Headset
                            Ein Shot in sein' Kopf nach der ersten Begegnung
                            Fortnite ist eine Bewegung...
                        </p>
                        <Button className="mt-6 w-full bg-blue-500 hover:bg-blue-600 text-white font-semibold">
                            Mehr erfahren
                        </Button>
                    </CardContent>
                </Card>
                <div className="flex-1 flex items-center justify-center">
                    <Image
                        src="https://water.koeln/wp-content/uploads/2019/09/standartskill_new_portfolio_portrait_v6-480x480.jpg"
                        alt="Standart Skill"
                        className="rounded-lg shadow-md max-w-full h-auto"
                    />
                </div>
            </div>
        </div>
    );
}
