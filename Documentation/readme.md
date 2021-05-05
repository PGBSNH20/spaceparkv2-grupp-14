# Documentation

## 28/04-2021

* Vi satt och planerade hur vi ska implementera vår kod och projekt. Vi Gjorde två olika Flowcharts.
Ena visar hur flowet ser ut när man använder vår program stegvis och det använder är mera 
fokuserad på kommunikationen mellan Client, API:et och DB:n.
* Vi skapade ett ConsoleApp, WebApi och la till RestClient Nuget Package.

# Flowcharts
#### Menu
![menu_flowchart](https://i.imgur.com/H6mORe0.png)

#### Client to Web API to Database communication example
![communication_flowchart](https://i.imgur.com/SJ3ecNW.png)

## 29/04-2021
Vi skapade några modeller och följe på vår flowchart. Modellerna döpte vi till
Account, Pay, People, Startship. Vi har skapat några controllers för att kunna kommunicera
med vår API för att hämta en lista på alla startship, Förare etc.

## 30/04-2021
Nedan här har vi en till flowchart gällande lösenord kryptering. Som ni ser så skickar klienten ett registreringsbegäran
till WebAPI:e. Därefter skickar API:et begäran med det icke-krypterade lösenord till Server klienten. Därefter
skapas ett entry med förarens namn, användarnamn och ett krypterad lösenord i själva databasen.
![30 april](https://user-images.githubusercontent.com/48633146/117115805-e510a780-ad8d-11eb-8585-55dbb6c53d9c.png)
