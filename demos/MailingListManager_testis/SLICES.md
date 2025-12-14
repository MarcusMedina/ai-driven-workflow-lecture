# ?? Vertical Slices - MailingListManager

> **Projekt:** MailingListManager (Blazor, .NET 10)
> **Metod:** AI-Driven Development
> **Total tid:** ~85 minuter (5 slices � ~17 minuter var)
> **Prioritet:** 1 ? 2 ? 3 ? 4 ? 5

---

## ?? Produktvision

**En mailista-hanterare d�r admin kan hantera prenumeranter s�kert.**

**F�rsta vertical slice:** Admin kan logga in och hantera emaillistan.

---

# ?? SLICE 1: Admin Login (Authentication)

## **Detaljer**

| F�lt           | V�rde                                                                  |
| -------------- | ---------------------------------------------------------------------- |
| **User Story** | Som admin vill jag logga in s�kert f�r att bara jag kan hantera listan |
| **MVP**        | H�rdkodad admin-anv�ndare (admin@example.com / Admin123!)              |
| **Scope**      | Login-formul�r + Cookie-auth + AuthenticationStateProvider             |
| **V�rde**      | ? S�kerhet - endast admin kommer �t systemet                           |
| **Tid**        | ~20 min                                                                |
| **Prioritet**  | ?? **M�STE** ha (blocker f�r resten)                                   |

## **Acceptance Criteria**

```
? Admin kan logga in med admin@example.com / Admin123!
? Felaktiga uppgifter ? Felmeddelande visas
? Inloggad admin ser "Logga ut"-knapp
? Utloggad anv�ndare omdirigeras till /login
? [Authorize] skyddar admin-sidor
? Sessioner f�rvaras i cookies
```

## **Edge Cases**

```
? Null/tomt email ? Valideringsfel
? Null/tomt l�senord ? Valideringsfel
? XSS i email-f�lt ? Saniteras automatiskt
? SQL injection ? N/A (h�rdkodade credentials)
? Case-sensitivity ? admin@example.com (case-insensitive)
? Whitespace trim ? "  admin@example.com  " ? Ok
```

## **Arkitektur**

```
ALTERNATIV 1 (REKOMMENDERAD): Cookie Authentication
?? ASP.NET Core Authentication middleware
?? H�rdkodad admin i appsettings.json
?? AuthenticationStateProvider (Blazor)
?? [Authorize] attributes
?? Komplexitet: ?/5 | Tid: 20 min | S�kerhet: ? Demo

ALTERNATIV 2: ASP.NET Core Identity
?? EF Core Identity
?? Password hashing
?? User management DB
?? Komplexitet: ????/5 | Tid: 120 min | S�kerhet: ? Produktion

ALTERNATIV 3: JWT Tokens
?? JWT generation vid login
?? Stateless token validation
?? Client-side storage
?? Komplexitet: ???/5 | Tid: 60 min | S�kerhet: ? API-focused
```

**Val:** Alternativ 1 (Cookie auth) - snabbt, enkelt, tillr�ckligt f�r demo.

## **Tester (TDD)**

```csharp
// AuthService.Tests.cs
[Fact]
public void Login_WithValidCredentials_ReturnsSuccess()
{
    // admin@example.com / Admin123! ? Autentiserad
}

[Fact]
public void Login_WithInvalidPassword_ReturnsUnauthorized()
{
    // admin@example.com / WrongPassword ? Unauthorized
}

[Fact]
public void Login_WithInvalidEmail_ReturnsUnauthorized()
{
    // wrong@example.com / Admin123! ? Unauthorized
}

[Fact]
public void Login_WithNullEmail_ReturnsValidationError()
{
    // null email ? ValidationException
}

[Fact]
public void Login_WithXSSAttempt_IsSanitized()
{
    // "<script>alert('xss')</script>" ? Saniteras
}
```

## **Implementation Checklist**

```
? Services/AuthService.cs
  ?? Task<AuthResult> LoginAsync(string email, string password)
  ?? Task LogoutAsync()

? Authentication/CustomAuthStateProvider.cs
  ?? Extends AuthenticationStateProvider
  ?? GetAuthenticationStateAsync()

? Components/Pages/Login.razor
  ?? Formul�r med email + password
  ?? Submit-knapp ? AuthService.LoginAsync()
  ?? Felmeddelanden

? appsettings.json
  ?? H�rdkodade credentials (demo-only)

? Program.cs
  ?? .AddAuthentication()
  ?? .AddCookie()
  ?? .AddScoped<CustomAuthStateProvider>()

? _Imports.razor
  ?? @using System.Security.Claims
  ?? @using Microsoft.AspNetCore.Components.Authorization

? Tester (xUnit)
  ?? AuthService.Tests.cs (5 tester, alla gr�na)
```

## **Deliverables**

- ? `Services/AuthService.cs` - Login-logik
- ? `Services/AuthService.Tests.cs` - Unit tests
- ? `Authentication/CustomAuthStateProvider.cs` - Blazor auth
- ? `Components/Pages/Login.razor` - Login UI
- ? `appsettings.json` - Credentials + config
- ? `Program.cs` - DI-setup

## **Definition of Done**

```
? Alla 5 tester gr�na
? Manual test: Login funkar
? Code review: SRP, DRY, SoC, KISS
? Ingen hardcoded secrets i kod (bara config)
? Dokumentation klar
? Commit med message enligt format
```

---

# ?? SLICE 2: Lista Prenumeranter (Read)

## **Detaljer**

| F�lt           | V�rde                                                        |
| -------------- | ------------------------------------------------------------ |
| **User Story** | Som inloggad admin vill jag se alla prenumeranter i en lista |
| **MVP**        | Visa prenumeranter fr�n databas                              |
| **Scope**      | EF Core + DbContext + Subscriber-modell + List-komponent     |
| **V�rde**      | ? Admin f�r �versikt �ver sin mailista                       |
| **Tid**        | ~15 min                                                      |
| **Prioritet**  | ?? **B�R** ha (efter Slice 1)                                |
| **Dependency** | ?? Slice 1 (kr�ver auth)                                     |

## **Acceptance Criteria**

```
? Endast inloggad admin kan se listan
? Lista visar alla prenumeranter (email)
? Tom lista ? "Inga prenumeranter �nnu" visas
? Data fr�n databas (inte h�rdkodad)
? Sida laddar snabbt (<100ms)
? Lista uppdateras vid CRUD-operationer
```

## **Edge Cases**

```
? Tom databas ? "Inga prenumeranter �nnu"
? 1000+ prenumeranter ? Paginering planeras (slice 6)
? Dubbletter i DB ? Visas (fixas av UNIQUE constraint)
? Null/tomma v�rden ? Filtreras bort
? Databasfel ? Felmeddelande visas
```

## **Arkitektur**

```
Database (SQLite f�r demo)
?? Subscribers table
?  ?? Id (int, PK)
?  ?? Email (string, UNIQUE, NOT NULL)
?  ?? CreatedAt (datetime)
?
EF Core DbContext
?? MailingListContext.cs
?  ?? DbSet<Subscriber> Subscribers
?  ?? OnModelCreating() ? UNIQUE constraint
?
Services
?? SubscriberService.cs
?  ?? Task<List<Subscriber>> GetAllAsync()
?
UI Components
?? Pages/Subscribers.razor
   ?? @authorize (skyddad)
   ?? Visar lista
```

## **Tester (TDD)**

```csharp
// SubscriberService.Tests.cs
[Fact]
public async Task GetAllAsync_WithSubscribers_ReturnsAll()
{
    // DB has 3 subscribers ? Returns list with 3
}

[Fact]
public async Task GetAllAsync_WithEmptyDB_ReturnsEmptyList()
{
    // DB is empty ? Returns empty list
}

[Fact]
public async Task GetAllAsync_ReturnsSortedByEmail()
{
    // Returns emails sorted alphabetically
}

[Fact]
public async Task GetAllAsync_ExcludesNull()
{
    // Never returns null emails (filtered)
}

[Fact]
public async Task GetAllAsync_WithDatabaseError_ThrowsException()
{
    // DB connection fails ? Throws
}
```

## **Implementation Checklist**

```
? Models/Subscriber.cs
  ?? Id, Email, CreatedAt

? Data/MailingListContext.cs
  ?? DbSet<Subscriber> Subscribers
  ?? OnModelCreating() + UNIQUE constraint

? Services/SubscriberService.cs
  ?? Task<List<Subscriber>> GetAllAsync()

? Components/Pages/Subscribers.razor
  ?? @page "/subscribers"
  ?? @authorize
  ?? Lista fr�n SubscriberService
  ?? "Inga prenumeranter �nnu" - empty state

? Program.cs
  ?? .AddDbContext<MailingListContext>()
  ?? .AddScoped<SubscriberService>()

? appsettings.json
  ?? ConnectionString till SQLite

? Migrations
  ?? dotnet ef migrations add InitialCreate
  ?? dotnet ef database update

? Tester (xUnit)
  ?? SubscriberService.Tests.cs (5 tester, alla gr�na)
```

## **Deliverables**

- ? `Models/Subscriber.cs` - Datamodell
- ? `Data/MailingListContext.cs` - DbContext
- ? `Services/SubscriberService.cs` - Business logic
- ? `Services/SubscriberService.Tests.cs` - Unit tests
- ? `Components/Pages/Subscribers.razor` - UI
- ? `Migrations/InitialCreate.cs` - Database schema

## **Definition of Done**

```
? Alla 5 tester gr�na
? Manual test: Lista visas
? @authorize skyddar sidan
? Empty state hanterad
? Code review passed
? Database seeded med test-data
```

---

# ?? SLICE 3: L�gg till Prenumerant (Create)

## **Detaljer**

| F�lt           | V�rde                                                                        |
| -------------- | ---------------------------------------------------------------------------- |
| **User Story** | Som inloggad admin vill jag l�gga till nya emailadresser f�r att v�xa listan |
| **MVP**        | Formul�r + email-validering + spara till DB                                  |
| **Scope**      | Input-validering + Create-operation + UI-uppdatering                         |
| **V�rde**      | ? Admin kan bygga sin mailista                                               |
| **Tid**        | ~20 min                                                                      |
| **Prioritet**  | ?? **B�R** ha (efter Slice 2)                                                |
| **Dependency** | ?? Slice 2 (kr�ver lista)                                                    |

## **Acceptance Criteria**

```
? Admin kan fylla i email och klicka "L�gg till"
? Giltiga emails sparas till databasen
? Ogiltiga emails ? Valideringsfel
? Dubbletter ? Varna: "Email redan prenumerant"
? Tom input ? Valideringsfel
? Formul�r t�mmas efter framg�ng
? Lista uppdateras automatiskt
? Endast inloggad admin kan l�gga till
```

## **Edge Cases**

```
? Ogiltigt email-format ? Valideringsfel
? Whitespace ? Trimmas automatiskt
? Dubbletter (case-insensitive) ? Varna
? Mycket l�ngt email ? Trunkeras/v�gras
? Special characters ? Till�ts om RFC 5322-giltigt
? SQL injection i email ? F�rhindras av parameteriserad query
? XSS i feedback-meddelanden ? Saniteras
```

## **Validering**

```
Email Format (RFC 5322 basic)
?? Regex: ^[^\s@]+@[^\s@]+\.[^\s@]+$
?? L�ngd: 1-254 tecken
?? Till�tna chars: a-z, 0-9, ._+-@

Business Rules
?? M�ste vara unikt (UNIQUE constraint)
?? Case-insensitive duplicat-check
?? Trim whitespace f�re sparande
```

## **Tester (TDD)**

```csharp
// SubscriberService.Tests.cs - Create
[Fact]
public async Task CreateAsync_WithValidEmail_Succeeds()
{
    // "test@example.com" ? Sparad i DB
}

[Fact]
public async Task CreateAsync_WithInvalidEmail_Throws()
{
    // "invalid-email" ? ValidationException
}

[Fact]
public async Task CreateAsync_WithDuplicateEmail_Throws()
{
    // "test@example.com" redan finns ? DuplicateException
}

[Fact]
public async Task CreateAsync_WithNullEmail_Throws()
{
    // null ? ValidationException
}

[Fact]
public async Task CreateAsync_WithXSSAttempt_IsSanitized()
{
    // "<script>alert('xss')</script>@example.com" ? Saniteras
}
```

## **Implementation Checklist**

```
? Services/SubscriberService.cs (extend)
  ?? Task<Subscriber> CreateAsync(string email)
  ?? Validering: EmailValidator.IsValid(email)
  ?? Duplicate-check: await _context.Subscribers.AnyAsync()
  ?? Trim + ToLower() innan sparande

? Services/Validators/EmailValidator.cs
  ?? static bool IsValid(string email)
  ?? Regex-validering

? Components/Pages/Subscribers.razor (extend)
  ?? Form f�r "L�gg till email"
  ?? Input field + Submit-knapp
  ?? Felmeddelanden
  ?? Success-meddelande
  ?? Uppdatera lista efter Create

? Services/SubscriberService.Tests.cs (extend)
  ?? 5 nya tester f�r Create

? Models/Subscriber.cs (extend)
  ?? Validering p� modell-level (om �nskat)

? Data/MailingListContext.cs (extend)
  ?? S�kerst�ll UNIQUE constraint p� Email
  ?? Index f�r snabb lookup
```

## **Deliverables**

- ? `Services/SubscriberService.cs` (Updated)
- ? `Services/Validators/EmailValidator.cs` (New)
- ? `Components/Pages/Subscribers.razor` (Updated)
- ? `Services/SubscriberService.Tests.cs` (Extended)

## **Definition of Done**

```
? Alla 5 tester gr�na
? Manual test: Email l�ggs till
? Manual test: Dubbletter blockeras
? Manual test: Invalid emails blockeras
? Felmeddelanden tydliga
? Formul�r t�mms efter sparande
? Lista uppdateras automatiskt
```

---

# ?? SLICE 4: Ta bort Prenumerant (Delete)

## **Detaljer**

| F�lt           | V�rde                                                                               |
| -------------- | ----------------------------------------------------------------------------------- |
| **User Story** | Som inloggad admin vill jag ta bort emailadresser f�r att hantera avprenumerationer |
| **MVP**        | Delete-knapp + bekr�ftelsedialog + uppdatera lista                                  |
| **Scope**      | Delete-operation + UI-uppdatering + bekr�ftelse                                     |
| **V�rde**      | ? Admin kan hantera avprenumerationer                                               |
| **Tid**        | ~15 min                                                                             |
| **Prioritet**  | ?? **B�R** ha (efter Slice 3)                                                       |
| **Dependency** | ?? Slice 3 (kr�ver Create)                                                          |

## **Acceptance Criteria**

```
? Varje prenumerant har "Ta bort"-knapp
? Klick p� "Ta bort" ? Bekr�ftelsedialog
? Bekr�fta ? Email tas bort fr�n DB
? Avbryt ? Inget h�nder
? Efter borttagning ? Lista uppdateras
? Endast inloggad admin kan ta bort
? Systemmeddelande: "Email borttagen" visas
```

## **Edge Cases**

```
? Klick p� "Ta bort" tv� g�nger snabbt ? Idempotent
? Email raderas medan dialog �r �ppen ? Hanteras gracefully
? Databasfel vid delete ? Felmeddelande visas
? Ingen prenumeranter ? Lista tom
? XSS i bekr�ftelsedialog ? Saniteras
```

## **Tester (TDD)**

```csharp
// SubscriberService.Tests.cs - Delete
[Fact]
public async Task DeleteAsync_WithValidId_Succeeds()
{
    // Delete subscriber with id=1 ? Removed from DB
}

[Fact]
public async Task DeleteAsync_WithInvalidId_Throws()
{
    // Delete subscriber with id=999 ? NotFoundException
}

[Fact]
public async Task DeleteAsync_WithNullId_Throws()
{
    // Delete null ? ValidationException
}

[Fact]
public async Task DeleteAsync_IsIdempotent()
{
    // Delete twice ? Second returns NotFoundException (or silent)
}

[Fact]
public async Task DeleteAsync_UpdatesListAfterDelete()
{
    // Create 2, Delete 1 ? List has 1
}
```

## **Implementation Checklist**

```
? Services/SubscriberService.cs (extend)
  ?? Task DeleteAsync(int id)
  ?? Hanterar NotFoundException om inte finns

? Components/Pages/Subscribers.razor (extend)
  ?? "Ta bort"-knapp f�r varje prenumerant
  ?? Bekr�ftelsedialog (Blazor modal eller sweetalert)
  ?? Submit: await SubscriberService.DeleteAsync(id)
  ?? Uppdatera lista efter delete
  ?? "Email borttagen"-meddelande

? Services/SubscriberService.Tests.cs (extend)
  ?? 5 nya tester f�r Delete

? Models/Exceptions/NotFoundException.cs
  ?? Custom exception f�r CRUD
```

## **Deliverables**

- ? `Services/SubscriberService.cs` (Updated)
- ? `Components/Pages/Subscribers.razor` (Updated)
- ? `Services/SubscriberService.Tests.cs` (Extended)
- ? `Models/Exceptions/NotFoundException.cs` (New)

## **Definition of Done**

```
? Alla 5 tester gr�na
? Manual test: Delete funkar
? Manual test: Bekr�ftelsedialog
? Manual test: Lista uppdateras
? Idempotency testerad
? Felmeddelanden hanterade
```

---

# ?? SLICE 5: Email-validering (Quality)

## **Detaljer**

| F�lt           | V�rde                                                                        |
| -------------- | ---------------------------------------------------------------------------- |
| **User Story** | Som admin vill jag veta att bara giltiga emails �r i listan f�r datakvalitet |
| **MVP**        | Regex-validering + dubblettcheck + tydliga felmeddelanden                    |
| **Scope**      | Valideringslogik + felmeddelanden + unit tests                               |
| **V�rde**      | ? Datakvalitet - bara giltiga emails i systemet                              |
| **Tid**        | ~15 min                                                                      |
| **Prioritet**  | ?? **B�R** ha (kan g�ras parallellt med slice 3)                             |
| **Dependency** | ?? Slice 3 (Create-logik)                                                    |

## **Acceptance Criteria**

```
? Email-format valideras (RFC 5322 basic)
? Ogiltiga format ? Tydligt felmeddelande
? Dubbletter blockeras ? "Already subscribed"
? Whitespace trimmas automatiskt
? Case-insensitive duplikat-check
? L�nga emails trunkeras/blockeras
? Alla felmeddelanden p� svenska
```

## **Validering Rules**

```
EMAIL FORMAT (RFC 5322 basic)
?? Format: localpart@domain.tld
?? Regex: ^[a-zA-Z0-9._\-+]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$
?? L�ngd: 5-254 tecken
?? Till�tna chars: a-z, 0-9, ._+-
?? M�ste inneh�lla exakt en @
?? Domain m�ste ha minst en punkt

DUPLICATES
?? Case-insensitive check
?? Whitespace trimmed
?? UNIQUE constraint i DB

EDGE CASES
?? "test@example.com" ?
?? "first.last@example.com" ?
?? "user+tag@example.co.uk" ?
?? "test@localhost" ?
?? "test" ?
?? "test@" ?
?? "@example.com" ?
?? "test@@example.com" ?
```

## **Error Messages**

```
Felaktig email-format
?? "Email m�ste vara i formatet: namn@exempel.se"

Email redan prenumerant
?? "test@example.com �r redan prenumerant"

Tom email
?? "Email-f�ltet f�r inte vara tomt"

Email f�r l�ngt
?? "Email f�r max vara 254 tecken"

Ok�nt fel
?? "N�got gick fel. F�rs�k igen."
```

## **Tester (TDD)**

```csharp
// EmailValidator.Tests.cs
[Theory]
[InlineData("test@example.com")]
[InlineData("first.last@example.com")]
[InlineData("user+tag@example.co.uk")]
[InlineData("user_name@example.com")]
public void IsValid_WithValidEmails_ReturnsTrue(string email)
{
}

[Theory]
[InlineData("invalid")]
[InlineData("test@")]
[InlineData("@example.com")]
[InlineData("test@@example.com")]
[InlineData("test@localhost")]
[InlineData("")]
public void IsValid_WithInvalidEmails_ReturnsFalse(string email)
{
}

[Fact]
public void IsValid_WithXSSAttempt_Sanitizes()
{
    // "<script>alert('xss')</script>@example.com" ? False
}

[Fact]
public void IsValid_WithWhitespace_Trims()
{
    // "  test@example.com  " ? True
}

[Fact]
public void IsValid_CaseInsensitive()
{
    // "TEST@EXAMPLE.COM" ? True
}
```

```csharp
// SubscriberService.Tests.cs - Validation
[Fact]
public async Task CreateAsync_ChecksForDuplicates_CaseInsensitive()
{
    // "test@example.com" och "TEST@EXAMPLE.COM" ? Duplicate
}

[Fact]
public async Task CreateAsync_TrimsWhitespace()
{
    // "  test@example.com  " ? Sparas som "test@example.com"
}

[Fact]
public async Task CreateAsync_RejectsLongEmails()
{
    // 255+ chars ? ValidationException
}
```

## **Implementation Checklist**

```
? Services/Validators/EmailValidator.cs
  ?? static bool IsValid(string email)
  ?? Regex: ^[a-zA-Z0-9._\-+]+@[a-zA-Z0-9.\-]+\.[a-zA-Z]{2,}$
  ?? Length check: 5-254
  ?? Trim + ToLower()

? Services/SubscriberService.cs (extend)
  ?? Validering innan Create
  ?? Duplicate-check: case-insensitive
  ?? Trim whitespace
  ?? Tydliga error messages

? Components/Pages/Subscribers.razor (extend)
  ?? Visa error messages fr�n validering
  ?? Real-time validation (optional)

? Services/Validators/EmailValidator.Tests.cs (new)
  ?? 15+ tester f�r email-format

? Services/SubscriberService.Tests.cs (extend)
  ?? 5+ tester f�r validering-logik

? Resources/Messages/ValidationMessages.cs
  ?? Centraliserade felmeddelanden p� svenska
```

## **Deliverables**

- ? `Services/Validators/EmailValidator.cs` (Enhanced)
- ? `Services/Validators/EmailValidator.Tests.cs` (New)
- ? `Services/SubscriberService.cs` (Enhanced)
- ? `Components/Pages/Subscribers.razor` (Enhanced)
- ? `Resources/Messages/ValidationMessages.cs` (New)

## **Definition of Done**

```
? Alla 15+ tester gr�na
? Manual test: Giltiga emails accepteras
? Manual test: Ogiltiga emails blockeras
? Manual test: Dubbletter blockeras
? Felmeddelanden tydliga och p� svenska
? Whitespace trimmas
? Case-insensitive dublikat-check
```

---

# ?? N�STA STEPS EFTER SLICE 5

## **Backlog f�r Slice 6-10 (Future)**

### **Slice 6: Email-notifikation**

- Skicka bekr�ftelse-email vid subscription
- Use case: SendGrid eller SMTP

### **Slice 7: Paginering**

- Lista 20 prenumeranter per sida
- Anv�nd Radzen Blazor components

### **Slice 8: Search/Filter**

- Filtrera prenumeranter efter email-fragment
- Real-time search

### **Slice 9: Export/Import**

- Exportera lista som CSV
- Importera emails fr�n CSV

### **Slice 10: Audit Log**

- Logga alla �ndringar (vem, n�r, vad)
- Visa �ndringshistorik

---

# ?? SAMMANFATTNING

| Slice | Titel                 | User Story           | Tid    | V�rde       |
| ----- | --------------------- | -------------------- | ------ | ----------- |
| 1     | Admin Login           | Logga in s�kert      | 20 min | ?? M�ste ha |
| 2     | Lista Prenumeranter   | Se all prenumeranter | 15 min | ?? B�r ha   |
| 3     | L�gg till Prenumerant | V�xa listan          | 20 min | ?? B�r ha   |
| 4     | Ta bort Prenumerant   | Hantera avprenum.    | 15 min | ?? B�r ha   |
| 5     | Email-validering      | Datakvalitet         | 15 min | ?? B�r ha   |

**Total tid:** ~85 minuter (5 � 17 min)

**Status:** ? Alla slices helt specifierade och klara f�r implementation

---

# ?? G� IG�NG

F�r varje slice, f�lj dessa 7 steg:

1. **F�rtydliga & Spec** (2 min) ? _Klar! Se ovan_
2. **Backlog & Slices** (3 min) ? _Klar! Se ovan_
3. **Arkitektur** (3 min) ? _Klar f�r Slice 1, g�r f�r �vriga_
4. **TDD** (5 min) ? _Skriv tester + kod_
5. **Manuell test** (3 min) ? _Testa i UI_
6. **Refactor** (3 min) ? _Granska kod_
7. **Commit** (2 min) ? _Commit med message_

**Slut tid per slice:** ~20 minuter

Se `DEMO_WALKTHROUGH.md` f�r detaljer om processen.

---

**Lycka till! ??**
