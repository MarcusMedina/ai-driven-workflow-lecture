# The AI-Driven Development Workflow

> En 7-stegs process för att gå från idé till produktionskod med AI som partner

---

## 🎯 Filosofi först

**AI är inte autopilot. AI är en kraftförstärkare.**

Om du jobbar kaotiskt → AI förstärker kaoset
Om du jobbar strukturerat → AI förstärker produktiviteten

Det här är inte "ännu ett verktyg att lära sig". Det här är **hur man tänker** när man jobbar med AI.

---

## 🔄 The Dev Loop (översikt)

```
    1. Förtydliga (DU - 2 min)
         ↓
    2. Backlog & Vertical Slices (AI hjälper - 3 min)
         ↓
    3. Arkitektur (AI föreslår, DU väljer - 3 min)
         ↓
    4. TDD - Tester + Implementation (AI kodar båda - 5 min)
         ↓
    5. Manuell test (DU testar - 3 min)
         ↓
    6. Refaktorera (AI granskar, DU beslutar - 3 min)
         ↓
    7. Commit (DU commitar - 2 min)
         ↓
    (Nästa vertical slice)
```

**Total tid per slice: ~20 minuter**

Varje iteration levererar **värde** (en komplett user journey).

**Små steg. Snabba loopar. Verifierad kod.**

---

## 🎯 De 7 stegen - med promptar

### Steg 1: Förtydliga & Spec (DU)
**Tid: ~2 minuter**

Du gör detta SJÄLV innan AI. Skriv ner:
```
Problem: [EN mening - vad ska lösas?]
Input: [Vad kommer in?]
Output: [Vad ska ut?]
Edge cases: [Tom input? Null? Ogiltiga värden?]
```

**Ingen AI här.** Du måste förstå problemet först.

---

### Steg 2: Backlog & Vertical Slices (AI hjälper)
**Tid: ~3 minuter**

**Prompt:**
```
Jag vill bygga [FEATURE/SYSTEM].

VIKTIGT: Vi planerar nu, inget kodande än.
Bara brainstorming och nedbrytning.

Bryt ner i vertical slices där varje slice:
- Är en KOMPLETT user journey (end-to-end)
- Ger värde separat
- Kan byggas på 15-25 minuter
- Är testbar och deploybar
- Respekterar dataintegritet

Ge mig de 3 viktigaste slicesen först, prioriterade efter värde.
```

**AI ger förslag. DU väljer vilka slices och i vilken ordning.**

---

### Steg 3: Arkitektur (AI föreslår, DU väljer)
**Tid: ~3 minuter**

**Prompt:**
```
För denna vertical slice: [BESKRIV SLICE]

VIKTIGT: Vi planerar arkitektur nu, inget kodande än.
Bara jämföra approaches.

Ge mig TRE arkitektur-approaches:
1. Enklast möjliga (prototyp)
2. Balanserad (production-ready, säker)
3. Enterprise (fullt utbyggd)

För varje approach, förklara:
- Komplexitet
- Säkerhet (validering, sanitering)
- Performance
- Maintainbarhet
- Trade-offs

Rekommendera baserat på: [ditt scenario - t.ex. "litet team, MVP-fas, dataintegritet viktigt"]
```

**AI producerar flera lösningar - du väljer.**

---

### Steg 4: TDD - Tester + Implementation (AI kodar)
**Tid: ~5 minuter**

**Prompt:**
```
NU KODAR VI (planeringen är klar).

Implementera [FUNKTION/FEATURE] med TDD.

Skriv tester + implementation i ett svep:

Tester för [FUNKTION]:
- Normala flödet: [beskriv]
- Edge case 1: [tom lista]
- Edge case 2: [null-värden]
- Edge case 3: [ogiltiga värden]

Skriv testerna OCH implementationen. Alla tester ska bli gröna.

Krav:
- SRP (en metod = en uppgift)
- DRY (ingen upprepad logik)
- SoC (tydliga ansvarsområden)
- KISS (så enkelt som möjligt)
```

**OBS:** Vi hoppar över red-fasen för enkelhetens skull. AI skriver både tester och implementation direkt.

**Kör testerna. De ska bli gröna. Annars debugga.**

---

### Steg 5: Manuell test (DU testar)
**Tid: ~3 minuter**

Nu testar DU manuellt:
- Kör programmet end-to-end
- Testa UX (känns det rätt?)
- Testa integration (funkar det med andra delar?)
- Performance (är det snabbt nog?)

**Unit-tester täcker logik. Manuella tester täcker UX och integration.**

---

### Steg 6: Refaktorera (AI granskar, DU beslutar)
**Tid: ~3 minuter**

**Prompt:**
```
Granska denna kod:

[KLISTRA IN KOD]

Checklista:
- [ ] Tester (täcker de edge cases?)
- [ ] Läsbarhet (självförklarande namn?)
- [ ] Struktur (följer SRP, DRY, SoC, KISS?)
- [ ] Förstår nästa person koden?
- [ ] Säkerhet (SQL injection, XSS, etc?)

Ge konkreta förbättringsförslag.
```

**AI föreslår. DU beslutar. Refaktorera. Kör testerna igen - de ska förbli gröna.**

---

### Steg 7: Commit (DU commitar)
**Tid: ~2 minuter**

Commit message:
```
[VAD] - [VARFÖR]

✅ Add TodoFilter with case-insensitive matching - Users expect search regardless of case
```

**Sen går du till nästa vertical slice.**

---

## Steg 1: Förtydliga problemet

### Varför?
Innan AI kan hjälpa måste DU veta vad du vill.

### Hur?
Skriv ner problemet i **EN mening**.

#### Exempel:
❌ Dåligt: "Jag vill ha en todo-app"
✅ Bra: "Användare ska kunna filtrera todos efter status"

❌ Dåligt: "Gör det snabbare"
✅ Bra: "API-anrop tar >2 sekunder, ska ta <500ms"

❌ Dåligt: "Fixa buggen"
✅ Bra: "När användare söker med tomma fält kraschar appen"

### The 2-Minute Rule
> Kan du förklara problemet på 2 minuter?
> **JA** → Fortsätt
> **NEJ** → Du förstår inte problemet tillräckligt bra än

---

## Steg 2: Skriv minimispec

### Varför?
En spec tvingar dig att tänka klart. 3-5 rader räcker.

### Format:
```
Problem: [EN mening]
Input: [Vad kommer in?]
Output: [Vad ska ut?]
Edge case: [Vad händer om input är tom/null/konstig?]
```

### Exempel:
```
Problem: Filtrera todos efter status
Input: Lista av Todo-objekt + status-sträng ("done", "pending")
Output: Filtrerad lista med todos som matchar status
Edge cases:
  - Tom lista → returnera tom lista
  - Ingen match → returnera tom lista
  - Case-insensitive matching ("Done" = "done")
```

### Tips:
- Skriv specen i en fil (spec.md, comment, etc)
- AI kan se den när du promptar
- Du kan länka till den i commits

---

## Steg 3: Intent Prompting

### Vad är Intent Prompting?
Istället för att säga **VAD** AI ska göra, säg **VARFÖR** och be om **förklaring först**.

### Exempel:

#### ❌ Traditionell prompt:
```
Skriv en metod som filtrerar todos
```
**Problem:** Du får kod direkt. Kanske bra, kanske dålig. Du vet inte.

#### ✅ Intent prompt:
```
Jag behöver filtrera en lista av objekt efter ett status-fält.
Jag vill ha en ren funktion utan sidoeffekter.

INNAN du kodar: Förklara hur du skulle strukturera det.
Vilka alternativ finns? Vad rekommenderar du?
```

**Resultat:**
- AI tänker högt
- Du får alternativ att välja mellan
- Du förstår trade-offs
- Du är fortfarande arkitekten

### Varför det funkar:
AI som får "förklara först" tvingas:
- Tänka igenom problemet
- Överväga edge cases
- Föreslå clean solutions

---

## Steg 4: AI som arkitekt

### Filosofi:
**AI kan producera 10 lösningar. DU väljer rätt.**

### Prompt-pattern:
```
Ge mig TRE sätt att lösa det här:
1. Enklast möjliga (minimal komplexitet)
2. Balanserat (production-ready)
3. Enterprise-nivå (fullt utbyggt)

Förklara trade-offs och rekommendera för mitt scenario.
```

### Exempel (filtrera todos):

**Alternativ 1: Enklast**
```csharp
todos.Where(t => t.Status == status).ToList();
```
- Pro: En rad, tydlig
- Con: Case-sensitive, ingen null-hantering

**Alternativ 2: Balanserad**
```csharp
public List<Todo> FilterByStatus(List<Todo> todos, string status)
{
    if (todos == null || string.IsNullOrEmpty(status))
        return new List<Todo>();

    return todos.Where(t =>
        t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)
    ).ToList();
}
```
- Pro: Null-safe, case-insensitive, testbar
- Con: Lite mer kod

**Alternativ 3: Enterprise**
```csharp
// ISpecification pattern, repository, DI...
```
- Pro: Fullt extensible
- Con: Overkill för 5 todos

### Din uppgift:
**Välj baserat på kontext:**
- Prototyp? → Alternativ 1
- Produktion? → Alternativ 2
- Skalbar plattform? → Alternativ 3

AI föreslår. **DU** fattar beslut.

---

## Steg 5: Generera små kodblock (med TDD)

### Nyckelprincip:
**En funktion i taget. Tester först.**

### Prompt-pattern:
```
Implementera FilterByStatus med TDD:

1. Skriv tester först för:
   - Normala flödet (lista med matches)
   - Edge case: tom lista
   - Edge case: ingen match
   - Edge case: case-insensitive

2. Implementera funktionen

3. Kör testerna

4. Refaktorera om nödvändigt
```

### Varför TDD med AI?
✅ AI tvingas tänka igenom edge cases
✅ Testerna KÖR direkt - ingen gissning
✅ Du vet att koden faktiskt fungerar
✅ Refactoring blir trygg (testerna fångar regressions)

### The Marcus Method:
> **"TDD first, sen commit"**
>
> Varje steg MÅSTE ha gröna tester innan commit.
> Inga undantag.

### Flöde:
```
1. AI skriver tester → ❌ Red (förväntat)
2. AI skriver implementation → ✅ Green
3. Du granskar → ✅ Refactor
4. Alla tester gröna → ✅ Commit
```

---

## Steg 6: Testa och debugga

### När testerna är röda:

#### Prompt för debugging:
```
Här är testet som failar: [copy-paste]
Här är felet: [copy-paste error message]

Förklara:
1. VAD som händer (steg för steg)
2. VARFÖR det händer (root cause)
3. HUR man fixar det

Jag vill FÖRSTÅ, inte bara få en fix.
```

### Tips:
- Copy-paste EXAKT felmeddelande
- Inkludera stack trace
- Visa testdata som orsakar felet
- Be om förklaring, inte bara kod

### När testerna är gröna:
**KÖR MANUELLT OCKSÅ.**

AI-tester kan missa:
- UI/UX-problem
- Performance-issues
- Integration-buggar

Automation är bra. **Men testa själv också.**

---

## Steg 7: Refaktorera och commit

### Innan du commitar:
Gå igenom [Quality Checklist](QUALITY-CHECKLIST.md):

#### 1. Testerna OK?
- [ ] Tester skrivna först
- [ ] Normala flödet fungerar
- [ ] Edge cases hanterade
- [ ] Allt grönt

#### 2. Koden läsbar?
- [ ] Metodnamn förklarar VAD (inte HUR)
- [ ] En metod = en uppgift (SRP)
- [ ] Ingen upprepad logik (DRY)
- [ ] Ingen "clever" kod (KISS)

#### 3. Strukturen tydlig?
- [ ] Rätt ansvar på rätt ställe (SoC)
- [ ] Beroenden tydliga
- [ ] Lätt att hitta saker

#### 4. Nästa person förstår?
- [ ] README uppdaterad (om nödvändigt)
- [ ] Kommentarer där det är ovanligt
- [ ] Commit message förklarar VARFÖR

### Prompt för code review:
```
Granska koden mot denna checklista:
[copy-paste checklistan]

Förklara vad som är bra och vad som kan förbättras.
```

### Commit message format:
```
[VAD] - [VARFÖR]

Exempel:
✅ Add TodoFilter with case-insensitive matching - Users expect search to work regardless of case
✅ Refactor FilterByStatus to reduce complexity - Method was doing 3 things, now does 1

❌ Fixed stuff
❌ Updated code
❌ Changes
```

### COMMIT OFTA
Små commits = lätt att:
- Hitta när buggar introducerades
- Rollback specifika ändringar
- Code review
- Förstå historiken

**Mantra: "Green tests = Commit time"**

---

## 🔄 Loopen i praktiken

### Scenario: "Bygg en todo-app"

#### Traditionellt (kaos):
```
Prompt: "Bygg en todo-app med frontend och backend"
→ AI genererar 1000 rader kod
→ Du förstår 20%
→ Det funkar... typ
→ Nästa utvecklare gråter
```

#### AI-Driven Workflow (strukturerat):

**Iteration 1:**
1. Förtydliga: "Lägg till en todo"
2. Spec: Input=text, Output=todo med ID+text+status
3. Intent: "Jag vill kunna lägga till todos..."
4. Arkitekt: "Ge mig enklaste vs robust lösning"
5. Kod: TDD → AddTodo() med tester
6. Testa: ✅ Grönt
7. Commit: "Add CreateTodo method"

**Iteration 2:**
1. Förtydliga: "Lista alla todos"
2. Spec: Input=inget, Output=lista av todos
3. Intent: "Behöver visa alla todos..."
4. Arkitekt: "In-memory? Databas? File?"
5. Kod: TDD → GetAllTodos() med tester
6. Testa: ✅ Grönt
7. Commit: "Add GetAllTodos method"

**Iteration 3:**
1. Förtydliga: "Filtrera todos efter status"
2. Spec: (se tidigare exempel)
3. Intent: "Filtrera utan sidoeffekter..."
4. Arkitekt: "LINQ? Custom loop?"
5. Kod: TDD → FilterByStatus() med tester
6. Testa: ✅ Grönt
7. Commit: "Add FilterByStatus with case-insensitive matching"

**Efter 3 iterationer:**
✅ 3 fungerande features
✅ Alla testade
✅ Du förstår varje rad
✅ Clean commit history
✅ Redo för nästa steg

**Total tid: ~30-45 minuter**

---

## 💡 Micro-MVP Thinking

### Definition:
**Micro-MVP = 1 funktion, 1 resultat, körbart på 5-10 minuter**

### Varför?
- AI älskar tydlighet
- Snabba vinnar → motivation
- Små block → lätt att debugga
- Struktur växer organiskt

### Prompt för nedbrytning:
```
Jag vill bygga [STORT FEATURE].

Bryt ner det till micro-MVPs där varje steg är:
- 1 funktion
- 1 tydligt resultat
- Självständigt testbart
- Tar <10 minuter

Lista de 5 första stegen.
```

### Exempel:

#### ❌ Stort: "E-commerce site"
#### ✅ Micro-MVPs:
1. Product class med properties
2. ProductList som håller products
3. AddProduct() metod
4. GetProductById() metod
5. FilterByCategory() metod

Varje steg = committable, körbart, testbart.

---

## 🎓 AI som junior partner

### Mindset:
**Du är arkitekten. AI är byggnadsarbetaren.**

| AI:s styrka          | Din styrka           |
|----------------------|----------------------|
| Syntax               | Arkitektur           |
| Boilerplate          | Design decisions     |
| Implementation       | Direction            |
| Speed                | Quality              |

### Det AI INTE kan:
- Veta VAD som ska byggas
- Förstå business-kontext
- Prioritera features
- Avgöra "tillräckligt bra"
- Se långsiktiga konsekvenser

### Det DU gör:
- Definierar problemet
- Väljer approach
- Granskar kvalitet
- Fattar arkitektur-beslut
- Äger koden

**AI föreslår. Du bestämmer.**

---

## 🚫 Vanliga misstag (och hur man undviker dem)

### 1. "Bygg hela appen"-syndromet
**Symptom:** Prompta för stora features
**Fix:** Micro-MVP. En funktion i taget.

### 2. Blind copy-paste
**Symptom:** Kopiera AI-kod utan att förstå
**Fix:** Be om förklaring. 2-minute rule.

### 3. Skippa tester
**Symptom:** "Det funkar, jag committar"
**Fix:** TDD first. Alltid.

### 4. Ingen refactoring
**Symptom:** "Det funkar" = klart
**Fix:** Quality checklist. Varje gång.

### 5. Acceptera första lösningen
**Symptom:** Tar AI:s första förslag
**Fix:** Be om alternativ. Jämför trade-offs.

### 6. Stora commits
**Symptom:** En commit med 500 rader
**Fix:** Commit vid gröna tester. Små steg.

---

## 📊 Mät framgång

### Bra indikatorer:
✅ Kan förklara varje rad kod
✅ Tester är gröna
✅ Commits är små och frekventa
✅ Refactoring sker regelbundet
✅ Code review går snabbt
✅ Buggar hittas tidigt

### Dåliga indikatorer:
❌ "Det funkar men jag vet inte varför"
❌ Många failande tester
❌ Stora sällsynta commits
❌ Ingen refactoring
❌ Code review tar timmar
❌ Buggar i produktion

---

## 🎯 Sammanfattning

1. **Förtydliga** - EN mening
2. **Minimispec** - 3-5 rader
3. **Intent Prompting** - Förklara VARFÖR, be om design
4. **AI som arkitekt** - Få alternativ, välj själv
5. **Små kodblock** - TDD first, en funktion
6. **Testa/Debugga** - Grön innan vidare
7. **Refaktorera + Commit** - Quality check, sedan commit

**Mantra:**
> "TDD first, sen commit. Små steg, stora system."

---

**Nästa steg:**
- Använd [Prompt Templates](PROMPTS.md)
- Granska med [Quality Checklist](QUALITY-CHECKLIST.md)
- Studera [Principer](PRINCIPLES.md)
- Utforska [Demo-exempel](../demos/TodoFilter/)

---

**Skapad av Marcus Ackre Medina**
📚 [AI-Driven Development på GitHub](https://github.com/MarcusMedina/ai-driven-workflow)
