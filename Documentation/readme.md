# Documentation

## Innehållsförteckning
1. Endpoints
2. Översikt
3. Test
4. Code struktur
5. Detaljer
6. Daglig rapport (Kan istället skriva hur långt tid tog vissa grejer mer än dem andra)

## Endpoints
### Base
`/api`

### Account
*POST* `api/account` - Adds a new account to the database

<details>
<summary>Example</summary>
Body: 
  
```js
{
  "Username":"JohnDoe",
  "Password":"123",
  "People":
  {
    "Name":"Darth Vader"
  }
}
```
Responses: 
* Returns Status code 201 (Created) if registration succeded.
* Returns Status code 409 (Conflict) if username or star wars person already exists.
* Returns Status code 406 (Not acceptable) if star wars person does not exist on the [Star Wars API](https://swapi.dev/api/people/).
    * This is to ensure you can only register as a star wars character.
</details>

*GET* `api/account/{username},{password}` - Used for account validation

<details>
<summary>Example</summary>
Body:
  
```js
{
  "Username":"JohnDoe",
  "Password":"123"
}
```
Responses: 
* Returns Status code 200 (OK) if body matches anything in the database
* Returns Status code 401 (Unauthorized) if body does not match anything in the database
</details>



# Översikt

Vi satt och planerade hur vi ska implementera vår kod och projekt och två flowchart som har beskrivit om lite längre 
ner i vår Översikt beskrivning. 
Vi skapade ett ConsoleApp, WebAPI och la till RestClient Nuget Package.
1. ConsoleApp är i princip vår entrypoint där klienten har tillgång till för
att kunna registrera sig själva, registera parkering och betala för sin parkering.
Det är också en entrypoint för klienten att kunna logga in, se parkeringshistoriken,
lägga till spaceport, nuvarande info gällande parkeringarna.
2. WebAPI:et är i princip vår verktyg för kunna kommunicera mellan klienten och databasen.

Via vår consoleapp som är vår entrypoint kan klienten välja att registrera eller logga in eller Exit. Väljer man
Register då kan registrera sig genom att knappa in sitt username och lösenord. Därefter kan man välja
Register Parking för att parkera sitt StartShip. Väljer klienten Log In så kan dem logga in om de knappar
in rätt user och pass. Därefter kan de betala för sitt parkering och de kan ej registrera för sitt parkering
om de redan har parkering 1 gång. Klienten kan se parkerings historik, Nuvarande parkeringar. Loggar man in
som admin kan man tillägga fler SpacePort. 

Här har ni våra flowcharts:
Första visar hur flowet ser ut när man använder vår program stegvis och det andra är mera 
fokuserad på kommunikationen mellan Client, API:et och DB:n.

## Flowcharts
#### Menu
![menu_flowchart](https://i.imgur.com/H6mORe0.png)

#### Client to Web API to Database communication example
![communication_flowchart](https://i.imgur.com/SJ3ecNW.png)




# Code Struktur

När en klient knappar in ett username, password och ett korrekt förarenamn så skickar vi detta
till vår Register metod som skapar en Post Request till vår Accountcroller.

```csharp
public static async Task<IRestResponse> Register(string user, string pw, string character)
        {
            var account = new Account()
            {
                Username = user,
                Password = pw,
                People = new People()
                {
                    Name = character
                }
            };

            var client = new RestClient(BASE);
            var request = new RestRequest("api/Account", Method.POST);
            request.AddJsonBody(JsonConvert.SerializeObject(account));

            var response = await client.ExecuteAsync(request);

            return response;
        }
```
Får klienten ett 201 Created status så returenes ett Account objekt annars
får dem göra om hela processen.
```csharp
if (response.Result.StatusCode == HttpStatusCode.Created)
            {
                return new Account()
                {
                    Username = username,
                    People = new People()
                    {
                        Name = realname
                    }
                };
            }
            else
            {
                Console.WriteLine(API.ParseStatus(response.Result));
                var option = Menu.ShowMenu($"{API.ParseStatus(response.Result)}\nRetry?", new string[] { "Yes", "No" });

                if(option == "Yes")
                {
                    return null;
                }
                else
                {
                    Initializer.Run();
                }


                return null;
            }
```

Som jag nämnde innan när en klient registrerar sig så skickar en Post request till vår AccountController och därefter skapas det i vår Account Tabell.  
När klienten loggar in så kollar vi först så att att det inmatade username och password finns med. Om det inte finns med får klienten göra
ett nytt försök annars så skickar vi en Get request till DB:n för att hämta informationen. Därefter så är klienten
inloggad. 

Varje gång klienten registrerar sin parkering så skickas det ett Post request till ParkController och därefter sparas det i Pay tabellen.
Om samma Person med samma ID har ej betalat för sin parkering då kan dem ej registrera en ny parking igen. Här nedan kan ni se hur kod strukturen är för
detta:

```csharp
[HttpPost]
        public async Task<IActionResult> Post([FromBody] Pay park)
        {
            using(var db = new SpaceContext())
            {
                if(db.Pay.Any(x => x.Name == park.Name && x.PaidAt == null))
                {
                    return Conflict("Your account already has one unpaid parking at the moment. To resolve this, pay and retry parking.");
                }
                else
                {
                    park.PaidAt = null;
                    db.Pay.Add(park);
                    db.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created, "Your parking has been registered!");
                }
            }
        }

```

Det inloggade klienten kan betala för sitt parkering och det kan vi ske om enligt nedan:
Om samma Person har med samma Id har fortfarande ej betalat för sin parkering så kan dem betala det.
Med andra ord om vår PaidAt property i vår Pay tabell är Null så kan dem betala annars får dem felmeddelande
om att Parkeringen är redan betald.

```csharp
[HttpPut("{id}")]
        public async Task<IActionResult> Put(int id)
        {
            using(var db = new SpaceContext())
            {
                try
                {
                    var pay = db.Pay.Where(x => x.ID == id).First();
                    if (pay.PaidAt == null)
                    {
                        pay.PaidAt = DateTime.Now;
                        db.SaveChanges();

                        return Ok("Your parking has been paid for and you may now park again if you wish.");
                    }
                    else
                    {
                        return Conflict($"Parking with id [{id}] has already been paid");
                    }
                }
                catch(InvalidOperationException)
                {
                    return NotFound($"No parking with id [{id}] exists.");
                }
            }
        }
```
Den inloggade klienten kan se sin Parkerings historik och det hämtar vi genom att anropa på DB:n med en
Get-Request. och då kan vi se en lista på alla förarna som har registrerat sig i Pay tabellen. Detta gäller
för både kunder som har betalat och ej betalat. Dock i Nuvarande Parkerings alternativet kan man enbart se
kunder som inte har betalat för sitt parkering.

Klienten kan också loggas in som admin och det kanske ske om dem endast slår in rätt admin user och pass.
I vårt system har vi enbart ett admin konto och User är admin och Pass är admin123. Loggar man in som admin
kan man göra allt som kunderna också kan plus att de har ett ytteligare alternativ som är för att skapa ett Spaceport.
Pay tabellen i vår DB är kopplat till Spaceport klassen som vi har skapat och i varje Spaceport kan 
max 5 Starships parkeras.


# Test

Testet nedan ser till så att rätt antar People från API:et hämtas.

```csharp
 [Fact]
        public void FetchAllPeople_Test()
        {
            ISwapi<People> swapi = new SwapiPeople();
            var result = swapi.FetchAll().Result;

            Assert.Equal(82, result.Count);
        }
```
Testet nedan ser till att Rätt ID är förknippat med rätt förarenamn.

```csharp
[Fact]
        public void FetchPeopleById_Test()
        {
            ISwapi<People> swapi = new SwapiPeople();
            var result = swapi.FetchById(3).Result;

            Assert.Equal("R2-D2", result.Name);
        }
```


# Detaljer
Vi har en extra säkerhetsdetalj där vi enkrytar passwords:en som förs in i DB:n och det har vi gjort genom att skapa ett 
Encryption klass som ni ser nedan:
Encrypt() metoden i klassen enkryptar passworden som vi matar. 
```csharp
public class Encryption
    {
        public static string Encrypt(string str)
        {
            string result = string.Empty;

            for(int i = 0; i < str.Length; i++)
            {
                if (str[i] < 120)
                    result += Convert.ToChar(str[i] + 7);
                else
                    result += str[i];
            }
        }
    }
```
Detta används i t.ex. vår Get() method i vår AccountController som ni ser nedan:
```csharp
using (var db = new SpaceContext())
            {
                try
                {
                    var acc = db.Accounts.First(x => 
                        x.Username.ToLower() == username.ToLower()
                        && Encryption.Encrypt(password) == x.Password);

                    return Ok("Logged in successfully!");
                }
                catch (InvalidOperationException)
                {
                    return Unauthorized("Invalid login parameters.");
                }
            }
```
Nedan här har vi en till flowchart gällande lösenord kryptering. Som ni ser så skickar klienten ett registreringsbegäran
till WebAPI:e. Därefter skickar API:et begäran med det icke-krypterade lösenord till Server klienten. Därefter
skapas ett entry med förarens namn, användarnamn och ett krypterad lösenord i själva databasen.

![30 april](https://user-images.githubusercontent.com/48633146/117115805-e510a780-ad8d-11eb-8585-55dbb6c53d9c.png)

# Misstag som vi ändrade under processen

## Början
Från början skrev vi lite pseudokod för registreringsprocessen. Tanken bakom koden är att om vi ska kunna hitta rätt förarenamn 
från webAPI:et som ska kunna registrera sig och få ett StatusCode 200 att detta är godkänt annars ska vi få statuscode 404 eftersom att vi ej hittar det namnet i vår webAPI.
* Vi skapade ett interface där klasserna SwapiPeople och SwapiStarship implementerar detta. Dessa klasser har vi användt för att skapade ett anrop mot webAPI:en.
Vi flyttade vår Get() method som vi hade i StarshipController till SwapiStarship istället och döpte om metoden till FetchAll().
* Vi ska 4 stycken unittest och dessa heter:
* Fetch_All_Starships_Test() = Testar om vi får alla starships.
* Fetch_Starships_By_Id_Test() = Test om vi kan hitta den respektive starship genom att skriva in starships ID.
* Fetch_All_People_Test() = Testar om vi får alla Förararna.
* Fetch_People_By_ID_Test = Test om vi kan hitta den respektive förare genom att skriva in förerens ID.
* Skapade ett SpaceContext klass där vi kopplar våra lokala connectionstring för att kunna ansluta oss mot DB:n.
Därefter la vi ett init migration för att uppdatera vår DB med våra tabeller.
På bilden nedan ser ni relationerna mellan våra tabeller
![DBConnection](https://user-images.githubusercontent.com/48633146/117143352-05059280-adb1-11eb-8d9a-ca66da86859b.PNG)

## Utvecklare API 

För att kunna läsa om våran API end point: 
* Starta programmet i dubug lägge => iisexprss 
* Navigrea till https://localhost:44332/swagger/index.html 
* Du kommer fram till en swager sida med alla end-point. 
* För att kuna test APIet med Postman
* Starta progarmmet i debug lägge => SpaceParkAPI
* Starta postman applikationen 
* välja metod typen /Get, /Post 
* Endpoint http://localhost:5000/api/{controller name}

## Slutet

I slutet märkte vi att vi hade lite för många tabeller och såg att vi inte hade t.ex. ett behov av Vehicle tabellen. Eftersom att Infon i Vehicle och
Starship var ganska lika om de propertiesen vi behövde kunde vi hämta från båda tabellerna. Därefter insåg vi att vi inte ens behövde ha Starship tabellen
över lag och vår slutliga DB tabelln kan ni på bilden nedan:
