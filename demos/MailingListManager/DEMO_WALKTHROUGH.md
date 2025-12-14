# 🎬 AI-Driven Development Workflow - Komplett Demo

> **Projekt:** MailingListManager (Blazor)
> **Första slice:** Admin-inloggning + Registrering/Avregistrering av emailadresser
> **Tid:** ~20 minuter per slice
> **Mål:** Visa ALLA 7 STEGEN från idé till commit

---

## 🎯 Vad vi ska bygga

**Produktvision:** En mailista-hanterare där admin kan hantera prenumeranter.

**Första vertical slice:** Admin kan logga in och lägga till/ta bort emailadresser.

---

# 📋 STEG 1: Förtydliga & Spec (2 min)

## **VAD DU GÖR:**
```
Innan vi frågar AI - klarifiera SJÄLV vad vi vill ha.
"Kan jag förklara detta på 1 minut till någon?"
```

## **Specifikation:**

### **Problem:**
Admin behöver kunna:
1. Logga in säkert
2. Se lista över prenumeranter
3. Lägga till nya emailadresser
4. Ta bort emailadresser

### **Input/Output:**
```
Login:
  Input: Email + lösenord
  Output: Autentiserad session ELLER felmeddelande

Lägg till prenumerant:
  Input: Email-adress
  Output: Prenumerant tillagd ELLER valideringsfel

Ta bort prenumerant:
  Input: Email-adress
  Output: Prenumerant borttagen

Lista prenumeranter:
  Input: -
  Output: Lista över alla emailadresser
```

### **Edge cases:**
```
✅ Ogiltigt email-format → Valideringsfel
✅ Dubbletter → Varna användare
✅ Tom lista → Visa "Inga prenumeranter ännu"
✅ Felaktiga login-uppgifter → Felmeddelande
✅ SQL injection → Förhindra (parameteriserade queries)
```

### **Success criteria:**
```
✅ Admin kan logga in
✅ Admin ser lista över prenumeranter
✅ Admin kan lägga till email
✅ Admin kan ta bort email
✅ Alla inputs valideras
✅ Säkerhet: Ingen SQL injection, XSS
```

## **✅ Checkpoint:**
```
"Kan jag förklara detta på 1 minut?"
JA → Gå vidare till Steg 2
NEJ → Förtydliga mer
```

---

# 🗂️ STEG 2: Backlog & Vertical Slices (3 min)

## **PROMPT TILL AI:**

```
Jag bygger en mailista-hanterare i Blazor.

FUNKTIONALITET:
- Admin kan logga in
- Admin kan se lista över prenumeranter
- Admin kan lägga till/ta bort emailadresser

VIKTIGT: Vi planerar nu, inget kodande än.

Bryt ner detta i vertical slices (kompletta user journeys).

Varje slice ska:
- Kunna byggas på 15-25 minuter
- Vara testbar och deploybar
- Ge värde till användaren
- Respektera dataintegritet

Föreslå 3-5 vertical slices i prioritetsordning.
```

## **FÖRVÄNTAT SVAR FRÅN AI:**

```
Föreslagna vertical slices (prioritetsordning):

1. **Admin Login (Authentication)**
   - User story: Som admin vill jag logga in säkert
   - MVP: Hårdkodad admin-användare (admin@example.com)
   - Scope: Login-formulär + session management
   - Värde: Säkerhet - endast admin kommer åt system
   - Tid: ~20 min

2. **Lista Prenumeranter (Read)**
   - User story: Som admin vill jag se alla prenumeranter
   - MVP: Visa lista från databas
   - Scope: Databasmodell + UI-lista
   - Värde: Admin får översikt
   - Tid: ~15 min

3. **Lägg till Prenumerant (Create)**
   - User story: Som admin vill jag lägga till nya emailadresser
   - MVP: Formulär + validering + spara till DB
   - Scope: Input-validering + create-operation
   - Värde: Bygga listan
   - Tid: ~20 min

4. **Ta bort Prenumerant (Delete)**
   - User story: Som admin vill jag ta bort emailadresser
   - MVP: Delete-knapp + bekräftelse
   - Scope: Delete-operation + UI-uppdatering
   - Värde: Hantera avprenumerationer
   - Tid: ~15 min

5. **Email-validering (Quality)**
   - User story: Som admin vill jag förhindra ogiltiga emails
   - MVP: Regex-validering + dubblettcheck
   - Scope: Valideringslogik + felmeddelanden
   - Värde: Datakvalitet
   - Tid: ~15 min

REKOMMENDATION: Börja med slice 1 (Admin Login).
```

## **DU VÄLJER:**
```
"OK, vi kör slice 1 först: Admin Login.
Resten läggs i backlog."
```

---

# 🏗️ STEG 3: Arkitektur (3 min)

## **PROMPT TILL AI:**

```
Vi ska implementera Admin Login för mailista-hanteraren.

KONTEXT:
- Blazor WebApp (.NET 10)
- Första slicen: Admin kan logga in
- MVP: Hårdkodad admin-användare för demo

VIKTIGT: Vi planerar arkitektur nu, inget kodande än.

Föreslå 3 olika arkitekturlösningar för authentication:

För varje lösning, beskriv:
1. Komplexitet (1-5, där 1 = enklast)
2. Säkerhet
3. Performance
4. Maintainbarhet
5. Trade-offs

Vilken rekommenderar du för en demo/MVP?
```

## **FÖRVÄNTAT SVAR FRÅN AI:**

```
3 ALTERNATIV FÖR AUTHENTICATION:

═══════════════════════════════════════════════════════════

ALTERNATIV 1: Hårdkodad admin med session (ENKLAST - REKOMMENDERAD FÖR DEMO)

Implementation:
- Hårdkodad email/lösenord i appsettings.json
- ASP.NET Core Authentication med Cookie
- Blazor AuthenticationStateProvider
- [Authorize] attribute på admin-sidor

Komplexitet: ⭐ (1/5) - Minimal setup
Säkerhet: ⭐⭐⭐ (3/5) - Bra för demo, inte produktion
Performance: ⭐⭐⭐⭐⭐ (5/5) - Mycket snabb
Maintainbarhet: ⭐⭐⭐ (3/5) - Enkel men ej skalbar

Trade-offs:
✅ Snabb att implementera (~20 min)
✅ Inbyggt i .NET
✅ Perfekt för demo/MVP
❌ Endast en admin-användare
❌ Lösenord i config (ej produktion-safe)

═══════════════════════════════════════════════════════════

ALTERNATIV 2: ASP.NET Core Identity (FULL LÖSNING)

Implementation:
- EF Core Identity med användartabeller
- Password hashing (bcrypt)
- Role-based authorization
- User management UI

Komplexitet: ⭐⭐⭐⭐ (4/5) - Mycket boilerplate
Säkerhet: ⭐⭐⭐⭐⭐ (5/5) - Production-ready
Performance: ⭐⭐⭐⭐ (4/5) - Bra med caching
Maintainbarhet: ⭐⭐⭐⭐ (4/5) - Standardlösning

Trade-offs:
✅ Production-ready från dag 1
✅ Stöd för flera admins
✅ Industry standard
❌ 2-3 timmar setup
❌ Overkill för demo
❌ Mycket boilerplate-kod

═══════════════════════════════════════════════════════════

ALTERNATIV 3: JWT Tokens (API-FOCUSED)

Implementation:
- JWT token generation vid login
- Token validation på API-requests
- Refresh token pattern
- Blazor client-side token storage

Komplexitet: ⭐⭐⭐ (3/5) - Medelsvår
Säkerhet: ⭐⭐⭐⭐ (4/5) - Bra med rätt implementation
Performance: ⭐⭐⭐⭐ (4/5) - Stateless = skalbart
Maintainbarhet: ⭐⭐⭐ (3/5) - Token management kan bli komplext

Trade-offs:
✅ Skalbart (stateless)
✅ Bra för API-heavy apps
✅ Modern approach
❌ Mer kod än alternativ 1
❌ Token refresh complexity
❌ Overkill för enkel admin-app

═══════════════════════════════════════════════════════════

REKOMMENDATION FÖR DEMO:

Alternativ 1 (Hårdkodad admin med Cookie Authentication)

Motivering:
- Kan byggas på ~20 minuter
- Visar authentication-koncept tydligt
- Tillräckligt säkert för demo
- Lätt att förstå för åhörare
- Kan uppgraderas till Identity senare (slice 6 i backlog)

NÄSTA STEG: Implementera alternativ 1 med TDD
```

## **DU VÄLJER:**
```
"Perfekt! Vi kör alternativ 1 - hårdkodad admin med Cookie auth.
Enkelt, tydligt, snabbt.

Nu går vi till steg 4: TDD."
```

---

# 🧪 STEG 4: TDD - Tester + Implementation (5 min)

## **PROMPT TILL AI:**

```
NU KODAR VI (planeringen är klar).

Implementera Admin Login med TDD.

KONTEXT:
- Blazor WebApp (.NET 10)
- Cookie Authentication
- Hårdkodad admin: admin@example.com / Admin123!
- AuthenticationStateProvider för Blazor

Skriv tester + implementation i ett svep:

TESTER FÖR:
- Korrekt login → Autentiserad
- Felaktigt lösenord → Unauthorized
- Felaktig email → Unauthorized
- Null inputs → Valideringsfel
- XSS i inputs → Saniterad

IMPLEMENTATION:
- AuthService (login-logik)
- Blazor Login-page
- AuthenticationStateProvider
- [Authorize] attribute

Krav:
- SRP (varje klass = en uppgift)
- DRY (ingen upprepad logik)
- SoC (separation: Auth logic vs UI)
- KISS (så enkelt som möjligt)

Alla tester ska bli gröna.
```

## **FÖRVÄNTAT SVAR FRÅN AI:**

AI skapar nu:
1. `Services/AuthService.cs` - Login-logik
2. `Services/AuthService.Tests.cs` - Unit tests
3. `Components/Pages/Login.razor` - Login-sida
4. `Authentication/CustomAuthStateProvider.cs` - Blazor auth
5. `Program.cs` - Konfiguration

---

# ✅ STEG 5: Manuell Test (3 min)

```bash
# Starta appen
dotnet run

# Öppna: https://localhost:5001/login

# Testa manuellt:
1. Logga in med admin@example.com / Admin123! → Ska fungera
2. Logga in med fel lösen → Ska ge felmeddelande
3. Testa ogiltigt email → Ska validera
4. Klicka logout → Ska logga ut
```

---

# 🔧 STEG 6: Refaktorera (3 min)

## **PROMPT TILL AI:**

```
Granska koden mot checklist:

1. Testerna OK?
   - Alla gröna?
   - Edge cases täckta?

2. Koden läsbar?
   - SRP (varje metod = en uppgift)?
   - DRY (ingen upprepad logik)?
   - KISS (enkel som möjligt)?

3. Säkerhet?
   - XSS-skydd?
   - SQL injection-skydd?
   - Lösenord aldrig i plaintext?

4. Nästa person förstår?
   - Kommentarer där behövs?
   - Tydliga namn?

Föreslå förbättringar.
```

---

# 📝 STEG 7: Commit (2 min)

```bash
git add .
git commit -m "feat: Add admin authentication with cookie-based login

✅ Secure login with hardcoded admin credentials
✅ Cookie-based session management
✅ AuthenticationStateProvider for Blazor
✅ [Authorize] attribute protection
✅ 5 passing tests (edge cases covered)

User story: As admin I want to login securely
Slice 1/5 complete - ready for next slice (List Subscribers)

TDD: All tests green ✅"
```

---

# 🎯 RESULTAT EFTER SLICE 1

## **Vad vi har byggt (20 min):**
✅ Admin kan logga in
✅ Session management
✅ Protected routes
✅ 5 gröna tester
✅ Production-ready (för demo)

## **Nästa slice i backlog:**
2. Lista Prenumeranter (Read)
3. Lägg till Prenumerant (Create)
4. Ta bort Prenumerant (Delete)
5. Email-validering (Quality)

## **Repeat processen för nästa slice!**

---

# 📊 TOTAL TIDSLINJE (Slice 1)

| Steg | Tid | Du gör | AI gör |
|------|-----|--------|--------|
| 1. Förtydliga | 2 min | Skriv spec själv | - |
| 2. Backlog | 3 min | Fråga AI | AI föreslår slices |
| 3. Arkitektur | 3 min | Fråga AI | AI ger 3 alternativ |
| 4. TDD | 5 min | Prompt AI | AI skriver tester + kod |
| 5. Manuell test | 3 min | Testa själv | - |
| 6. Refactor | 3 min | Fråga AI | AI granskar |
| 7. Commit | 2 min | Commit själv | - |

**Total: ~20 minuter → Production-ready slice! 🚀**

---

# 🎤 DEMO-TIPS

### **Vad du VISAR:**
1. **Steg 1:** Din spec (skriven på förhand)
2. **Steg 2:** Prompt → AI svarar med slices
3. **Steg 3:** Prompt → AI ger 3 alternativ → du väljer
4. **Steg 4:** Prompt → AI skriver kod (visa resultatet)
5. **Steg 5:** Kör tester live → GRÖNA
6. **Steg 6:** Prompt → AI granskar
7. **Steg 7:** Commit-meddelande

### **Vad du SÄGER:**
```
"Detta är inte AI som kodar ÅT dig.
Detta är AI som kodar MED dig.

Du tänker strategiskt (VAD ska vi bygga).
AI kodar taktiskt (HUR bygger vi det).

20 minuter från idé till production-ready kod.
DET är AI-Driven Development!"
```

---

**LYCKA TILL MED DEMON! 💪**
