# TodoFilter - PHP Example

> Same workflow, different language (PHP 8.0+)

---

## 🎯 What's the same?

- TDD approach (tests first)
- SRP, DRY, KISS principles
- Edge case handling
- Case-insensitive matching

**The workflow works across languages!**

---

## 🚀 Run the example

### Install dependencies
```bash
composer install
```

### Run tests
```bash
composer test
# or
./vendor/bin/phpunit
```

### Run tests with coverage
```bash
composer run test:coverage
```

---

## 📁 Structure

```
php/
├── src/
│   ├── Todo.php                # Model
│   └── TodoFilterService.php   # Service
├── tests/
│   └── TodoFilterServiceTest.php  # Tests (PHPUnit)
├── composer.json               # Dependencies
├── phpunit.xml                # PHPUnit config
└── README.md                  # This file
```

---

## 🧪 Tests

Same test coverage as C# and JavaScript versions:

### FilterByStatus
- ✅ Matching status
- ✅ No matches
- ✅ Empty list
- ✅ Null status
- ✅ Case-insensitive

### FilterByDateAfter
- ✅ Matching dates
- ✅ Null date

### FilterByTitleContains
- ✅ Matching titles
- ✅ Case-insensitive
- ✅ Null inputs

---

## 💡 Key differences from C# and JavaScript

### Syntax
**C#:**
```csharp
public List<Todo> FilterByStatus(List<Todo> todos, string status)
{
    return todos.Where(t => t.Status == status).ToList();
}
```

**JavaScript:**
```javascript
filterByStatus(todos, status) {
  return todos.filter((t) => t.status === status);
}
```

**PHP:**
```php
public function filterByStatus(array $todos, ?string $status): array
{
    return array_filter(
        $todos,
        fn(Todo $todo) => strcasecmp($todo->status, $status) === 0
    );
}
```

### Type Safety
PHP 8.0+ has:
- Type declarations (`array`, `?string`, `\DateTime`)
- Return type hints (`: array`)
- Nullable types (`?string`)
- Arrow functions (`fn()`)

### Testing
**C# (xUnit):**
```csharp
[Fact]
public void FilterByStatus_WithMatches_ReturnsFiltered() { }
```

**JavaScript (Jest):**
```javascript
test('should return filtered list when status matches', () => { });
```

**PHP (PHPUnit):**
```php
public function testFilterByStatusReturnsFilteredListWhenStatusMatches(): void { }
```

---

## 🎓 What stays the same?

1. **Workflow**: Förtydliga → Spec → Intent → Arkitekt → TDD → Test → Refactor
2. **Principles**: SRP, DRY, SoC, KISS
3. **TDD**: Tests first, implementation second
4. **Edge cases**: Null handling, empty arrays

**This is the point: The PROCESS is language-independent!**

---

## 📚 Next steps

Try implementing:
- `filterByMultipleStatuses(array $todos, array $statuses): array`
- `combineFilters(array $todos, array $filters): array`
- `sortByDate(array $todos, bool $ascending): array`

Use the same workflow!

---

## 🔧 Requirements

- PHP 8.0 or higher
- Composer

---

**Part of AI-Driven Development Workshop**
Marcus Ackre Medina @ YH Campus Mölndal
