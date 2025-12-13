---
marp: true
theme: uncover
size: 16:9
paginate: true
style: |
  section {
    background-color: #1a1a1a;
    color: #e1e1e1;
    font-size: 22px;
    padding: 40px;
  }
  h1 {
    color: #4a9eff;
    font-size: 2.2em;
  }
  h2 {
    color: #64b5f6;
    font-size: 1.6em;
  }
  h3 {
    color: #90caf9;
    font-size: 1.3em;
  }
  code {
    background: #2d2d2d;
    color: #a8e063;
    font-size: 0.8em;
    padding: 2px 6px;
    border-radius: 3px;
  }
  pre {
    background: #2d2d2d;
    padding: 15px;
    border-radius: 8px;
    font-size: 0.7em;
  }
  blockquote {
    border-left: 4px solid #4a9eff;
    padding-left: 20px;
    font-style: italic;
    color: #b0b0b0;
  }
  strong {
    color: #ffd54f;
  }
  ul, ol {
    font-size: 0.9em;
  }
  .columns {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    gap: 20px;
  }
---

<!-- _class: lead -->

# 🎄 AI-Driven Development Workflow 🎄

**En praktisk guide för att bli grym tillsammans med AI**
*(utan att bli Grinchen som stal julafton)*

Marcus Ackre Medina
YH Campus Mölndal
2024-12-15 ☃️

---

## Vem är jag?

**Marcus Ackre Medina**

- Programmeringslärare @ Campus Mölndal
- 25+ år som systemutvecklare
- Älskar ren kod, TDD, och att bygga rätt från början
- Fortfarande inte sur på AI (än) 😄

**Idag pratar vi inte om verktyg.**
**Vi pratar om hur ni använder det ni lärde er igår.**

---

<!-- _class: lead -->

# 0. Intro

_5 minuter - jag lovar att hålla tiden_ ⏰

---

# Kort och gott

> "It's not what you've got, it's how you use it."

Det gäller även LLM:er och julklappar.

Vilken modell du använder spelar mindre roll än hur du promptar.
Tydlig struktur, principer som TDD, SRP och DRY, och bra krav ger bra resultat – oavsett modell.

*(Och nej, det här är inte ännu en "använd AI-verktyg X"-presentation)* 🎁

---

## Vad ni fått hittills (dagens julklappar)

🎁 **Oscar** gav er verktygen (Cursor, Agents, smart prompting)
🎁 **Andrija** lärde er tänka kritiskt (värde vs hype)

**Nu:** Jag ger er workflow-strukturen

> "Oscar visade er motorsågen. Andrija lärde er att inte såga av benet.
> Jag visar hur man faktiskt bygger möbler (som inte kollapsar)." 🪑

---

## Helgens lärande - helheten

<div class="columns">

**Oscar 🛠️**
Verktyg + Prompting
_"Hur man kör bilen"_

**Andrija 🧠**
Värde vs Hype
_"Vart man ska köra"_

**Marcus 🗺️**
Workflow + Struktur
_"Färdplanen (med GPS)"_

</div>

**Tillsammans = Professionell AI-driven utveckling**

---

## Varför är vi här? (förutom kaffet)

Ni har kört hårt hela helgen:
- ✅ Lärt er verktyg
- ✅ Förstått affärsvärde
- ✅ Byggt grejer

**Men** - hur många har:
- Frågat AI samma sak 5 gånger? 🙋
- Fått kod som "funkar" men känns rörig? 🙋
- Fastnat för att projektet blev för stort? 🙋

---

## Dagens mål

**FIXA DET. FÖR GOTT.**

Ni ska lämna idag med:
- ✅ En repeterbar process
- ✅ Konkreta prompts att använda
- ✅ Självförtroende att bygga hållbart

**Sen bygger ni era MVPer** (och de ska bli bra!)

---

## Min tes

> **Det är workflow, inte verktyg, som avgör hur långt du kommer.**

Cursor är bra. Claude är bra. ChatGPT är bra.
*(Ja, även Copilot har sina stunder)* 😏

**Men det är strukturen som gör skillnaden mellan:**
- "Fick det att funka" vs "Byggde något maintainbart"
- "En feature" vs "Ett helt system"

---

<!-- _class: lead -->

# 1. Problemet
*(eller: varför era projekt känns som en julgran utan fäste)*

_5 minuter_

---

## Vad händer ofta?

🔄 Hoppar mellan olika AI-verktyg
📦 Projekten blir för stora direkt
🏗️ Svårt att veta när något är "klart"
🤔 Osäkerhet kring kodkvalitet
🎄 "Funkar" men ingen vågar röra koden

**Resultat:** Kod som funkar, men känns osäker att jobba vidare med.

---

## Vanliga utmaningar

❌ Promptar för mycket på en gång
❌ "Bygg hela appen" direkt
❌ Svårt att veta om AI-koden är bra
❌ Ingen tydlig process att följa
❌ Osäker på när man ska commit

**Känner ni igen er?**

*(Det är okej, vi har alla varit där. Förra veckan. Flera gånger.)* 😅

---

## Här är grejen

> **AI är en förstärkare.**

Den förstärker **bra arbetssätt**.
Men den förstärker också **osäkra**.

**Strukturerat arbete** → Snabbt OCH hållbart ✅
**Ad-hoc arbete** → Snabbt men rörigt ⚠️

**Vi ska göra det strukturerat. Idag.**

---

<!-- _class: lead -->

# 2. AI som juniorer
## Du som arkitekt
*(Santa's workshop-modellen)*

_10 minuter_

---

## Mindset-skiftet

<div class="columns">

**Gammalt tänk**
```
Jag
 ↓
AI (expert)
 ↓
"Bygg åt mig"
```
*Resultat: Svart magi, osäkerhet*

**Nytt tänk**
```
Jag (arkitekt)
 ↓
AI + AI + AI (juniorer)
 ↓
"Ge mig alternativ"
```
*Resultat: Kontroll, förståelse*

</div>

---

## Tre sanningar

### 1. AI kan producera flera lösningar

**DU väljer rätt**

```
Prompt: "Ge mig tre sätt att hantera input:
- Ett enkelt
- Ett robust
- Ett över-engineerat (enterprise-style)

Förklara när jag ska välja vilket."
```

**Oscar visade prompting - nu använder vi det för att JÄMFÖRA, inte bara generera.**

---

## Tre sanningar (forts.)

### 2. AI kan skriva kod

**DU formar systemet**

| AI:s styrka    | Din styrka       |
| -------------- | ---------------- |
| Syntax         | Arkitektur       |
| Boilerplate    | Design decisions |
| Implementation | Direction        |
| Speed          | Quality          |

**AI är snabb. Du är klok. Tillsammans = farligt bra.** 🚀

---

## Tre sanningar (forts.)

### 3. Du är den som behövs

> **AI kan göra dig 10x snabbare.**
> **Men bara om du vet:**
>
> - **VAD** som ska byggas
> - **VARFÖR** det ska byggas så
> - **NÄR** något är tillräckligt bra

**Arkitektur och kontext → därför behövs du alltid**

*(Även när AI:n säger "jag kan allt")* 😎

---

## Så hur jobbar man då?

Oscar visade hur man promptar.
Andrija visade hur man tänker kritiskt.

Nu sätter vi ihop det till en **loop**.
En **strukturerad process**.

När ni kan den här, kan ni bygga **vad som helst**.
**Repeterbart. Skalbart. Utan panik.**

---

<!-- _class: lead -->

# 3. The AI-Driven Dev Loop
*(Julens workflow-present)* 🎁

_10 minuter_

---

## De 7 stegen

```
1. Förtydliga
    ↓
2. Minimispec
    ↓
3. Intent Prompting
    ↓
4. AI som arkitekt
    ↓
5. Små kodblock (TDD)
    ↓
6. Testa/Debugga
    ↓
7. Refaktorera + Commit
```

**Varje iteration: 5-15 minuter**
**Repeterbart. Varje. Gång. Som en god jul-tradition.** 🎄

---

## Steg 1: Förtydliga

**EN mening. Det är allt.**

❌ "Jag vill ha en todo-app"
✅ "Användare ska kunna filtrera todos efter status"

❌ "Gör det snabbare"
✅ "API-anrop tar >2s, ska ta <500ms"

**Om du inte kan förklara det enkelt, har du inte förstått det.**

*(Albert Einstein sa nåt liknande. Smart kille.)* 🧠

---

## The 2-Minute Rule

> Kan du förklara problemet på 2 minuter?

**JA** → Fortsätt, du är redo
**NEJ** → Du förstår inte problemet tillräckligt bra

**Ingen kodning innan du förstår.**

*(Detta gäller även när PM:en stressar.)*

---

## Steg 2: Minimispec

3-5 rader. Inte mer.

```
Problem: Filtrera todos efter status
Input: Lista av Todo + status-sträng
Output: Filtrerad lista
Edge cases:
  - Tom lista → returnera tom lista
  - Case-insensitive ("Done" = "done")
```

**Det tar 2 minuter. Gör det.**

*(Ja, även när det känns onödigt. SÄRSKILT då.)* ⏱️

---

## Steg 3: Intent Prompting

**Inte bara "VAD", utan "VARFÖR och HUR"**

❌ Basic prompt:
```
"Skriv en metod som filtrerar todos"
```

✅ Intent prompt (som Oscar visade):
```
"Jag behöver filtrera objekt efter status-fält.
Jag vill ha en ren funktion utan sidoeffekter.

INNAN du kodar: Förklara hur du skulle
strukturera det. Vilka alternativ finns?"
```

---

## Varför Intent Prompting?

AI som får **"förklara först"** tvingas:

- Tänka igenom problemet
- Överväga edge cases
- Föreslå strukturerade lösningar

**Du får alternativ. Du väljer. Du är arkitekten.**

**Detta är Oscars teaching i praktiken - vi använder det systematiskt i varje iteration.**

---

## Steg 4: AI som arkitekt

```
"Ge mig TRE sätt att lösa det här:

1. Enklast möjliga (KISS)
2. Balanserat (production-ready)
3. Enterprise-nivå (kanske overkill?)

Förklara trade-offs och rekommendera."
```

**AI föreslår. DU fattar beslut.**
**Andrijas kritiska tänkande applicerat på kod.**

---

## Exempel: FilterByStatus

**Enklast:**
```csharp
todos.Where(t => t.Status == status).ToList();
```
Pro: En rad, tydlig. Con: Case-sensitive, ingen null-check

**Balanserad:**
```csharp
if (todos == null) return new List<Todo>();
return todos.Where(t =>
  t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)
).ToList();
```
Pro: Null-safe, case-insensitive. Con: Lite mer kod

**DU väljer baserat på kontext. Inte AI. Inte PM. DU.**

---

## Steg 5: Små kodblock (TDD)

**Oscar visade TDD-prompting - nu ser ni hur det blir del av loopen.**

```
"Implementera FilterByStatus med TDD:

1. Skriv tester först:
   - Normal case (happy path som vi inte kallar det 😏)
   - Tom lista
   - Ingen match
   - Case-insensitive

2. Implementera
3. Kör tester
4. Refactorera"
```

---

## Varför TDD i varje iteration?

✅ AI tvingas tänka igenom edge cases
✅ Testerna KÖR direkt - ingen gissning
✅ Du vet att koden fungerar
✅ Refactoring blir trygg

> **"TDD first, sen commit"**
> Varje steg MÅSTE ha gröna tester.

**Detta är inte nytt - Oscar lärde er detta.**
**Nu ser ni hur det blir en naturlig del av varje liten iteration.**

---

## Steg 6: Testa och debugga

**När testerna är röda:**

```
"Här är testet som failar: [paste]
Här är felet: [paste error]

Förklara:
1. VAD som händer (steg för steg)
2. VARFÖR det händer (root cause)
3. HUR man fixar det (inte bara "gör så här")

Jag vill FÖRSTÅ, inte bara få en fix."
```

**Smart prompting för förståelse, inte bara fixes.**

---

## Steg 7: Refaktorera + Commit

**Innan commit - checklistan:**

✅ Testerna OK?
✅ Koden läsbar? (SRP, DRY, KISS)
✅ Strukturen tydlig? (SoC)
✅ Nästa person förstår? (du om 6 månader)
✅ Steget säkrat?

**Alla gröna? → Commit!**

*(Nej, inte "fix stuff". Riktiga commit messages.)*

---

## Commit message format

```
[VAD] - [VARFÖR]

✅ Add TodoFilter with case-insensitive matching
   - Users expect search to work regardless of case

✅ Refactor validation logic for clarity
   - Method was doing 3 things, now does 1

❌ Fixed stuff
❌ Changes
❌ asdf
```

**Små commits. Ofta. Med mening.**

---

<!-- _class: lead -->

# 4. Demo i Claude Code
*(Jag bygger en Todo-filter live. Vad kan gå fel?)* 😅

_15 minuter - LIVE_

---

## Vad vi ska bygga

**TodoFilter** - enkel men komplett

- Filtrera todos efter status
- Följer hela 7-stegs loopen
- TDD från början (som Oscar visade)
- Commit vid gröna tester

**Ni ser PROCESSEN, inte magin.**

*(Och om det går fel får ni se debuggingen också. Win-win.)* 🎯

---

## Demo-flöde

1. **Minimispec** (2 min) - Förtydliga först
2. **Intent prompt** (2 min) - Förklara varför
3. **Be om alternativ** (3 min) - AI som arkitekt
4. **Välj approach** (1 min) - DU bestämmer
5. **TDD - tester först** (3 min) - Oscars metod
6. **Implementation** (2 min) - Kod som följer testerna
7. **Refactorera + commit** (2 min) - Quality check

**LIVE CODING - följ med!**

---

<!-- Här kör du live demo -->

## Demo Notes (för dig, Marcus)

**Öppna Claude Code**
```bash
cd demos/TodoFilter
```

**Börja med spec.md:**
```
Problem: Filtrera todos efter status
Input: List<Todo>, string status
Output: Filtrerad lista
Edge: Tom lista, case-insensitive
```

**SÄG:** "Det här tar 90 sekunder. Varje gång. Ingen gissning."

---

## Demo Notes (forts.)

**Intent Prompt:**

```
Jag vill bygga TodoFilter.FilterByStatus().

Använd TDD (som Oscar visade):
- Tester för: tom lista, match, ingen match, case
- Sen implementera

Förklara hur du strukturerar det först.
```

**Låt AI förklara → Välj approach → Koda → Commit**

**PÅPEKA:** "Detta är exakt den loop ni kan upprepa för varje liten feature. Imorgon. På måndag. Alltid."

---

<!-- _class: lead -->

# 5. Micro-MVP Thinking
*(Eller: Hur man bygger en snögubbe, ett snöboll i taget)* ⛄

_10 minuter_

---

## Från feature till steg

Andrija lärde er bedöma värde.
Nu ska vi bryta ner värdet i byggbara steg.

**Micro-MVP = minsta körbara värde**

❌ "Bygg en todo-app"
✅ "Skapa en funktion som lägger till todo"

❌ "Gör ett API"
✅ "Gör EN endpoint som returnerar JSON"

---

## Micro-MVP definition

**1 funktion = 1 resultat = 5-10 minuter**

Varför?

- AI älskar tydlighet
- Snabba vinnar → motivation → julkänsla 🎄
- Små block → lätt debugga
- Struktur växer organiskt
- **Du kan följa varje steg** (ingen svart magi)

---

## Exempel: E-commerce

❌ **Stort:** "Bygg e-commerce site"

✅ **Micro-MVPs:**

1. Product class med properties
2. ProductList som håller products
3. AddProduct() metod
4. GetProductById() metod
5. FilterByCategory() metod

**Varje steg = committable, körbart, testbart**
**Varje steg = en iteration av 7-stegs loopen**

---

## Prompt för nedbrytning

```
"Jag vill bygga [STORT FEATURE].

Bryt ner till micro-MVPs där varje steg:
- 1 funktion
- 1 resultat
- <10 minuter
- Testbart
- Ger värde (för debugging eller användare)

Lista de 5 första stegen."
```

**AI hjälper dig planera - DU väljer ordning.**

---

## Key takeaway

> **Små framsteg du FÖRSTÅR**
> **slår**
> **stora framsteg du KOPIERAR**

**Bygg smart. Bygg litet. Bygg ofta.**

**Oscar gav er verktygen.**
**Andrija gav er tankesättet.**
**Nu har ni processen.**

**= Ni är redo att bygga riktiga MVPer. Idag.** 🚀

---

<!-- _class: lead -->

# 6. Sammanfattning + Takeaways
*(Eller: Julklappsöppning av kunskap)* 🎁

_5 minuter_

---

## De 7 stegen (igen)

1. **Förtydliga** - EN mening
2. **Minimispec** - 3-5 rader
3. **Intent Prompting** - Förklara VARFÖR
4. **AI som arkitekt** - Få alternativ, välj själv
5. **Små kodblock (TDD)** - Tester först
6. **Testa/Debugga** - Grönt innan vidare
7. **Refaktorera + Commit** - Quality check

**En loop. Repeterbar. Skalbar. Som en god julrutin.** 🎄

---

## Mantran att komma ihåg

> **"TDD first, sen commit"**

> **"Små steg, stora system"**

> **"Fungerar ≠ Klart"**

> **"AI föreslår. Jag bestämmer."**

*(Skriv ner dem. Sätt på väggen. Tatuera om ni vill. Jag dömer inte.)* 😄

---

## När ni bygger era MVPer IDAG

**Använd denna loop:**

1. Skriv ner problemet i EN mening
2. Gör en 3-raders spec
3. Använd intent prompting (som Oscar visade)
4. Be om 3 alternativ, välj smart (som Andrija lärde)
5. Bygg EN sak i taget (TDD)
6. Granska och refaktorera
7. Commit vid gröna tester

**Upprepa. Varje. Feature.**

---

## Vad ni har lärt er denna helg

**Oscar 🛠️:** Verktyg + Smart prompting
**Andrija 🧠:** Kritiskt tänkande om AI-värde
**Marcus 🗺️:** Strukturerad, repeterbar process

**= Professionell AI-driven utveckling**

**Inte bara snabbt.**
**Snabbt OCH hållbart.**

**Som en bra julklapp - användbar länge.** 🎁

---

## Resurser ni får med er

📚 **GitHub-repo:**

- WORKFLOW.md (7-stegs loop i detalj)
- PROMPTS.md (copy-paste templates)
- QUALITY-CHECKLIST.md (använd innan commit)
- PRINCIPLES.md (SRP, DRY, SoC, KISS med exempel)
- TodoFilter demo (komplett C#, JavaScript, PHP)

**github.com/MarcusMedina/ai-driven-workflow-lecture**

*(Allt gratis. Inga julkort behövs.)* 😊

---

## Avslutning

> **"AI gör dig snabb.**
> **Men det är strukturen som gör dig farlig."**

**Ni är arkitekterna.**
**AI är bara byggnadsarbetarna.**

**Oscar + Andrija + denna loop = ni är redo.**

**Nu: Gå och bygg något awesome. Jag tror på er!** 🚀

---

<!-- _class: lead -->

# Frågor?

**Och kom ihåg:**
**Du äger koden. Inte AI:n.**

*(Även när den påstår annat.)*

---

<!-- _class: lead -->

# Tack! 🎄

Marcus Ackre Medina
**marcus@campusmolndal.se**
**github.com/MarcusMedina**

**God jul och lycka till med era projekt!** ☃️

**Stort tack till Oscar och Andrija för fantastiska föreläsningar!**
*(Ni satte ribban högt - no pressure.)* 😅
