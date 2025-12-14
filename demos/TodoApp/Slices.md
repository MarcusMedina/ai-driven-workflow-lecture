# Arkitekturval: Balanserad (production-ready) för alla slices

> Vi kodar inte här – detta är instruktioner för hur varje slice ska byggas med samma arkitekturprinciper.

## Översikt
- **UI:** Blazor Server-komponenter. Endast tunn UI-logik; inga datastrukturer direkt i komponenterna.
- **Applikationslager:** Tjänster med business-regler (t.ex. `TodoService`). All validering och invariants här.
- **Datalager:** Repo + EF Core mot en liten DB (SQLite för lokal dev, SQL Server i drift). Migrationer för schema.
- **Validering & sanitering:** DataAnnotations/FluentValidation i tjänstelagret; sanitering av fri text; återge felmeddelanden kontrollerat.
- **Säkerhet:** CSRF/antiforgery (redan i Blazor Server), framtida auth via cookie/Identity när behov uppstår.
- **Testbarhet:** Enhetstester på tjänstelagret; integrationstester mot in-memory/SQLite in-memory vid behov.
- **Observability:** Minimal loggning runt mutations (skapa/ändra) med korrelations-id i pipeline när det kommer fler användare.

## Generellt arbetsflöde per slice
1) Definiera datamodell och invariants i applikationslagret.  
2) Lägg till/uppdatera migrations (EF Core) – schema ändras endast via migration.  
3) Implementera tjänstemetoder med validering, sanitering och transaktioner vid behov.  
4) Anropa tjänster från UI; UI ska hantera loading/fel men inte affärsregler.  
5) Lägg till tester på tjänstelagret för nya regler/kommandon.  
6) Manuell E2E-verifiering av slice-flödet.  

## Slice 1 — Skapa → se → bocka av en todo
- **Datamodell:** `Todo { Id (GUID), Text, Completed, CreatedAt }`. `CreatedAt` är immutable.
- **Validering:** Text obligatorisk, trimmas, minsta längd 2–3 tecken, max t.ex. 200–500 tecken; spara som normaliserad trimmed text.
- **Sanitering:** HTML-encode/neutralisera skript i UI-rendering (default Blazor), men också server-side trim/normalisera whitespace.
- **Persistens:** EF Core + SQLite; skapa migration för `Todos`-tabell med index på `CreatedAt`.
- **Operationer:** Append-only + toggle. Ingen deletion. Toggle får inte ändra `CreatedAt`.
- **UI:** Enkel lista utan filtrering; checkbox för toggle; visa statuschip; visa `CreatedAt` lokal tid.
- **Tester:** Enhetstester för `AddTodo` (validering, trim, kräver text), `ToggleTodo` (byter status, lämnar `CreatedAt` oförändrad).

## Framåt (andra slices) — håll samma principer
- När nya fält/funktioner tillkommer: uppdatera modell + migration; validera i tjänstelagret; UI bara konsumerar.
- Batch/filters/sortering: inför query-metoder i tjänsten, inte i UI.
- Eventuella borttagningar/återställningar: använd mjuk-radering med `DeletedAt` + index, aldrig hårdradera utan beslut.
- Auth/roller när det behövs: lägg i middleware + policys; UI frågar tjänstelagret för behörighetsbeslut.

## Framtida slices (förbättringar på CreateTodo) — Klara
- Extrahera validering/sanitering för titel till separat metod/helper (DRY + SRP) och dela med tester. **Klar**
- Centralisera encoder-konfiguration (HtmlEncoder factory/static) och återanvänd i DI + tester. **Klar**
- Whitespace-normalisering (kollapsa inre whitespace efter trim) för konsistenta titlar och mindre utrymme för trickad input. **Klar**

## Ny slice: Sök och filtrering
- Funktion: Fritext-sök i todo-text och filter för status (alla/öppna/klara).
- Krav: Ingen mutation; endast query-path i tjänstelager. UI skickar filter/söksträng, tjänsten returnerar filtrerad/sorterad lista.
- Validering: Trim och begränsa längd på söksträng (t.ex. max 100 tecken). Sanitering via befintlig encoder (UI visar encoded).
- Performance: Index på Text/Completed om vi kör DB; i minnesläge enkel LINQ med ordentlig ordning (senaste överst).
- Testning: Enhetstester på tjänstens query-metoder (sök + statusfilter kombinerat), edge-cases för tom/null sök.

## Ny slice: Kategorier
- Funktion: Todos har kategori (default "Allmänt"), visas i listan och kan filtreras.
- Krav: Validera/trim/sanera kategori. Default om tom/null. Kategorifilter (alla eller specifik kategori) i query-väg.
- UI: Fält för kategori vid skapande (förifyllt med "Allmänt"), chip i listan och dropdown för att filtrera kategori.
- Testning: Enhetstester som täcker defaultkategori, custom kategori (trim/sanera) och filtret i query-metoden.
