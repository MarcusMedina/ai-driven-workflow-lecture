<?php

namespace TodoFilter\Tests;

use PHPUnit\Framework\TestCase;
use TodoFilter\Todo;
use TodoFilter\TodoFilterService;

/**
 * Tests for TodoFilterService (PHP)
 * Using PHPUnit framework
 *
 * Install: composer install
 * Run: ./vendor/bin/phpunit
 *
 * This demonstrates TDD approach - same tests as C# and JavaScript versions
 */
class TodoFilterServiceTest extends TestCase
{
    private TodoFilterService $service;

    protected function setUp(): void
    {
        $this->service = new TodoFilterService();
    }

    // FilterByStatus Tests

    public function testFilterByStatusReturnsFilteredListWhenStatusMatches(): void
    {
        // Arrange
        $todos = [
            new Todo(1, 'Task 1', 'done'),
            new Todo(2, 'Task 2', 'pending'),
            new Todo(3, 'Task 3', 'done'),
        ];

        // Act
        $result = $this->service->filterByStatus($todos, 'done');

        // Assert
        $this->assertCount(2, $result);
        foreach ($result as $todo) {
            $this->assertEquals('done', $todo->status);
        }
    }

    public function testFilterByStatusReturnsEmptyListWhenNoMatches(): void
    {
        // Arrange
        $todos = [
            new Todo(1, 'Task 1', 'done'),
            new Todo(2, 'Task 2', 'pending'),
        ];

        // Act
        $result = $this->service->filterByStatus($todos, 'archived');

        // Assert
        $this->assertEmpty($result);
    }

    public function testFilterByStatusReturnsEmptyListWhenInputIsEmpty(): void
    {
        // Act
        $result = $this->service->filterByStatus([], 'done');

        // Assert
        $this->assertEmpty($result);
    }

    public function testFilterByStatusReturnsEmptyListWhenStatusIsNull(): void
    {
        // Arrange
        $todos = [new Todo(1, 'Task 1', 'done')];

        // Act
        $result = $this->service->filterByStatus($todos, null);

        // Assert
        $this->assertEmpty($result);
    }

    public function testFilterByStatusIsCaseInsensitive(): void
    {
        // Arrange - users expect "Done" = "done"
        $todos = [
            new Todo(1, 'Task 1', 'Done'),
            new Todo(2, 'Task 2', 'DONE'),
            new Todo(3, 'Task 3', 'done'),
        ];

        // Act
        $result = $this->service->filterByStatus($todos, 'done');

        // Assert
        $this->assertCount(3, $result);
    }

    // FilterByDateAfter Tests

    public function testFilterByDateAfterReturnsTodosCreatedAfterCutoffDate(): void
    {
        // Arrange
        $cutoffDate = new \DateTime('2024-01-01');
        $todos = [
            new Todo(1, 'Old', 'done', new \DateTime('2023-12-31')),
            new Todo(2, 'New', 'done', new \DateTime('2024-01-02')),
            new Todo(3, 'Newer', 'done', new \DateTime('2024-01-15')),
        ];

        // Act
        $result = $this->service->filterByDateAfter($todos, $cutoffDate);

        // Assert
        $this->assertCount(2, $result);
        foreach ($result as $todo) {
            $this->assertGreaterThan($cutoffDate, $todo->createdAt);
        }
    }

    public function testFilterByDateAfterReturnsEmptyListWhenDateIsNull(): void
    {
        // Arrange
        $todos = [new Todo(1, 'Task', 'done')];

        // Act
        $result = $this->service->filterByDateAfter($todos, null);

        // Assert
        $this->assertEmpty($result);
    }

    // FilterByTitleContains Tests

    public function testFilterByTitleContainsReturnsTodosWithMatchingTitle(): void
    {
        // Arrange
        $todos = [
            new Todo(1, 'Buy milk', 'pending'),
            new Todo(2, 'Buy bread', 'pending'),
            new Todo(3, 'Clean house', 'pending'),
        ];

        // Act
        $result = $this->service->filterByTitleContains($todos, 'buy');

        // Assert
        $this->assertCount(2, $result);
        foreach ($result as $todo) {
            $this->assertStringContainsStringIgnoringCase('buy', $todo->title);
        }
    }

    public function testFilterByTitleContainsIsCaseInsensitive(): void
    {
        // Arrange
        $todos = [
            new Todo(1, 'BUY milk', 'pending'),
            new Todo(2, 'buy bread', 'pending'),
        ];

        // Act
        $result = $this->service->filterByTitleContains($todos, 'BuY');

        // Assert
        $this->assertCount(2, $result);
    }

    public function testFilterByTitleContainsReturnsEmptyListWhenInputsAreNull(): void
    {
        // Act & Assert
        $this->assertEmpty($this->service->filterByTitleContains([], null));
        $this->assertEmpty($this->service->filterByTitleContains([], ''));
    }
}
