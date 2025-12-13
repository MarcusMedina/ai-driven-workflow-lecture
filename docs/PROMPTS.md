# AI Prompt Templates
> Copy-paste ready prompts för professionell AI-driven utveckling

---

## 📋 Basic Implementation Prompt

```
## Problem
[Beskriv vad du försöker lösa i EN mening]

## Kontext
- Språk/ramverk: [C#, Java, Python, React...]
- Befintlig kod: [Vad finns redan?]
- Begränsningar: [Inga externa libs, måste vara async, etc]

## Förfrågan
VIKTIGT: Vi planerar nu, inget kodande än.

Jag vill att du FÖRST förklarar hur du skulle strukturera lösningen.
Diskutera olika alternativ och deras trade-offs.

SEDAN (när jag säger till) implementera med TDD:
1. Skriv tester först (normala flödet + edge cases)
2. Implementera funktionen
3. Se till att alla tester är gröna
4. Föreslå förbättringar

## Kvalitetskrav
- Små metoder (SRP - en uppgift per metod)
- Ingen upprepad logik (DRY)
- Självförklarande kod (KISS)
- Tydlig separation of concerns (SoC)

## Klar när
- [ ] Alla tester gröna
- [ ] Kod refactored och läsbar
- [ ] Redo att commita
```

---

## 🧪 TDD-First Prompt

```
NU KODAR VI (planeringen är klar).

Implementera [FUNKTION/KLASS] med TDD-approach:

**Steg 1: Tester först**
Skriv tester för:
- Normala flödet: [beskriv förväntat beteende]
- Edge case 1: [tom input, null, etc]
- Edge case 2: [stora dataset, specialtecken, etc]
- Error case: [vad ska hända vid fel?]

**Steg 2: Implementation**
Implementera funktionen så att alla tester blir gröna.
Fokusera på:
- En metod = en uppgift (SRP)
- Självförklarande namn
- Minimal komplexitet (KISS)

**Steg 3: Refaktorering**
Granska koden:
- Följer den SRP? (En uppgift per metod)
- Är namnen självförklarande?
- Finns upprepad logik som kan brytas ut? (DRY)
- Är ansvaren tydligt separerade? (SoC)

**Steg 4: Dokumentation**
Lägg till:
- XML-kommentarer (C#/Java) eller docstrings (Python)
- Förklara VARFÖR, inte VAD
- Dokumentera edge cases som hanteras
- Eventuella exceptions
```

---

## 🏗️ Architecture Comparison Prompt

```
För denna vertical slice: [BESKRIV SLICE/FEATURE]

VIKTIGT: Vi planerar arkitektur nu, inget kodande än.
Bara jämföra approaches.

Ge mig TRE arkitektur-approaches:

1. **Enklast möjliga** (prototyp)
   - Hur ser koden ut?
   - Komplexitet?
   - När är detta tillräckligt bra?
   - Trade-offs?

2. **Balanserad** (production-ready, säker)
   - Vilka klasser/moduler behövs?
   - Hur separeras ansvaren? (SoC)
   - Säkerhet (validering, sanitering)?
   - Hur testar man det?

3. **Enterprise** (fullt utbyggd)
   - Vilka patterns används?
   - Hur skalar den?
   - Är det overkill för mitt use case?
   - Komplexitetskostnad?

För varje approach, förklara:
- Komplexitet
- Säkerhet (validering, sanitering)
- Performance
- Maintainbarhet
- Trade-offs

Rekommendera baserat på: [ditt scenario]

**Exempel scenario:**
- Projektstorlek: litet team
- Fas: MVP-fas
- Krav: dataintegritet viktigt, snabb time-to-market
```

---

## 🔍 Code Review Prompt

```
Granska denna kod mot följande checklista och förklara hur den presterar på varje punkt:

**1. Tester**
- Finns det tester?
- Täcker de normala flödet + edge cases?
- Är testerna själva läsbara och maintainbara?

**2. Läsbarhet**
- Är metod/variabel-namn självförklarande?
- Följer varje metod SRP? (gör EN sak)
- Finns det "clever" kod som borde förenklas? (KISS)
- Kan koden förstås utan kommentarer?

**3. Struktur**
- Är ansvaren tydligt separerade? (SoC)
- Finns upprepad logik? (DRY)
- Är beroenden tydliga och få?
- Passar abstraktionsnivåerna?

**4. Dokumentation**
- Behövs kommentarer för att förstå koden?
- Förklarar kommentarer VARFÖR, inte VAD?
- Är komplexa delar förklarade?

**5. Förbättringsförslag**
- Vad är koddens största svagheter?
- Hur skulle du refaktorera den?
- Vilka ändringar ger störst värde?
- Finns säkerhetsrisker?

[KLISTRA IN KOD HÄR]
```

---

## ♻️ Refactoring Prompt

```
Jag har kod som fungerar, men den behöver förbättras.

**Nuvarande problem:**
[Lång metod / upprepning / svår att testa / etc]

**Mål med refactoring:**
[Bättre läsbarhet / enklare att testa / reducera komplexitet]

**Refaktorera med fokus på:**

1. **SRP** - Bryt ut metoder som gör mer än EN sak
2. **DRY** - Eliminera upprepad logik
3. **SoC** - Separera olika ansvarsområden
4. **KISS** - Förenkla komplex logik
5. **Testbarhet** - Gör koden lättare att testa

**Process:**
1. Föreslå refactoring-steg (inte allt på en gång - små steg!)
2. Visa före/efter för varje steg
3. Förklara VARFÖR ändringen förbättrar koden
4. Behåll samma funktionalitet (tester ska vara gröna)
5. Föreslå nya tester om beteendet ändras

[KLISTRA IN KOD HÄR]
```

---

## 🐛 Debug & Explain Prompt

```
Jag har en bugg/oväntat beteende:

**Vad jag förväntade mig:**
[Beskriv förväntat resultat]

**Vad som faktiskt händer:**
[Beskriv faktiskt resultat + felmeddelanden]

**Min kod:**
[KLISTRA IN RELEVANT KOD]

**Testdata (om relevant):**
[Input-data som orsakar buggen]

**Förklara:**
1. VAD som händer (steg-för-steg genom koden)
2. VARFÖR det händer (root cause, inte bara symptom)
3. HUR man fixar det (med förklaring, inte bara kod)
4. Finns det ANDRA potentiella buggar i samma kod?
5. Hur kan jag förhindra liknande buggar i framtiden?

Skriv förklaringen så att jag LÄRA mig något, inte bara får en fix.
Föreslå också tester som skulle ha fångat denna bugg.
```

---

## 📊 Micro-MVP / Vertical Slices Prompt

```
Jag vill bygga [STORT FEATURE/SYSTEM].

VIKTIGT: Vi planerar nu, inget kodande än.
Bara brainstorming och nedbrytning.

Bryt ner i vertical slices där varje slice:
- Är en KOMPLETT user journey (end-to-end)
- Ger värde separat
- Kan byggas på 15-25 minuter
- Är testbar och deploybar
- Respekterar dataintegritet

Ge mig de 3 viktigaste slicesen först, prioriterade efter värde.

**För varje slice, beskriv:**
- Input/Output
- Definition of Done
- Edge cases som måste hanteras
- Säkerhetsaspekter (validering, sanitering)

**Mitt feature:**
[Beskriv stort feature här]

**Begränsningar:**
[Tid, teknologi, kunskap, etc]

**Exempel:**
Feature: Todo-hantering
→ Slice 1: Skapa todo (Input → Validera → Spara → Visa)
→ Slice 2: Visa todos (Hämta → Sortera → Rendera)
→ Slice 3: Filtrera todos (Input filter → Applicera → Visa resultat)
```

---

## 🎯 Intent Prompting Template

```
## Vad jag försöker uppnå
[Beskriv målet, inte lösningen - VARFÖR behöver jag det här?]

## Kontext
- Projektet handlar om: [domain/problem space]
- Jag har redan: [befintlig kod/struktur]
- Begränsningar: [tekniska/tid/kunskap]
- Målgrupp: [vem ska använda detta?]

## Hur jag tänker (initialt)
[Din första idé - be AI utmana/förbättra den]

## Förfrågan
FÖRST: Förklara hur du skulle närma dig problemet
- Vilka alternativ finns?
- Vilka trade-offs har varje alternativ?
- Vad rekommenderar du och VARFÖR?

SEDAN: När vi valt approach - implementera med TDD
- Tester först
- Små steg
- Refactorera efter varje steg

## Kvalitetskrav
- [ ] SRP (varje metod gör EN sak)
- [ ] DRY (ingen upprepad logik)
- [ ] SoC (tydlig separation av ansvar)
- [ ] KISS (så enkelt som möjligt)
- [ ] Självförklarande kod
- [ ] Vältestad

## Success criteria
[Hur vet jag att det här är klart och BRA?]
```

---

## 💡 Tips för effektiva prompts

### DO ✅
- **Var specifik** om kontext (språk, ramverk, begränsningar)
- **Be om FÖRKLARING först**, kod sen
- **Inkludera kvalitetskrav** (SRP, DRY, SoC, KISS)
- **Använd TDD** som standard
- **Fråga efter alternativ**, inte EN lösning
- **Bryt ner stora problem** till micro-MVPs
- **Be om trade-off-analys** när du väljer approach
- **Verifiera att du förstår** innan du commitar

### DON'T ❌
- **Prompta "bygg hela appen"** - för stort
- **Acceptera första lösningen blint** - jämför alternativ
- **Glöm edge cases** - de kommer tillbaka senare
- **Skippa refactoring-steget** - "fungerar" ≠ "bra"
- **Commit kod du inte förstår** - du äger den sen
- **Ignorera varningar från AI** - den ser ofta mer än du
- **Försök vara "clever"** - KISS slår clever varje gång

---

## 🎯 The 2-Minute Rule

Innan du börjar koda:
> **Kan du förklara vad du ska göra på 2 minuter?**

- **JA** → Fortsätt, du förstår problemet
- **NEJ** → Bryt ner mer / be AI förklara

**Prompt för 2-minute test:**
```
Förklara för mig som om jag vore en kollega vad den här koden gör och
varför vi löste det så. Max 2 minuter.

Om förklaringen blir längre = koden är för komplex eller jag förstår inte.
```

Om AI behöver längre tid = koden behöver förenklas ELLER du behöver förstå bättre.

---

## 📝 The TDD Mantra (för AI-prompts)

```
"Implementera [FEATURE] enligt denna ordning:

1. Tester först (red)
2. Implementation (green)
3. Refaktorera (clean)
4. Commit när alla tester är gröna

Förklara vad varje test verifierar och varför."
```

Detta tvingar AI att:
- Tänka igenom edge cases INNAN kodning
- Skapa verifierbar kod
- Producera maintainbar kod
- Ge dig förståelse (genom förklaringar)

---

## 🔄 The Micro-Step Pattern

```
Jag vill implementera [FEATURE].

Steg 1: Ge mig DEN MINSTA funktionen som ger värde
Steg 2: Skriv tester för denna funktion
Steg 3: Implementera
Steg 4: Refactorera
Steg 5: Vad är NÄSTA minsta steg?

Repetera tills feature är klar.
Varje steg ska vara committable.
```

---

## 🚀 Advanced: The Architecture Dialogue

```
Jag ska bygga [SYSTEM/FEATURE].

VIKTIGT: Vi planerar nu, inget kodande än.
Bara arkitektur-diskussion och nedbrytning.

Låt oss ha en arkitektur-dialog:

1. Jag beskriver vad jag vill uppnå: [beskriv]
2. Du ställer frågor om krav jag kanske missat
3. Du föreslår 2-3 alternativa arkitekturer
4. Vi diskuterar trade-offs
5. Jag väljer approach
6. Du bryter ner i micro-MVPs
7. Vi börjar implementera första steget (då säger jag "NU KODAR VI")

Börja med att ställa dina frågor.
```

Detta skapar ett **samarbete** istället för "gör åt mig".

---

**Skapad av Marcus Ackre Medina**
📚 Mer resurser: [AI-Driven Development på GitHub](https://github.com/MarcusMedina/ai-driven-workflow)
