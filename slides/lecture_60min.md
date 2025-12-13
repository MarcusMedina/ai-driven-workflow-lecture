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

# AI-Driven Development Workflow

**En praktisk guide för att bli grym tillsammans med AI**

Marcus Ackre Medina
YH Campus Mölndal
2024-12-13

---

## Vem är jag?

**Marcus Ackre Medina**

- Programmeringslärare @ Campus Mölndal
- 25+ år som systemutvecklare
- Älskar ren kod, TDD, och att bygga rätt från början

**Idag pratar vi inte om verktyg.**
**Vi pratar om hur du tänker.**

---

<!-- _class: lead -->

# 0. Intro

_5 minuter_

---

# Kort och gott

“It’s not what you’ve got, it’s how you use it.”

Det gäller även LLM:er.
Vilken modell du använder spelar mindre roll än hur du promptar.
Tydlig struktur, principer som TDD, SRP och DRY, och bra krav ger bra resultat – oavsett modell.

---

## Varför är vi här?

Ni har kört hårt hela helgen:

- **Oscar** visade verktyg (Cursor + Agent)
- **Andrija** lärde er ifrågasätta hype
- Ni har **byggt grejer**

Nu ska jag visa något viktigt:
**Hur man faktiskt jobbar med AI som utvecklare**

---

## Min tes

> **Det är workflow, inte verktyg, som avgör hur långt du kommer.**

Cursor är bra. Claude är bra. ChatGPT är bra.

**Men ingen av dem gör dig till bra utvecklare.**
**Det gör ditt tankesätt.**

---

## Snabb check

Hur många har idag:

- Frågat AI samma sak 5 gånger? 🙋
- Fått kod som "funkar" men känns rörig? 🙋
- Fastnat för att projektet blev för stort? 🙋

**Känner ni igen er?**

Det är EXAKT det vi ska fixa. Nu.

---

<!-- _class: lead -->

# 1. Problemet idag

_5 minuter_

---

## Vad händer nu?

🔄 Hoppar mellan Cursor, Lovable, ChatGPT, Claude, Bolt...
🎯 Men ingen lär er **hur man tänker**
📦 Projekten blir för stora direkt
🏗️ Ingen arkitektur, bara "AI säg åt mig"

**Resultat:** Kaos. Kod ni inte förstår. Buggar överallt.

---

## Vanliga misstag

❌ Prompta för mycket på en gång
❌ "Bygg hela appen" direkt
❌ Ingen struktur innan kodning
❌ Kopiera AI-kod blint
❌ Ingen reflektion över kvalitet

**Känner ni igen er?**

---

## Här är grejen

> **AI är en förstärkare.**

Den förstärker **bra arbetssätt**.
Men den förstärker också **dåliga**.

**Jobbar du kaotiskt** → Du blir snabbt kaotisk
**Jobbar du strukturerat** → Du blir snabbt grym

---

<!-- _class: lead -->

# 2. AI som juniorer

## Du som arkitekt

_10 minuter_

---

## Mindset-skiftet

<div class="columns">

**Gamla tänket**

```
Jag
 ↓
AI (expert)
 ↓
"Bygg åt mig"
```

**Nya tänket**

```
Jag (arkitekt)
 ↓
AI + AI + AI (juniorer)
 ↓
"Ge mig 3 lösningar"
```

</div>

---

## Tre sanningar

### 1. AI kan producera 10 lösningar

**DU väljer rätt**

```
Prompt: "Ge mig tre sätt att hantera input:
- Ett enkelt
- Ett robust
- Ett över-engineerat

Förklara när jag ska välja vilket."
```

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

---

## Tre sanningar (forts.)

### 3. Du är den som behövs

> **AI kan göra dig 10x snabbare.** > **Men bara om du vet:**
>
> - **VAD** som ska byggas
> - **VARFÖR** det ska byggas så
> - **NÄR** något är tillräckligt bra

**Arkitektur och kontext → därför behövs du alltid**

---

## Så hur jobbar man då?

Det finns en loop.
En **enkel loop**.

När ni kan den här, kan ni bygga **vad som helst**.

---

<!-- _class: lead -->

# 3. The AI-Driven Dev Loop

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

---

## Steg 1: Förtydliga

**EN mening. Det är allt.**

❌ "Jag vill ha en todo-app"
✅ "Användare ska kunna filtrera todos efter status"

❌ "Gör det snabbare"
✅ "API-anrop tar >2s, ska ta <500ms"

---

## The 2-Minute Rule

> Kan du förklara problemet på 2 minuter?

**JA** → Fortsätt
**NEJ** → Du förstår inte problemet tillräckligt bra

**Ingen kodning innan du förstår.**

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

---

## Steg 3: Intent Prompting

**Inte "VAD", utan "VARFÖR"**

❌ Traditionell prompt:

```
"Skriv en metod som filtrerar todos"
```

✅ Intent prompt:

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
- Föreslå clean solutions

**Du får alternativ. Du väljer. Du är arkitekten.**

---

## Steg 4: AI som arkitekt

```
"Ge mig TRE sätt att lösa det här:

1. Enklast möjliga
2. Balanserat (production-ready)
3. Enterprise-nivå

Förklara trade-offs och rekommendera."
```

**AI föreslår. DU fattar beslut.**

---

## Exempel: FilterByStatus

**Enklast:**

```csharp
todos.Where(t => t.Status == status).ToList();
```

Pro: En rad. Con: Case-sensitive, ingen null-check

**Balanserad:**

```csharp
if (todos == null) return new List<Todo>();
return todos.Where(t =>
  t.Status.Equals(status, StringComparison.OrdinalIgnoreCase)
).ToList();
```

Pro: Null-safe, case-insensitive. Con: Lite mer kod

---

## Steg 5: Små kodblock (TDD)

**En funktion i taget. Tester först.**

```
"Implementera FilterByStatus med TDD:

1. Skriv tester först:
   - Normal case
   - Tom lista
   - Ingen match
   - Case-insensitive

2. Implementera
3. Kör tester
4. Refactorera"
```

---

## Varför TDD med AI?

✅ AI tvingas tänka igenom edge cases
✅ Testerna KÖR direkt - ingen gissning
✅ Du vet att koden fungerar
✅ Refactoring blir trygg

> **"TDD first, sen commit"**
> Varje steg MÅSTE ha gröna tester.

---

## Steg 6: Testa och debugga

**När testerna är röda:**

```
"Här är testet som failar: [paste]
Här är felet: [paste error]

Förklara:
1. VAD som händer
2. VARFÖR det händer
3. HUR man fixar det

Jag vill FÖRSTÅ, inte bara få en fix."
```

---

## Steg 7: Refaktorera + Commit

**Innan commit - checklistan:**

✅ Testerna OK?
✅ Koden läsbar? (SRP, DRY, KISS)
✅ Strukturen tydlig? (SoC)
✅ Nästa person förstår?
✅ Steget säkrat?

**Alla gröna? → Commit!**

---

## Commit message format

```
[VAD] - [VARFÖR]

✅ Add TodoFilter with case-insensitive matching
   - Users expect search to work regardless of case

❌ Fixed stuff
❌ Changes
```

**Små commits. Ofta.**

---

<!-- _class: lead -->

# 4. Demo i Claude Code

_15 minuter - LIVE_

---

## Vad vi ska bygga

**TodoFilter** - enkel men komplett

- Filtrera todos efter status
- TDD från början
- Varje steg i loopen
- Commit vid gröna tester

**Ni ser PROCESSEN, inte magin.**

---

## Demo-flöde

1. **Minimispec** (2 min)
2. **Intent prompt** (2 min)
3. **Be om alternativ** (3 min)
4. **Välj approach** (1 min)
5. **TDD - tester först** (3 min)
6. **Implementation** (2 min)
7. **Refactorera + commit** (2 min)

**LIVE CODING - följ med!**

---

<!-- Här skulle du köra live demo, men i slidesen har vi placeholder -->

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

---

## Demo Notes (forts.)

**Intent Prompt:**

```
Jag vill bygga TodoFilter.FilterByStatus().

Använd TDD:
- Tester för: tom lista, match, ingen match, case
- Sen implementera

Förklara hur du strukturerar det först.
```

**Låt AI förklara → Välj approach → Koda**

---

<!-- _class: lead -->

# 5. Micro-MVP Thinking

_10 minuter_

---

## Största misstaget

❌ "Bygg en todo-app"
✅ "Skapa en funktion som lägger till todo"

❌ "Gör ett API"
✅ "Gör EN endpoint som returnerar JSON"

❌ "Fullstack-projekt"
✅ "Rendera en komponent"

---

## Micro-MVP definition

**1 funktion = 1 resultat = 5-10 minuter**

Varför?

- AI älskar tydlighet
- Snabba vinnar → motivation
- Små block → lätt debugga
- Struktur växer organiskt

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

---

## Prompt för nedbrytning

```
"Jag vill bygga [STORT FEATURE].

Bryt ner till micro-MVPs där varje steg:
- 1 funktion
- 1 resultat
- <10 minuter
- Testbart

Lista de 5 första stegen."
```

---

## Key takeaway

> **Små framsteg du FÖRSTÅR** > **slår** > **stora framsteg du KOPIERAR**

**Bygg smart. Bygg litet. Bygg ofta.**

---

<!-- _class: lead -->

# 6. Sammanfattning + Takeaways

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

---

## Mantran att komma ihåg

> **"TDD first, sen commit"**

> **"Små steg, stora system"**

> **"Fungerar ≠ Klart"**

> **"AI föreslår. Jag bestämmer."**

---

## När ni åker hem imorgon

**Gör detta i en vecka:**

1. Skriv ner problemet i EN mening
2. Gör en 3-raders spec
3. Fråga AI om design INNAN kod
4. Bygg EN sak i taget (TDD)
5. Granska och refaktorera
6. Commit vid gröna tester

**Ni kommer bli förvånade hur mycket bättre koden blir.**

---

## Resurser ni får med er

📚 **GitHub-repo:**

- WORKFLOW.md (7-stegs loop i detalj)
- PROMPTS.md (copy-paste templates)
- QUALITY-CHECKLIST.md (använd innan commit)
- PRINCIPLES.md (SRP, DRY, SoC, KISS)
- TodoFilter demo (komplett exempel)

**github.com/MarcusMedina/ai-driven-workflow**

---

## Avslutning

> **"AI gör dig snabb.** > **Men det är strukturen som gör dig farlig."**

**Ni är arkitekterna.**
**AI är bara byggnadsarbetarna.**

---

<!-- _class: lead -->

# Frågor?

**Och kom ihåg:**
**Du äger koden. Inte AI:n.**

---

<!-- _class: lead -->

# Tack!

Marcus Ackre Medina
**marcus@campusmolndal.se**
**github.com/MarcusMedina**

**Lycka till med era projekt!**
