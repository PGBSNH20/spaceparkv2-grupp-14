# Documentation

## 28/04-2021

* Vi satt och planerade hur vi ska implementera vår kod och projekt. Vi gjorde två olika Flowcharts.
Ena visar hur flowet ser ut när man använder vår program stegvis och det använder är mera 
fokuserad på kommunikationen mellan Client, API:et och DB:n.
Vi skapade ett ConsoleApp, WebAPI och la till RestClient Nuget Package.
* ConsoleApp är i princip vår entrypoint där klienten har tillgång till för
att kunna registrera sig själva, registera parkering och betala för sin parkering.
Det är också en entrypoint för admin att kunna logga in, se parkeringshistoriken,
lägga till spaceport, nuvarande info gällande parkeringarna.
* WebAPI:et är i princip vår verktyg för kunna kommunicera mellan klienten och databasen.

# Flowcharts
#### Menu
![menu_flowchart](https://i.imgur.com/H6mORe0.png)

#### Client to Web API to Database communication example
![communication_flowchart](https://i.imgur.com/SJ3ecNW.png)

## 29/04-2021
Vi skapade några modeller och följe på vår flowchart. Modellerna döpte vi till
Account, Pay, People, Startship. Vi har skapat några controllers för att kunna kommunicera
med vår API för att hämta en lista på alla startship, Förare etc.
* People = Det här klassen är för förarnas information. Det betstår av ID, Name och En lista av Vehicles properties.
* Vehicles = Det består av ett Id och ett Vehicle property som hämtar vehicle länkarna som en string.
* Account = Det här klassen består av ID, Username, Password, FK_People där vi kan se vilken förareID som är kopplad till respektive kontot.
Detta gäller enbart kunder. För Admin har enbart ett inbyggt konto och det har ingen koppling med detta alls.
* Starship = Innehåller ID, Name, Model, Passengers och det är dem infon vi valde att hämta via vår web api.
* Pay = Innehåller ID, StartParking, PaidAt, Person och där kan vi se vilken personID som har reggat, betalat och ser start på
personens startparkeringstid.

Vi skapade några Controllers som t.ex. AccountController.

## 30/04-2021
Nedan här har vi en till flowchart gällande lösenord kryptering. Som ni ser så skickar klienten ett registreringsbegäran
till WebAPI:e. Därefter skickar API:et begäran med det icke-krypterade lösenord till Server klienten. Därefter
skapas ett entry med förarens namn, användarnamn och ett krypterad lösenord i själva databasen.

![30 april](https://user-images.githubusercontent.com/48633146/117115805-e510a780-ad8d-11eb-8585-55dbb6c53d9c.png)

## 01/05-2021
* Vi skapade ett async metod för att hämta alla Starships från WebAPI:et genom vår Starship och StarshipResponse klass i samband
med vår StarshipController.
* Minor bugg: ShowMenu metod i vår Menu klass hade vi ett litet problem med där inte kunden inte kunde välja olika alternativ i consoleAppen genom att trycka på
upp och ner pilknappen. (Status = Fixed).
* Fixade till vår kod i console så att det blir färre rader helt enkelt.

