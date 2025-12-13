# TodoFilter Demo

> Demonstrerar AI-Driven Development Workflow med TDD i C#

---

## 📋 Vad är det här?

Detta är ett praktiskt exempel på hur man använder **AI-Driven Development Workflow** för att bygga ren, testbar kod.

Projektet implementerar enkla filter-funktioner för todos, men fokus är på **PROCESSEN**, inte produkten.

---

## 🎯 Vad demonstreras?

### 1. TDD (Test-Driven Development)
✅ Tester skrivna FÖRST
✅ Implementation SEN
✅ Alla tester gröna innan commit

### 2. Clean Code Principles
✅ **SRP** - Varje metod gör EN sak
✅ **DRY** - Ingen upprepad logik
✅ **SoC** - Separation mellan Models och Services
✅ **KISS** - Så enkelt som möjligt

### 3. Edge Case Handling
✅ Null-hantering
✅ Tom lista-hantering
✅ Case-insensitive matching

---

## 🚀 Kör projektet

### Förutsättningar
- .NET 10.0 SDK

### Bygg projektet
```bash
dotnet build
```

### Kör alla tester
```bash
dotnet test
```

### Kör specifika tester
```bash
dotnet test --filter "FilterByStatus"
```

---

## 📁 Projektstruktur

```
TodoFilter/
├── src/
│   └── TodoFilter/
│       ├── Models/
│       │   └── Todo.cs                    # Data model
│       └── Services/
│           └── TodoFilterService.cs       # Business logic
└── tests/
    └── TodoFilter.Tests/
        └── Services/
            └── TodoFilterServiceTests.cs  # Unit tests
```

---

## 🧪 Vad testas?

### FilterByStatus
- ✅ Normal case (matching status)
- ✅ No matches
- ✅ Empty list
- ✅ Null list
- ✅ Null status
- ✅ Case-insensitive matching

### FilterByDateAfter
- ✅ Matching dates
- ✅ Null list

### FilterByTitleContains
- ✅ Matching titles
- ✅ Case-insensitive search
- ✅ Null inputs

**Total: 11 tester - alla gröna ✅**

---

## 💡 Hur detta följer AI-Driven Workflow

### 1. Förtydliga
> Problem: Filtrera todos efter status

### 2. Minimispec
```
Input: List<Todo>, string status
Output: Filtered list
Edge: Null lists, case-insensitive
```

### 3. Intent Prompting
```
"Jag behöver filtrera todos.
Funktionen ska vara:
- Null-safe
- Case-insensitive
- Enkel att testa

Förklara hur du skulle strukturera det."
```

### 4. AI som arkitekt
Fick tre alternativ:
1. Enkel LINQ (vald)
2. Fluent API
3. Specification pattern (overkill)

### 5. TDD
- Skrev tester FÖRST
- Implementerade SEN
- Alla gröna

### 6. Testa
```bash
dotnet test
# Passed: 11, Failed: 0
```

### 7. Refaktorera + Commit
- Code review mot checklist
- Alla principer följda
- Commit!

---

## 📊 Code Quality Checklist ✅

### 1. Testerna OK?
- [x] Tester först
- [x] Normal case + edge cases
- [x] Alla gröna

### 2. Koden läsbar?
- [x] Självförklarande namn (`FilterByStatus`, inte `DoStuff`)
- [x] SRP (varje metod gör EN sak)
- [x] Ingen upprepad logik (DRY)
- [x] KISS (enkel LINQ, inga fancy patterns)

### 3. Strukturen tydlig?
- [x] Models separerade från Services (SoC)
- [x] Inga beroenden mellan filter-metoder
- [x] Logisk filstruktur

### 4. Nästa person förstår?
- [x] XML-kommentarer på publika metoder
- [x] Kommentarer förklarar VARFÖR (case-insensitive)
- [x] README finns

### 5. Steget säkrat?
- [x] Alla tester gröna
- [x] Bygger utan errors
- [x] Redo för commit

---

## 🎓 Lärdomar

### DO ✅
- Skriv tester FÖRST
- Små metoder (en uppgift)
- Hantera edge cases (null, tom lista)
- Case-insensitive där användare förväntar det
- Kommentera VARFÖR, inte VAD

### DON'T ❌
- Implementera innan tester
- Glöm null-hantering
- Gör metoder som gör många saker
- Över-engineera (KISS!)

---

## 📚 Nästa steg

1. **Utforska koden** - Läs TodoFilterService.cs och testerna
2. **Lägg till egen feature** - Följ samma workflow
3. **Prova live** - Be AI hjälpa dig med nästa filter-metod

**Förslag på nästa micro-MVP:**
- `FilterByMultipleStatuses(todos, statuses[])`
- `FilterByDateRange(todos, start, end)`
- `CombineFilters(todos, filters[])`

Använd samma process:
1. Förtydliga
2. Minimispec
3. Intent prompt
4. Be om alternativ
5. TDD
6. Testa
7. Refactorera + commit

---

## 🔗 Relaterade resurser

- [WORKFLOW.md](../../docs/WORKFLOW.md) - Hela 7-stegs processen
- [PROMPTS.md](../../docs/PROMPTS.md) - Prompt templates
- [QUALITY-CHECKLIST.md](../../docs/QUALITY-CHECKLIST.md) - Använd innan commit
- [PRINCIPLES.md](../../docs/PRINCIPLES.md) - SRP, DRY, SoC, KISS

---

**Skapad som del av AI-Driven Development Workshop**
Marcus Ackre Medina @ YH Campus Mölndal
