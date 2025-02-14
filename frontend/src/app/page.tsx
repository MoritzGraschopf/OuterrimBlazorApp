import {Card, CardHeader, CardTitle, CardContent, CardDescription} from "@/components/ui/card";
import {Button} from "@/components/ui/button";
import Image from "next/image";
import hellyeah from "@/img/hellyeah.jpg"
import Link from "next/link";

export default function Home() {
    return (
        <>
            <header className="sticky flex items-center justify-end px-4 py-2 border-b">
                <Button asChild variant="link">
                    <Link
                        href="https://www.theguardian.com/lifeandstyle/2025/jan/22/skibidi-sigma-and-slay-the-most-popular-kids-slang-and-what-it-means"
                        target="_blank">
                        Skibidi
                    </Link>
                </Button>
            </header>
            <div className="m-4">
                <div className="h-[100%] grid grid-cols-2 place-items-center gap-4">
                    <Card className="h-[100%] w-[100%]">
                        <CardContent className="h-[100%] flex justify-center items-center">
                            <Image src={hellyeah} alt={"wow"} className="h-[50%] w-[100%]"/>
                        </CardContent>
                    </Card>
                    <Card className="h-[100%] w-[100%]">
                        <CardHeader>
                            <CardTitle>Photoshop (ApoRed)</CardTitle>
                            <CardDescription>Featuring Krappi & KsFreak</CardDescription>
                        </CardHeader>
                        <CardContent>
                            <p>Nagelneuer Benzer, Arme aus dem Fenster
                                Baller mit der MK 4-8
                                Der Typ, der aufm Spielplatz gedealt hat
                                Trägt neue Sneaker, schon wieder
                                Im Wert von einem Tierpark, Bitch</p><br/>
                            <p>Baller meine Lines, trage meine Shines
                                Und du weinst, und du weißt, dass du mein Level nicht erreichst
                                Und ich scheiß auf dein'n Style, ich scheiß auf dein Life
                                Macht euch einfach bereit, denn ich rappe zurzeit, nice (nice)</p><br/>
                            <p>ApoRed wieder jetzt am Mic
                                Frise fresh gestylt, Sneaker jetzt in weiß
                                Riding in the Benzer, noch besser, ein Tesla
                                Auf Insta folgt mir deine Schwester, fresh, Mann</p><br/>
                            <p>Auf einmal will jetzt jeder mein Hintermann sein
                                Und das alles nur wegen paar Instagram-Likes</p><br/>
                            <p>Ich werd Model und scheiß auf dein'n Homojob
                                Denn Ladies sagen, ich seh aus, so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop</p><br/>
                            <p>Breite Arme, so wie dieser Robocop
                                Und Ladies sagen, ich seh aus, so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop</p><br/>
                            <p>Photoshop</p><br/>
                            <p>Es ist Krappi und Ks
                                Mit dem Eight-Pack
                                Fresher than you, die fresheste Crew
                                Wir gehen jetzt ab wie ein Henna-Tattoo, ah</p><br/>
                            <p>Uns geht es zu gut auf YouTube
                                Zu smooth, too fresh
                                Everyday Saturday und gut Cash
                                Check mal das Mic, sind hier jetzt grad die Besten zurzeit
                                Einfach mächtig gehypt, hab'n den heftigsten Style
                                Tragen die trendigsten Nikes, unsre Freshness zerreißt, nice (nice, nice)</p><br/>
                            <p>Likegeil ist unser Lifestyle
                                Highlife, Highfive</p><br/>
                            <p>Ich werd Model und scheiß auf dein'n Homojob
                                Denn Ladies sagen, ich seh aus, so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop</p><br/>
                            <p>Breite Arme, so wie dieser Robocop
                                Und Ladies sagen, ich seh aus, so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop
                                So wie Photoshop, so wie so wie Photoshop</p><br/>
                        </CardContent>
                    </Card>
                </div>
            </div>
        </>
    );
}
