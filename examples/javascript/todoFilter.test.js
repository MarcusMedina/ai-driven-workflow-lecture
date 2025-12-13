/**
 * Tests for TodoFilterService (JavaScript)
 * Using Jest framework
 *
 * Install: npm install --save-dev jest
 * Run: npm test
 *
 * This demonstrates TDD approach - same tests as C# version
 */

const { Todo, TodoFilterService } = require('./todoFilter');

describe('TodoFilterService', () => {
  let service;

  beforeEach(() => {
    service = new TodoFilterService();
  });

  describe('filterByStatus', () => {
    test('should return filtered list when status matches', () => {
      // Arrange
      const todos = [
        new Todo(1, 'Task 1', 'done'),
        new Todo(2, 'Task 2', 'pending'),
        new Todo(3, 'Task 3', 'done'),
      ];

      // Act
      const result = service.filterByStatus(todos, 'done');

      // Assert
      expect(result).toHaveLength(2);
      expect(result.every((t) => t.status === 'done')).toBe(true);
    });

    test('should return empty list when no matches', () => {
      // Arrange
      const todos = [
        new Todo(1, 'Task 1', 'done'),
        new Todo(2, 'Task 2', 'pending'),
      ];

      // Act
      const result = service.filterByStatus(todos, 'archived');

      // Assert
      expect(result).toHaveLength(0);
    });

    test('should return empty list when input is empty', () => {
      // Act
      const result = service.filterByStatus([], 'done');

      // Assert
      expect(result).toHaveLength(0);
    });

    test('should return empty list when input is null', () => {
      // Act
      const result = service.filterByStatus(null, 'done');

      // Assert
      expect(result).toHaveLength(0);
    });

    test('should return empty list when status is null', () => {
      // Arrange
      const todos = [new Todo(1, 'Task 1', 'done')];

      // Act
      const result = service.filterByStatus(todos, null);

      // Assert
      expect(result).toHaveLength(0);
    });

    test('should be case-insensitive', () => {
      // Arrange - users expect "Done" = "done"
      const todos = [
        new Todo(1, 'Task 1', 'Done'),
        new Todo(2, 'Task 2', 'DONE'),
        new Todo(3, 'Task 3', 'done'),
      ];

      // Act
      const result = service.filterByStatus(todos, 'done');

      // Assert
      expect(result).toHaveLength(3);
    });
  });

  describe('filterByDateAfter', () => {
    test('should return todos created after cutoff date', () => {
      // Arrange
      const cutoffDate = new Date('2024-01-01');
      const todos = [
        new Todo(1, 'Old', 'done', new Date('2023-12-31')),
        new Todo(2, 'New', 'done', new Date('2024-01-02')),
        new Todo(3, 'Newer', 'done', new Date('2024-01-15')),
      ];

      // Act
      const result = service.filterByDateAfter(todos, cutoffDate);

      // Assert
      expect(result).toHaveLength(2);
      expect(result.every((t) => t.createdAt > cutoffDate)).toBe(true);
    });

    test('should return empty list when input is null', () => {
      // Act
      const result = service.filterByDateAfter(null, new Date());

      // Assert
      expect(result).toHaveLength(0);
    });
  });

  describe('filterByTitleContains', () => {
    test('should return todos with matching title', () => {
      // Arrange
      const todos = [
        new Todo(1, 'Buy milk', 'pending'),
        new Todo(2, 'Buy bread', 'pending'),
        new Todo(3, 'Clean house', 'pending'),
      ];

      // Act
      const result = service.filterByTitleContains(todos, 'buy');

      // Assert
      expect(result).toHaveLength(2);
      expect(
        result.every((t) => t.title.toLowerCase().includes('buy'))
      ).toBe(true);
    });

    test('should be case-insensitive', () => {
      // Arrange
      const todos = [
        new Todo(1, 'BUY milk', 'pending'),
        new Todo(2, 'buy bread', 'pending'),
      ];

      // Act
      const result = service.filterByTitleContains(todos, 'BuY');

      // Assert
      expect(result).toHaveLength(2);
    });

    test('should return empty list when inputs are null', () => {
      // Act & Assert
      expect(service.filterByTitleContains(null, 'test')).toHaveLength(0);
      expect(service.filterByTitleContains([], null)).toHaveLength(0);
    });
  });
});
