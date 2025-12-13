# Code Quality Checklist

> Använd denna checklista innan varje commit. Inga undantag.

---

## 🎯 The Rule

**Alla checkboxar måste vara ✅ innan commit.**

Om något är ❌ → Fixa det först.

Inga genvägar. Inga "fixa sen". Inga "det funkar ju".

---

## ✅ Checklist

### 1. Testerna säger OK

- [ ] **Tester skrivna först** (innan implementation)
- [ ] **Normala flödet fungerar** (det förväntade scenariot)
- [ ] **Edge cases hanterade** (tom input, null, stora dataset, etc)
- [ ] **Error cases hanterade** (vad händer vid fel?)
- [ ] **Alla tester gröna** (inga skip, inga ignore)

**Prompt för AI:**
```
Granska mina tester. Täcker de:
- Normala flödet?
- Edge cases (tom input, null, stora dataset)?
- Error cases (exceptions, invalid input)?

Vad saknas?
```

**Red flag:**
❌ "Det funkar när jag testar manuellt"
❌ "Jag lägger till tester sen"
❌ "Det är för enkelt för att behöva tester"

**Green flag:**
✅ Testerna skrevs INNAN koden
✅ Edge cases testade
✅ 100% grönt

---

### 2. Koden är läsbar

- [ ] **Metodnamn förklarar VAD** (inte HUR)
- [ ] **En metod = en uppgift** (SRP - Single Responsibility Principle)
- [ ] **Variabelnamn är självförklarande**
- [ ] **Ingen upprepad logik** (DRY - Don't Repeat Yourself)
- [ ] **Ingen "clever" kod** (KISS - Keep It Simple, Stupid)
- [ ] **Kan förstås utan kommentarer** (koden förklarar sig själv)

**Prompt för AI:**
```
Granska denna kod för läsbarhet:
- Förklarar metodnamnen VAD (inte HUR)?
- Gör varje metod EN sak?
- Finns upprepad logik?
- Finns "clever" kod som kan förenklas?
- Kan koden förstås utan kommentarer?

[KLISTRA IN KOD]
```

**Exempel på bra namn:**

✅ `FilterTodosByStatus(todos, status)`
❌ `DoStuff(list, str)`

✅ `CalculateTotalPrice(items)`
❌ `Calc(x)`

✅ `IsValidEmail(email)`
❌ `Check(s)`

**SRP-test (Single Responsibility Principle):**
> Kan du beskriva metoden utan att använda "och"?
>
> ✅ "Filtrerar todos" (EN sak)
> ❌ "Filtrerar todos OCH sorterar OCH sparar" (TRE saker)

**DRY-test (Don't Repeat Yourself):**
> Kopierar du samma logik på flera ställen?
>
> ❌ Ja → Bryt ut till egen metod
> ✅ Nej → Bra

**KISS-test (Keep It Simple):**
> Kan det göras enklare?
>
> ❌ Nested ternary operators
> ❌ Regex när string.Contains() funkar
> ❌ Rekursion när loop funkar
> ✅ Enklaste lösningen som fungerar

---

### 3. Strukturen är tydlig

- [ ] **Rätt ansvar på rätt ställe** (SoC - Separation of Concerns)
- [ ] **Beroenden är tydliga och få**
- [ ] **Lätt att hitta saker** (logisk filstruktur)
- [ ] **Klasser/moduler gör EN sak**
- [ ] **Inga guds-klasser** (klasser som gör allt)

**Prompt för AI:**
```
Granska arkitekturen:
- Är ansvaren tydligt separerade?
- Gör varje klass/modul EN sak?
- Finns beroenden som borde brytas?
- Är filstrukturen logisk?

Vad kan förbättras?
```

**SoC-exempel (Separation of Concerns):**

❌ **Dålig separation:**
```csharp
public class TodoManager
{
    // Hanterar databas
    public void SaveToDatabase(Todo todo) { }

    // Hanterar UI
    public void DisplayTodo(Todo todo) { }

    // Hanterar business logic
    public bool IsValid(Todo todo) { }
}
```

✅ **Bra separation:**
```csharp
public class TodoRepository        // Databas
public class TodoValidator         // Business logic
public class TodoView             // UI
```

**Beroende-test:**
> Hur många klasser måste ändras om X ändras?
>
> ❌ Många → För tätt kopplat
> ✅ Få → Bra separation

---

### 4. Nästa person förstår

- [ ] **README uppdaterad** (om nödvändigt)
- [ ] **Kommentarer där det är ovanligt** (inte självklart)
- [ ] **Kommentarer förklarar VARFÖR** (inte VAD)
- [ ] **Commit message förklarar kontext**
- [ ] **Kod kan förstås om 6 månader**

**Prompt för AI:**
```
Förklara denna kod som om jag vore ny på projektet:
- Vad gör den?
- Varför är den strukturerad så?
- Finns något ovanligt som borde kommenteras?

[KLISTRA IN KOD]
```

**Kommentar-regler:**

❌ **Dåliga kommentarer (förklarar VAD):**
```csharp
// Loopar igenom todos
foreach (var todo in todos) { }

// Returnerar true
return true;
```

✅ **Bra kommentarer (förklarar VARFÖR):**
```csharp
// Timeout måste vara längre än 30s pga external API rate limit
const int timeout = 35000;

// Case-insensitive matching - users expect "Done" = "done"
status.Equals(filter, StringComparison.OrdinalIgnoreCase)
```

**README-test:**
> Kan en ny utvecklare:
> - Förstå vad projektet gör?
> - Sätta upp utvecklingsmiljön?
> - Hitta var de ska börja läsa kod?
>
> ❌ Nej → Uppdatera README
> ✅ Ja → Bra

**Commit message-format:**
```
[VAD] - [VARFÖR]

✅ Add case-insensitive filtering - Users expect search to work regardless of case
✅ Refactor TodoFilter to extract validation - Reduce method complexity from 45 to 8 lines

❌ Fixed bug
❌ Updated code
❌ Changes
```

---

### 5. Steget är säkrat

- [ ] **Alla tester gröna** (ingen failar)
- [ ] **Koden kompilerar** (inga syntax-fel)
- [ ] **Inga varningar** (eller medvetna med kommentar)
- [ ] **Commit message skriven**
- [ ] **Redo för code review**

**Prompt för AI:**
```
Finns något som kan gå fel med denna kod i produktion?
- Null-hantering?
- Thread safety?
- Performance på stora dataset?
- Security issues?

[KLISTRA IN KOD]
```

**Pre-commit test:**
```bash
# Kör alla tester
dotnet test

# Kolla warnings
dotnet build

# Kolla code style (om du har linter)
dotnet format --verify-no-changes
```

**Commit bara om:**
✅ Alla tester gröna
✅ Build lyckas
✅ Inga nya warnings
✅ Du förstår vad du committar

---

## 🚀 Quick Reference

### Innan du börjar koda:
```
□ Problemet förtydligat (EN mening)
□ Minimispec skriven (3-5 rader)
□ Intent prompt redo
```

### Under kodning:
```
□ Tester FÖRST
□ Implementation SEN
□ Refactorera SIST
```

### Innan commit:
```
□ 1. Testerna säger OK
□ 2. Koden är läsbar
□ 3. Strukturen är tydlig
□ 4. Nästa person förstår
□ 5. Steget är säkrat
```

---

## 💡 The 2-Minute Rule (igen)

Innan du commitar:
> **Kan du förklara vad koden gör på 2 minuter?**

**JA** → Fortsätt till commit
**NEJ** → Du förstår inte koden tillräckligt bra. Refaktorera eller be AI förklara.

---

## 🎯 AI-Prompt för hel-checklist

```
Granska denna kod mot följande kvalitets-checklist:

1. TESTER
   - Tester först?
   - Täcker normala flödet + edge cases?
   - Alla gröna?

2. LÄSBARHET
   - Självförklarande namn?
   - SRP (en uppgift per metod)?
   - Ingen upprepad logik (DRY)?
   - KISS (så enkelt som möjligt)?

3. STRUKTUR
   - Separation of Concerns?
   - Tydliga beroenden?
   - Logisk organisation?

4. FÖRSTÅELSE
   - Nästa utvecklare förstår?
   - Kommentarer förklarar VARFÖR?
   - README uppdaterad?

5. SÄKRAT
   - Alla tester gröna?
   - Kompilerar?
   - Inga varningar?

Förklara vad som är bra och vad som behöver förbättras.

[KLISTRA IN KOD]
```

---

## ❌ Vanliga ursäkter (och varför de är dåliga)

### "Det funkar ju"
> Fungera ≠ Bra kod
>
> Bra kod = funkar + läsbar + maintainbar + testad

### "Jag lägger till tester sen"
> Sen = Aldrig
>
> TDD first. Alltid.

### "Det är för enkelt för att behöva tester"
> Enkel kod buggar också.
>
> Dessutom: Tester är dokumentation.

### "Jag förstår den inte men AI sa att den var bra"
> AI kan ha fel. DU äger koden.
>
> Om du inte förstår - be om förklaring eller förenkla.

### "Jag fixar det i nästa commit"
> Teknisk skuld växer.
>
> Fixa NU medan du är i kontexten.

### "Ingen annan kommer läsa den här koden"
> Du om 6 månader ÄR "någon annan".
>
> Skriv för framtida-dig.

---

## ✅ Mantran

> **"TDD first, sen commit"**

> **"Små steg, stora system"**

> **"Fungerar ≠ Klart"**

> **"Skriv kod för framtida-dig"**

> **"AI föreslår. Jag bestämmer."**

---

**Nästa steg:**
- Använd [Prompt Templates](PROMPTS.md) för AI-granskning
- Läs mer om [Principer](PRINCIPLES.md)
- Se exempel i [TodoFilter demo](../demos/TodoFilter/)

---

**Skapad av Marcus Ackre Medina**
📚 [AI-Driven Development på GitHub](https://github.com/MarcusMedina/ai-driven-workflow)
