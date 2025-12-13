# Code Principles - SRP, DRY, SoC, KISS

> Fyra principer som gör din kod professionell - med exempel i C#

---

## 🎯 Varför principer?

AI kan generera kod som **fungerar**.
Men **fungerar** ≠ **bra**.

Dessa 4 principer gör skillnaden mellan:
- Kod som funkar idag → Kod som funkar om 2 år
- Kod du förstår → Kod alla förstår
- Kod du kan ändra → Kod du vågar ändra

**Använd dessa när du promptar AI:**
```
Implementera [X] med fokus på:
- SRP: En uppgift per metod
- DRY: Ingen upprepad logik
- SoC: Tydlig separation av ansvar
- KISS: Så enkelt som möjligt
```

---

## 1. SRP - Single Responsibility Principle

### Definition:
> **En klass/metod ska ha EN anledning att ändras.**
>
> Med andra ord: Gör EN sak. Gör den bra.

### Test:
Kan du beskriva metoden utan att använda "och"?
- ✅ "Filtrerar todos"
- ❌ "Filtrerar todos OCH sorterar OCH sparar"

---

### ❌ Exempel: Bryter mot SRP

```csharp
public class TodoManager
{
    public void ProcessTodo(Todo todo)
    {
        // Validerar
        if (string.IsNullOrEmpty(todo.Title))
            throw new ArgumentException("Title required");

        // Sparar till databas
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("INSERT INTO Todos...", connection);
        command.ExecuteNonQuery();

        // Skickar email
        var smtp = new SmtpClient();
        smtp.Send(new MailMessage("todo@example.com", "New todo created"));

        // Loggar
        Console.WriteLine($"Todo created: {todo.Title}");
    }
}
```

**Problem:**
- Gör 4 saker: validering, databas, email, logging
- Ändra email-logik → måste ändra denna metod
- Ändra databas → måste ändra denna metod
- Svår att testa (måste mocka databas + SMTP + console)

---

### ✅ Exempel: Följer SRP

```csharp
// Varje klass har ETT ansvar
public class TodoValidator
{
    public bool IsValid(Todo todo)
    {
        return !string.IsNullOrEmpty(todo.Title);
    }
}

public class TodoRepository
{
    public void Save(Todo todo)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("INSERT INTO Todos...", connection);
        command.ExecuteNonQuery();
    }
}

public class TodoNotifier
{
    public void NotifyCreated(Todo todo)
    {
        var smtp = new SmtpClient();
        smtp.Send(new MailMessage("todo@example.com", $"New todo: {todo.Title}"));
    }
}

public class TodoLogger
{
    public void LogCreation(Todo todo)
    {
        Console.WriteLine($"Todo created: {todo.Title}");
    }
}

// Coordinator - orchestrerar ansvaren
public class TodoService
{
    private readonly TodoValidator _validator;
    private readonly TodoRepository _repository;
    private readonly TodoNotifier _notifier;
    private readonly TodoLogger _logger;

    public TodoService(TodoValidator validator, TodoRepository repository,
                       TodoNotifier notifier, TodoLogger logger)
    {
        _validator = validator;
        _repository = repository;
        _notifier = notifier;
        _logger = logger;
    }

    public void CreateTodo(Todo todo)
    {
        if (!_validator.IsValid(todo))
            throw new ArgumentException("Invalid todo");

        _repository.Save(todo);
        _notifier.NotifyCreated(todo);
        _logger.LogCreation(todo);
    }
}
```

**Fördelar:**
✅ Varje klass gör EN sak
✅ Lätt att testa varje del separat
✅ Ändra email-logik → ändra bara TodoNotifier
✅ Lätt att förstå vad varje klass gör

---

### AI-Prompt för SRP:

```
Granska denna kod för SRP:
- Gör varje metod/klass EN sak?
- Kan jag beskriva den utan "och"?
- Hur många anledningar finns att ändra den?

Om den bryter mot SRP - föreslå refactoring.

[KOD]
```

---

## 2. DRY - Don't Repeat Yourself

### Definition:
> **Varje bit kunskap ska ha EN representation i systemet.**
>
> Med andra ord: Kopiera inte kod. Bryt ut gemensam logik.

### Test:
Måste du ändra samma logik på flera ställen?
- ✅ Nej → Bra (DRY)
- ❌ Ja → Dåligt (WET - Write Everything Twice)

---

### ❌ Exempel: Bryter mot DRY (WET)

```csharp
public class TodoService
{
    public List<Todo> GetActiveTodos()
    {
        var todos = new List<Todo>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Todos WHERE Status = 'Active'", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            todos.Add(new Todo
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Status = reader.GetString(2)
            });
        }
        return todos;
    }

    public List<Todo> GetCompletedTodos()
    {
        var todos = new List<Todo>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Todos WHERE Status = 'Completed'", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            todos.Add(new Todo
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Status = reader.GetString(2)
            });
        }
        return todos;
    }

    public List<Todo> GetPendingTodos()
    {
        var todos = new List<Todo>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand("SELECT * FROM Todos WHERE Status = 'Pending'", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            todos.Add(new Todo
            {
                Id = reader.GetInt32(0),
                Title = reader.GetString(1),
                Status = reader.GetString(2)
            });
        }
        return todos;
    }
}
```

**Problem:**
- Samma databas-logik 3 gånger
- Ändra hur Todo skapas → ändra 3 ställen
- Ändra connection-hantering → ändra 3 ställen
- Bug i en = bug i alla (kanske)

---

### ✅ Exempel: Följer DRY

```csharp
public class TodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Gemensam metod - EN plats för databas-logik
    private List<Todo> GetTodosByStatus(string status)
    {
        var todos = new List<Todo>();
        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var command = new SqlCommand(
            "SELECT * FROM Todos WHERE Status = @Status",
            connection
        );
        command.Parameters.AddWithValue("@Status", status);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            todos.Add(MapTodoFromReader(reader));
        }

        return todos;
    }

    // Gemensam mapping - EN plats för Todo-skapande
    private Todo MapTodoFromReader(SqlDataReader reader)
    {
        return new Todo
        {
            Id = reader.GetInt32(0),
            Title = reader.GetString(1),
            Status = reader.GetString(2)
        };
    }

    // Publika metoder använder gemensam logik
    public List<Todo> GetActiveTodos() => GetTodosByStatus("Active");
    public List<Todo> GetCompletedTodos() => GetTodosByStatus("Completed");
    public List<Todo> GetPendingTodos() => GetTodosByStatus("Pending");
}
```

**Fördelar:**
✅ Databas-logik på EN plats
✅ Ändra mapping → ändra EN metod
✅ Ändra query-logik → ändra EN metod
✅ Bug-fix påverkar alla på en gång

---

### AI-Prompt för DRY:

```
Granska denna kod för upprepning:
- Finns samma logik på flera ställen?
- Kan gemensam logik brytas ut?
- Finns kopierad kod?

Föreslå refactoring för att eliminera upprepning.

[KOD]
```

---

## 3. SoC - Separation of Concerns

### Definition:
> **Olika ansvar ska vara i olika moduler/klasser.**
>
> Med andra ord: Databas-logik med databas. UI-logik med UI. Business-logik separat.

### Test:
Måste du ändra UI-kod för att ändra databas?
- ✅ Nej → Bra (separerat)
- ❌ Ja → Dåligt (tätt kopplat)

---

### ❌ Exempel: Dålig separation (allt i en klass)

```csharp
public class TodoForm
{
    private TextBox titleTextBox;
    private Button saveButton;

    private void SaveButton_Click(object sender, EventArgs e)
    {
        // UI-logik
        var title = titleTextBox.Text;

        // Validering (business logic)
        if (string.IsNullOrEmpty(title))
        {
            MessageBox.Show("Title required!");
            return;
        }

        if (title.Length > 100)
        {
            MessageBox.Show("Title too long!");
            return;
        }

        // Databas-logik
        using var connection = new SqlConnection("Server=...");
        connection.Open();
        var command = new SqlCommand(
            "INSERT INTO Todos (Title, Status) VALUES (@Title, 'Pending')",
            connection
        );
        command.Parameters.AddWithValue("@Title", title);
        command.ExecuteNonQuery();

        // UI-logik igen
        MessageBox.Show("Todo saved!");
        titleTextBox.Clear();
    }
}
```

**Problem:**
- UI, validering, och databas i samma metod
- Kan inte testa validering utan UI
- Kan inte byta databas utan att röra UI
- Kan inte återanvända validering i annat UI

---

### ✅ Exempel: Bra separation

```csharp
// BUSINESS LOGIC (Models + Validation)
public class Todo
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
}

public class TodoValidator
{
    public ValidationResult Validate(string title)
    {
        if (string.IsNullOrEmpty(title))
            return ValidationResult.Error("Title required");

        if (title.Length > 100)
            return ValidationResult.Error("Title too long");

        return ValidationResult.Success();
    }
}

public class ValidationResult
{
    public bool IsValid { get; set; }
    public string ErrorMessage { get; set; }

    public static ValidationResult Success() =>
        new ValidationResult { IsValid = true };

    public static ValidationResult Error(string message) =>
        new ValidationResult { IsValid = false, ErrorMessage = message };
}

// DATA LAYER (Databas)
public class TodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void Save(Todo todo)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var command = new SqlCommand(
            "INSERT INTO Todos (Title, Status) VALUES (@Title, @Status)",
            connection
        );
        command.Parameters.AddWithValue("@Title", todo.Title);
        command.Parameters.AddWithValue("@Status", todo.Status);
        command.ExecuteNonQuery();
    }
}

// SERVICE LAYER (Koordinerar business logic + data)
public class TodoService
{
    private readonly TodoValidator _validator;
    private readonly TodoRepository _repository;

    public TodoService(TodoValidator validator, TodoRepository repository)
    {
        _validator = validator;
        _repository = repository;
    }

    public ValidationResult CreateTodo(string title)
    {
        var validationResult = _validator.Validate(title);
        if (!validationResult.IsValid)
            return validationResult;

        var todo = new Todo { Title = title, Status = "Pending" };
        _repository.Save(todo);

        return ValidationResult.Success();
    }
}

// UI LAYER (Bara UI-logik)
public class TodoForm
{
    private TextBox titleTextBox;
    private Button saveButton;
    private readonly TodoService _todoService;

    public TodoForm(TodoService todoService)
    {
        _todoService = todoService;
    }

    private void SaveButton_Click(object sender, EventArgs e)
    {
        var title = titleTextBox.Text;
        var result = _todoService.CreateTodo(title);

        if (!result.IsValid)
        {
            MessageBox.Show(result.ErrorMessage);
            return;
        }

        MessageBox.Show("Todo saved!");
        titleTextBox.Clear();
    }
}
```

**Fördelar:**
✅ Validering testbar utan UI eller databas
✅ Byt databas → ändra bara TodoRepository
✅ Ändra UI → ändra bara TodoForm
✅ Återanvänd validering i web UI, mobile, API, etc

---

### AI-Prompt för SoC:

```
Granska arkitekturen för Separation of Concerns:
- Är UI, business logic, och data separerade?
- Kan varje lager testas separat?
- Finns beroenden mellan lager som borde brytas?

Föreslå förbättringar.

[KOD]
```

---

## 4. KISS - Keep It Simple, Stupid

### Definition:
> **Den enklaste lösningen som fungerar är bäst.**
>
> Med andra ord: Gör det inte mer komplext än nödvändigt.

### Test:
Kan det göras enklare och fortfarande fungera?
- ✅ Nej → Bra (KISS)
- ❌ Ja → Förenkla!

---

### ❌ Exempel: Över-komplicerat

```csharp
// "Clever" implementation med reflection och caching
public class TodoFilterEngine
{
    private readonly Dictionary<string, Func<Todo, bool>> _filterCache;
    private readonly MethodInfo _filterMethod;

    public TodoFilterEngine()
    {
        _filterCache = new Dictionary<string, Func<Todo, bool>>();
        _filterMethod = typeof(TodoFilterEngine)
            .GetMethod("FilterPredicate", BindingFlags.NonPublic | BindingFlags.Instance);
    }

    public List<Todo> Filter(List<Todo> todos, string status)
    {
        if (!_filterCache.ContainsKey(status))
        {
            var parameter = Expression.Parameter(typeof(Todo), "t");
            var property = Expression.Property(parameter, "Status");
            var constant = Expression.Constant(status, typeof(string));
            var equality = Expression.Equal(property, constant);
            var lambda = Expression.Lambda<Func<Todo, bool>>(equality, parameter);
            _filterCache[status] = lambda.Compile();
        }

        return todos.Where(_filterCache[status]).ToList();
    }
}
```

**Problem:**
- Över-engineerat för en enkel filter-operation
- Svår att förstå
- Svår att debugga
- Reflection = performance-kostnad
- Fungerar - men VARFÖR så komplext?

---

### ✅ Exempel: KISS (enkelt och tydligt)

```csharp
public class TodoFilter
{
    public List<Todo> FilterByStatus(List<Todo> todos, string status)
    {
        if (todos == null || string.IsNullOrEmpty(status))
            return new List<Todo>();

        return todos
            .Where(t => t.Status.Equals(status, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}
```

**Fördelar:**
✅ Lätt att förstå
✅ Lätt att testa
✅ Lätt att debugga
✅ Gör exakt vad som behövs, inget mer

---

### Exempel: När ska man INTE förenkla?

**Scenario:** Du behöver komplexitet för framtida krav

❌ **Fel approach:**
```csharp
// "Vi kanske behöver det sen"
public interface ITodoFilter<T> where T : IFilterable
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}
```
**Problem:** YAGNI (You Ain't Gonna Need It) - bygger för hypotetisk framtid

✅ **Rätt approach:**
```csharp
// Bygg för NU. Refactorera senare om det VERKLIGEN behövs.
public List<Todo> FilterByStatus(List<Todo> todos, string status)
{
    return todos.Where(t => t.Status == status).ToList();
}
```

**När komplexitet är OK:**
- Du HAR kravet nu (inte "kanske sen")
- Enkelhet skulle duplicera mycket kod
- Prestanda kräver det (med profiling-bevis)

**Men som regel:** Börja enkelt. Komplexitet kan läggas till. Enkelhet är svårare att återfå.

---

### AI-Prompt för KISS:

```
Granska denna kod för onödig komplexitet:
- Kan den förenklas?
- Finns "clever" kod som kan skrivas enklare?
- Är all komplexitet motiverad?

Föreslå enklare lösning om möjligt.

[KOD]
```

---

## 🎯 Sammanfattning

### SRP (Single Responsibility Principle)
> En klass/metod = EN uppgift

**Test:** Kan du beskriva den utan "och"?

### DRY (Don't Repeat Yourself)
> Kopiera inte logik. Bryt ut gemensam kod.

**Test:** Måste du ändra samma sak på flera ställen?

### SoC (Separation of Concerns)
> UI, business logic, data - separerade.

**Test:** Måste du ändra flera lager för en ändring?

### KISS (Keep It Simple, Stupid)
> Enklast som fungerar = bäst.

**Test:** Kan det göras enklare?

---

## 🚀 Använd med AI

### Prompt-template:
```
Implementera [FEATURE] med fokus på:

1. SRP - varje metod gör EN sak
2. DRY - ingen upprepad logik
3. SoC - tydlig separation av ansvar (UI/business/data)
4. KISS - så enkelt som möjligt

Förklara hur koden följer dessa principer.
```

### Code review med AI:
```
Granska koden mot SRP, DRY, SoC, KISS:

1. SRP: Gör varje metod/klass EN sak?
2. DRY: Finns upprepad logik?
3. SoC: Är ansvaren separerade?
4. KISS: Kan det förenklas?

[KOD]
```

---

## 📚 Nästa steg

- Använd [Quality Checklist](QUALITY-CHECKLIST.md) för att verifiera principer
- Se [TodoFilter demo](../demos/TodoFilter/) för praktiskt exempel
- Läs [Workflow](WORKFLOW.md) för att integrera principer i din process

---

**Skapad av Marcus Ackre Medina**
📚 [AI-Driven Development på GitHub](https://github.com/MarcusMedina/ai-driven-workflow)
