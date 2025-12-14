# 🎬 Demo-manus: TodoFilter med TDD (live-kodning)

> **Tid:** ~15 minuter
> **Mål:** Visa hur TDD hittar buggar innan de når produktion
> **Budskap:** "Utan TDD → buggar i prod. Med TDD → hittas på 30 sekunder."

---

## 📋 Förberedelser (innan demon)

### Terminal-setup:
```bash
# Terminal 1: Projektkatalogen
cd /mnt/c/git/ailecture/demos/TodoFilter

# Terminal 2: Redo att köra tester
cd /mnt/c/git/ailecture/demos/TodoFilter/tests/TodoFilter.Tests
```

### Filer att ha öppna i editor:
1. `src/TodoFilter/Services/TodoFilterService.BUGGY.cs` (startar här)
2. `tests/TodoFilter.Tests/Services/TodoFilterServiceTests.BUGGY.cs`
3. `src/TodoFilter/Services/TodoFilterService.cs` (fixad version - visar senare)

---

## 🎭 DEMON - STEG FÖR STEG

### **Intro (30 sekunder)**
```
"Nu ska vi bygga en TodoFilter. Jag ska MEDVETET skriva dålig kod först,
för att visa vad som händer utan TDD kontra med TDD."

"Scenario: Användare vill filtrera sina todos efter status - 'done', 'pending', etc."
```

---

## **STEG 1: Förtydliga & Spec** (1 min)

**VAD DU SÄGER:**
```
"Först - vad ska vi bygga? Tydlig spec!"

Problem: Filtrera todos efter status
Input: Lista av todos + status-sträng ("done", "pending")
Output: Filtrerad lista

Edge cases som MÅSTE hanteras:
✅ Tom lista → returnera tom lista
✅ Null input → returnera tom lista
✅ Case-insensitive: "Done" = "done" = "DONE"

"Om vi missar någon av dessa → buggar i produktion!"
```

---

## **STEG 2: Backlog & Vertical Slices** (30 sekunder)

**VAD DU SÄGER:**
```
"Vi har tre potentiella features:
1. FilterByStatus ← DENNA FÖRST (viktigast!)
2. FilterByDateAfter (backlog)
3. FilterByTitleContains (backlog)

Vi kör slice 1 idag."
```

---

## **STEG 3: Arkitektur** (30 sekunder)

**VAD DU SÄGER:**
```
"Enkel arkitektur:
- TodoFilterService (en klass)
- FilterByStatus (en metod)
- Tester i TodoFilterServiceTests

KISS - håll det enkelt!"
```

---

## **STEG 4: TDD - Buggy Version** (5 minuter) ⚠️ **HUVUDNUMRET!**

### **4A: Skriv BUGGY implementation först** (1 min)

**ÖPPNA:** `TodoFilterService.BUGGY.cs`

**VAD DU SÄGER:**
```
"Nu ska jag skriva en 'snabb lösning' - som många gör under tidspress."
```

**VISA KODEN (rad 17-26):**
```csharp
public List<Todo> FilterByStatus(List<Todo> todos, string status)
{
    if (todos == null)
        return new List<Todo>();

    // BUG: Case-sensitive jämförelse!
    return todos
        .Where(t => t.Status == status)  // ❌ DÅLIGT!
        .ToList();
}
```

**VAD DU SÄGER:**
```
"Ser bra ut? Funkar säkert? 🤔

NEJ! Två buggar gömmer sig här:
1. Ingen null-check på status → KRASCH om någon skickar null
2. Case-sensitive → 'Done' != 'done' → vi tappar data!"
```

---

### **4B: Skriv tester** (2 min)

**ÖPPNA:** `TodoFilterServiceTests.BUGGY.cs`

**VAD DU SÄGER:**
```
"Nu skriver vi tester för att BEVISA att det är trasigt."
```

**VISA TEST 1 (rad 31-47):**
```csharp
[Fact]
public void FilterByStatus_WithNullStatus_ReturnsEmptyList()
{
    var todos = new List<Todo>
    {
        new Todo { Id = 1, Title = "Task 1", Status = "done" }
    };

    var result = _service.FilterByStatus(todos, null);

    Assert.Empty(result);  // Vad förväntar vi? Tom lista!
}
```

**VISA TEST 2 (rad 56-74):**
```csharp
[Fact]
public void FilterByStatus_IsCaseInsensitive()
{
    var todos = new List<Todo>
    {
        new Todo { Id = 1, Title = "Task 1", Status = "Done" },
        new Todo { Id = 2, Title = "Task 2", Status = "DONE" },
        new Todo { Id = 3, Title = "Task 3", Status = "done" }
    };

    var result = _service.FilterByStatus(todos, "done");

    Assert.Equal(3, result.Count);  // Förväntar 3 - alla varianter!
}
```

**VAD DU SÄGER:**
```
"Två kritiska edge case-tester:
1. Null-hantering
2. Case-insensitive (verkliga användare skriver 'Done', 'DONE', 'done')

Utan dessa tester → buggarna når produktion!"
```

---

### **4C: KÖR TESTERNA → RÖDA!** (1 min) 💥

**KÖR I TERMINAL:**
```bash
cd tests/TodoFilter.Tests
dotnet test --filter "TodoFilterServiceTests_BUGGY"
```

**FÖRVÄNTAT RESULTAT:**
```
❌ FilterByStatus_WithNullStatus_ReturnsEmptyList
   System.NullReferenceException: Object reference not set to an instance

❌ FilterByStatus_IsCaseInsensitive
   Expected: 3
   Actual: 1
```

**VAD DU SÄGER:**
```
"BOOM! 💥

Test 1: KRASCHAR med NullReferenceException
Test 2: FAILAR - förväntar 3, får endast 1

Detta hade varit PRODUKTION-BUGGAR utan TDD!
Användare hade tappat data och fått krasch-rapporter."
```

**PAUSA - LÅT DET SJUNKA IN (5 sekunder tystnad)**

---

### **4D: FIXA buggarna** (1 min)

**ÖPPNA:** `TodoFilterService.cs` (den KORREKTA versionen)

**VISA FIXARNA (rad 17-27):**
```csharp
public List<Todo> FilterByStatus(List<Todo> todos, string status)
{
    // FIX #1: Null-check på BÅDA parametrarna
    if (todos == null || string.IsNullOrEmpty(status))
        return new List<Todo>();

    // FIX #2: Case-insensitive jämförelse
    return todos
        .Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
        .ToList();
}
```

**VAD DU SÄGER:**
```
"Två små ändringar:
1. ✅ Null-check på status också
2. ✅ .Equals() med StringComparison.OrdinalIgnoreCase

30 sekunders fix - men testerna tvingade oss att göra det INNAN produktion!"
```

---

### **4E: KÖR TESTERNA IGEN → GRÖNA!** (30 sekunder) ✅

**BYT TESTFIL:** Redigera `TodoFilterServiceTests.BUGGY.cs` rad 12 till:
```csharp
private readonly TodoFilterService _service;  // Ta bort "_BUGGY"
```

**ELLER kör de riktiga testerna:**
```bash
dotnet test --filter "TodoFilterServiceTests"
```

**FÖRVÄNTAT RESULTAT:**
```
✅ Passed! - Failed: 0, Passed: 6, Skipped: 0, Total: 6
```

**VAD DU SÄGER:**
```
"GRÖNT! ✅

Alla tester passar. Koden är nu:
✅ Null-säker
✅ Case-insensitive
✅ Redo för produktion

TDD hittade buggar på 30 sekunder istället för i produktionen!"
```

---

## **STEG 5: Manuell test** (1 min)

**VAD DU SÄGER:**
```
"Låt oss köra manuellt också - dubbel-kolla!"
```

**SKAPA:** Snabb test i `Program.cs` (om du vill visa):
```csharp
var service = new TodoFilterService();
var todos = new List<Todo>
{
    new Todo { Status = "Done" },
    new Todo { Status = "DONE" },
    new Todo { Status = "done" }
};

var result = service.FilterByStatus(todos, "done");
Console.WriteLine($"Hittade {result.Count} todos");  // → 3
```

**ELLER hoppa över och säg:**
```
"Vi har redan 6 gröna tester - vi litar på dem!"
```

---

## **STEG 6: Refaktorera** (30 sekunder)

**VAD DU SÄGER:**
```
"Kod är redan clean:
✅ SRP - en metod, en uppgift
✅ DRY - ingen upprepad logik
✅ KISS - enkelt och läsbart

Inga förbättringar behövs. DONE!"
```

---

## **STEG 7: Commit** (30 sekunder)

**KÖR I TERMINAL:**
```bash
git add .
git commit -m "feat: Add FilterByStatus with null-safety and case-insensitive matching

✅ Handles null inputs gracefully
✅ Case-insensitive status comparison
✅ 6 passing tests (edge cases covered)

TDD caught 2 bugs before production:
- NullReferenceException on null status
- Case-sensitive comparison losing data"
```

**VAD DU SÄGER:**
```
"Commit! Klar för deploy.

Utan TDD: Buggar i produktion.
Med TDD: Buggar hittade på 30 sekunder.

Det är VÄRDET med AI-driven TDD!"
```

---

## 🎯 SAMMANFATTNING (1 min)

### **VAD DU SÄGER:**
```
"Vad har vi lärt oss?

INNAN TDD:
❌ Skrev 'snabb lösning'
❌ Såg OK ut
❌ 2 buggar gömde sig → produktion

MED TDD:
✅ Tester tvingade oss att tänka på edge cases
✅ Buggar hittade på 30 sekunder
✅ Säker kod INNAN deploy

TDD är inte 'extra arbete' - det är FÖRSÄKRING.
Utan det betalar ni med produktions-buggar.

Och med AI som skriver både tester OCH implementation?
20 minuter från idé till production-ready kod.

DETTA är AI-Driven Development!"
```

---

## 📊 TIDSLINJE (Total: ~15 min)

| Steg | Tid | Aktivitet |
|------|-----|-----------|
| 1 | 1 min | Förtydliga spec |
| 2 | 30 sek | Backlog |
| 3 | 30 sek | Arkitektur |
| 4A | 1 min | Visa buggy kod |
| 4B | 2 min | Visa tester |
| 4C | 1 min | Kör tester → RÖDA ❌ |
| **PAUS** | **5 sek** | **Låt budskapet sjunka in** |
| 4D | 1 min | Fixa buggar |
| 4E | 30 sek | Kör tester → GRÖNA ✅ |
| 5 | 1 min | Manuell test (optional) |
| 6 | 30 sek | Refaktor (ingen behövs) |
| 7 | 30 sek | Commit |
| Sammanfattning | 1 min | Key takeaways |

---

## 🎤 KEY TALKING POINTS

### **Budskap 1: TDD hittar buggar INNAN produktion**
```
"Null-krasch och tappade data → hade varit produktions-buggar.
TDD hittade dem på 30 sekunder."
```

### **Budskap 2: Edge cases är inte optional**
```
"'Done' != 'done' verkar trivialt?
I produktion tappar vi 2 av 3 todos → arg användare!"
```

### **Budskap 3: AI + TDD = Superkraft**
```
"AI skriver både tester OCH implementation.
Du fokuserar på 'VAD' - AI fixar 'HUR'.
20 minuter från idé till prod-ready kod."
```

---

## 🛠️ TROUBLESHOOTING

### **Om tester inte kompilerar:**
```bash
# Kontrollera att du använder rätt namespace
dotnet build
```

### **Om du vill byta mellan buggy och fixed live:**
```csharp
// I TodoFilterServiceTests.BUGGY.cs rad 12:
private readonly TodoFilterService _service;        // Fixed version
private readonly TodoFilterService_BUGGY _service;  // Buggy version
```

### **Om du vill visa endast specifika tester:**
```bash
dotnet test --filter "FilterByStatus_WithNullStatus"
dotnet test --filter "FilterByStatus_IsCaseInsensitive"
```

---

## 📝 CHEAT SHEET (håll vid datorn)

### **Röda tester (buggy):**
```
NullReferenceException → "Ingen null-check!"
Expected 3, Actual 1 → "Case-sensitive!"
```

### **Gröna tester (fixed):**
```
6 passed → "Null-safe och case-insensitive!"
```

### **Commit message:**
```
feat: Add FilterByStatus
✅ Null-safe ✅ Case-insensitive ✅ 6 tests
TDD caught 2 bugs before production
```

---

## 🎬 LYCKA TILL!

**Kom ihåg:**
- Prata MER än du kodar (förklara tankeprocessen)
- Pausa efter röda tester (låt budskapet sjunka in)
- Visa entusiasm när testerna blir gröna!
- Koppla tillbaka till värde (Andrijas tema)

**Du kan göra detta! 💪**
