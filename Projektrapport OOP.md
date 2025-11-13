# Projektrapport OOP.

# 
Gruppuppgiften gick ut på att skapa ett system för sjukvården utifrån vissa krav och riktlinjer. 
Målet var aldrig att slutföra uppgiften utan det viktigaste var att man arbetade kontinurerligt med uppgiften,
arbeta väl med sin grupp samt att man dokumentara och förstod vad man gjorde. 
I min grupp var vi ärliga med oss själv.. Vi är inte de bästa programmererarna men det vi hade planerat att 
utföra ville vi utföra väl. Eller snarare att vi fokuserade på att det skulle fungera. 

#
Mitt ansvar i gruppen blev lite av en ofrivillig ledare. Jag fick ta på mig en del ansvar att organisera och dirigera.
Jag skrev aktivt i gruppen för att boka upp möten, styra upp arbetet och arrangera samtal på teams. 
I ett samtal som vi hade med Max under vår distansundervisning så fick jag också en indirekt rekommendation av honom
att försöka ta på mig den rollen. 
Det var aldrig min intention eller något jag ville göra. Jag förstår vikten av en  inte
ledare men jag tror samtidigt väldigt starkt på att en grupp fungerar bäst när de leder varandra.

Jag måste vara ärlig med att säga att jag tycker att dynamiken vi hade i gruppen var extremt utmanande och på en
nivå som jag aldrig har upplevt trots att jag har en 3-årig universitets utbilding där jag har jobbat med ett flertal
grupper och personligheter. Jag har kommit nära två av mina gruppmedlemmar så det handlar inte om att jag ogillar 
någon utan det är mer att jag upplevde att arbetet inte togs seriöst. 
Det kunde vara att man inte dök upp på möten, inte hörde av sig under hela dagen, prioriterade andra orelevanta saker
före studierna. Jag förstår att alla har ett liv utanför skolan men det var inte den typen av saker det handlade om.

Ena gruppmedlemmen fungerade inte alls med oss i början men mot slutet vände det helt och hållet vilket jag är väldigt glad för.
En annan extremt utmanande aspekt är att det är svårt att förstå två av mina gruppmedlemmar, vi förstår inte alltid
varandra och VÄLDIGT mycket tid och energi går att förklara och åter förklara simpla saker.
Det är inte någons fel utan bara en tråkig verklighet.
Jag ville ge upp projektet flera gånger och jag skrev båda till dig och Max om hur
missnöjd jag var med gruppen. Jag kan inte vara kvar i den här gruppen tills nästa gång om inte någon mer kommer in.
Det blir helt enkelt för mycket. Jag pratade med en annan gruppmedlemm som upplevera samma sak som jag gör, jag 
upplever ocskå att jag hamnar efter i mitt lärande då jag inte riktigt har någon att bolla med fram och tillbaka.

#
Under projektets gång känner jag åtminstone att jag har blivit mycket mer bekväm med github och jag lärde mig en hel
del om hur hela den biten fungerar vilket var väldigt givande. Jag upplevde att det var väldigt utmanande att försöka
håla koll på alla krav och specifikationer för att bygga upp systemet. Eller det var svårt att fokusera på den delen
du arbetade på utan att tänka på de andra delarna. 

Jag insåg under projektets gång men öven under våra presentationer
att jag har en hel del kvar att lära mig och att jag måste lägga ner ännu mer tid och energi på att försöka lära mig.
Om jag ska reflektera över mina kamrater ser mig så kanske de upplevde mig som väldigt krävande och på. 
Jag tror att det är viktigt att reflektera över hur människor uppfattar dig också, allt handlar inte om dig eller 
hur du ser saker och ting. Jag kanske är den som har gjort "fel" i gruppen eller jag kanske var den som gjorde 
att arbetet flöt på mindre bra. 

Jag fick höra ibland under projektets gång att jag skulle ta det lugnt och vi hade mycket tid kvar etc. Jag upplevde
dock innan presentationen att mina kamrater insåg att jag var rätt på det samt att vi borde ha varit mer föreberedda.
Jag fick höra under veckan av en av våra gruppmedlemmar att vi inte skulle ha någon presentation framför klassen,
vi var alltså på plats helt oförberedda vilket säger en hel del. Med det sagt jag vill föröska bli bättre på att
kommunicera till mina gruppmedlemmar framöver på ett mer lugnt sätt oavsett hur pass mycket arbete vi har kvar eller
hur efter vi är. 


#
Jag har reflekterat en del om vad jag tar med mig från projektet för även om det var en gansak en negativ upplevelse 
överlag så finns det alltid något positivt att hämta från de värsta upplevelserna. Rent "kodningsmässigt" så tycker
jag inte att jag utveckaldes särskilt mycket alls utan det var som jag nämnde innan mycket det här med github men 
även hur det är att jobba som grupp i programmering. Hur man strukturerar arbetat, hur EXTREMT viktigt det är med
kommunikation och hur påfrestande det kan vara. Det kanske låter konstigt men jag tänker också att jag har upplevt 
hur det är att arbeta i en dåligt fungerande grupp, det kan förhoppningsvis inte bli mycket värre. 
Jag hoppas att mina kamrater också lärde sig en hel del och förhoppningsvis så blir vårt nästa gruppprojekt mer 
lyckat oavsett vem vi hamnar med. 


# Dagbok 

2025/10/08 

Vi har ett gruppmöte på Teams där vi kommer överens om att skapa ett UML för grupparbetet.

2025/10/08
Börja jobba med login för samtliga användare. Vi delade upp arbetet också, jag blev tilldelad users.cs (login delen).

-Skapat interface IUser för alla användare.

-Implementerat User-klass med email och lösenord.

-Gjort TryLogin-metod som returnerar true/false.

-Bestämt gemensamma krav för alla användare i systemet.

2025/10/13 
Skoldag: strukturerade arbetet och jobbade med login delen personel.cs tillsammans mad Arbaz. Han kodade jag berätta vad han skulle koda. 

2025/10/14
Glömde att skriva vad jag gjorde den här dagen. Har för mig att jag jobbade vidare med min user.cs

2025/10/16
Vi fick skapa ett nytt repository och börja om från början. Vi strukturerade om arbetet och la fram en ny plan. Jag skapade en basic user registration. 

Lagt till rollbaserade behörighetskontroller i Program.cs så att endast SuperAdmin och Admin kan lägga till nya platser. Testade inloggning och meny för olika användarroller

2025/10/17 
Jag arbetade med Neha med rolepermission och role, skapade behörigheter för olika användarroller. 

2025/10/20 
Var sjuk idag.

2025/10/21
Idag jobbade jag med behörigheter för Personnel. Jag la till en lista med permissions i User.cs och en metod HasPermission för att kolla om användaren får göra något. I Program.cs ändrade jag menyn så att varje val bara fungerar om användaren har rätt behörighet, t.ex. se scheman, godkänna bokningar och läsa journaler. Jag la dessutom till enum med permissiontype i permission.cs 

2025/10/22
Spenderade stora delar av dagen med att lösa merge konflikter vi hade. Strukturerade om kod och hade en massa samtal med mina klasskamrater. 
-La till patientstatus enum i permission.cs för att hantera godkännande och avslag för aptienter.

-Lagt till journalentry klass med readlevel i permission.cs.

-Uppdaterat patient.cs för att lagra journalentry objekt istället för strängar.

-Lagt till addjournalentry metod i Patient.cs.

-Uppdaterat showjournal metod i patient.cs för att filtrera journalinlägg baserat på tittarens roll och readleevel.

-Fixat i program.cs för att skicka aktiv användare till showjournal och hantera visning av journaler för rollerna admin, personel och patient.
