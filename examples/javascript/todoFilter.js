/**
 * TodoFilter Service (JavaScript/Node.js)
 * Demonstrates: SRP, DRY, KISS principles
 *
 * This is the SAME logic as C# version, showing that
 * AI-Driven Workflow works across languages
 */

class Todo {
  constructor(id, title, status, createdAt = new Date()) {
    this.id = id;
    this.title = title;
    this.status = status;
    this.createdAt = createdAt;
  }
}

class TodoFilterService {
  /**
   * Filters todos by status (case-insensitive)
   * @param {Todo[]} todos - List of todos to filter
   * @param {string} status - Status to filter by
   * @returns {Todo[]} Filtered list
   */
  filterByStatus(todos, status) {
    // Edge case: null or empty inputs
    if (!todos || !status) {
      return [];
    }

    // Case-insensitive matching - users expect "Done" = "done"
    return todos.filter(
      (t) => t.status.toLowerCase() === status.toLowerCase()
    );
  }

  /**
   * Filters todos created after a specific date
   * @param {Todo[]} todos - List of todos
   * @param {Date} date - Cutoff date
   * @returns {Todo[]} Filtered list
   */
  filterByDateAfter(todos, date) {
    if (!todos || !(date instanceof Date)) {
      return [];
    }

    return todos.filter((t) => t.createdAt > date);
  }

  /**
   * Filters todos by title containing search term (case-insensitive)
   * @param {Todo[]} todos - List of todos
   * @param {string} searchTerm - Term to search for
   * @returns {Todo[]} Filtered list
   */
  filterByTitleContains(todos, searchTerm) {
    if (!todos || !searchTerm) {
      return [];
    }

    return todos.filter((t) =>
      t.title.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }
}

// Export for use in tests or other modules
if (typeof module !== 'undefined' && module.exports) {
  module.exports = { Todo, TodoFilterService };
}
