# TodoFilter - JavaScript Example

> Same workflow, different language

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
npm install
```

### Run tests
```bash
npm test
```

### Run tests with coverage
```bash
npm run test:coverage
```

### Watch mode (for development)
```bash
npm run test:watch
```

---

## 📁 Structure

```
javascript/
├── todoFilter.js           # Implementation
├── todoFilter.test.js      # Tests (Jest)
├── package.json           # Dependencies
└── README.md              # This file
```

---

## 🧪 Tests

Same test coverage as C# version:

### FilterByStatus
- ✅ Matching status
- ✅ No matches
- ✅ Empty list
- ✅ Null list
- ✅ Null status
- ✅ Case-insensitive

### FilterByDateAfter
- ✅ Matching dates
- ✅ Null list

### FilterByTitleContains
- ✅ Matching titles
- ✅ Case-insensitive
- ✅ Null inputs

---

## 💡 Key differences from C#

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

### Testing
**C# (xUnit):**
```csharp
[Fact]
public void FilterByStatus_WithMatches_ReturnsFiltered()
{
    Assert.Equal(2, result.Count);
}
```

**JavaScript (Jest):**
```javascript
test('should return filtered list when status matches', () => {
  expect(result).toHaveLength(2);
});
```

---

## 🎓 What stays the same?

1. **Workflow**: Förtydliga → Spec → Intent → Arkitekt → TDD → Test → Refactor
2. **Principles**: SRP, DRY, SoC, KISS
3. **TDD**: Tests first, implementation second
4. **Edge cases**: Null handling, empty lists

**This is the point: The PROCESS is language-independent!**

---

## 📚 Next steps

Try implementing:
- `filterByMultipleStatuses(todos, statuses[])`
- `combineFilters(todos, filters[])`
- `sortByDate(todos, ascending)`

Use the same workflow!

---

**Part of AI-Driven Development Workshop**
Marcus Ackre Medina @ YH Campus Mölndal
