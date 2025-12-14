# TodoFilter Razor Demo

> Samma filtreringslogik som `demos/TodoFilter`, men med en Blazor UI för att visa skillnaden mellan testdriven backend och interaktiv front.

## Vad är nytt jämfört med `TodoFilter`?
- ✅ Samma `TodoFilterService` kopierad rakt av (ingen UI-logik i den klassen).
- ✅ Blazor-sida som visar filtren live (status, datum, textmatchning).
- ✅ Statisk seed-data – inga API:er eller databaser behövs.
- ✅ Lämplig för demo: ändra filtret eller koden och se effekten direkt.

## Kör
```bash
cd demos/TodoFilter_razor/src/TodoFilter.Razor
dotnet restore   # kräver nätverk mot nuget.org
dotnet watch run # öppnar http://localhost:5148 (https på 7148)
```

> Om den försöker öppna https://localhost:0 beror det på launchSettings som nu är fastsatt på port 5148/7148.

## Struktur
```
TodoFilter_razor/
├── README.md
└── src/
    └── TodoFilter.Razor/
        ├── Models/Todo.cs              # samma modell som i C#-demot
        ├── Services/TodoFilterService.cs# oförändrad filtreringslogik
        ├── Data/TodoRepository.cs      # seed-data för UI:t
        ├── Pages/Home.razor            # interaktiv filtervy
        ├── Layout/...                  # enkel navigation/layout
        └── wwwroot/css/app.css         # lätt styling för tabellen/badges
```

## Demo-upplägg
1. Öppna `Pages/Home.razor` och visa hur filtret ropar på `TodoFilterService`.
2. Byt status i dropdown eller skriv en söksträng – UI använder samma logik som xUnit-testen i `TodoFilter`.
3. Lägg till ett nytt filtersteg och påpeka hur AI kan hjälpa, precis som i originaldemot.

Tipset till publiken: backend-koden är identisk, det enda som skiljer är att testfallet ersatts av en Blazor-sida.
