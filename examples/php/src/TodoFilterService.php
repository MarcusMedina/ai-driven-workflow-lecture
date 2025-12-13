<?php

namespace TodoFilter;

/**
 * TodoFilter Service (PHP)
 * Demonstrates: SRP, DRY, KISS principles
 *
 * This is the SAME logic as C# and JavaScript versions,
 * showing that AI-Driven Workflow works across languages
 */
class TodoFilterService
{
    /**
     * Filters todos by status (case-insensitive)
     *
     * @param Todo[] $todos List of todos to filter
     * @param string|null $status Status to filter by
     * @return Todo[] Filtered list
     */
    public function filterByStatus(array $todos, ?string $status): array
    {
        // Edge case: null or empty inputs
        if (empty($todos) || empty($status)) {
            return [];
        }

        // Case-insensitive matching - users expect "Done" = "done"
        return array_values(array_filter(
            $todos,
            fn(Todo $todo) => strcasecmp($todo->status, $status) === 0
        ));
    }

    /**
     * Filters todos created after a specific date
     *
     * @param Todo[] $todos List of todos
     * @param \DateTime|null $date Cutoff date
     * @return Todo[] Filtered list
     */
    public function filterByDateAfter(array $todos, ?\DateTime $date): array
    {
        if (empty($todos) || $date === null) {
            return [];
        }

        return array_values(array_filter(
            $todos,
            fn(Todo $todo) => $todo->createdAt > $date
        ));
    }

    /**
     * Filters todos by title containing search term (case-insensitive)
     *
     * @param Todo[] $todos List of todos
     * @param string|null $searchTerm Term to search for
     * @return Todo[] Filtered list
     */
    public function filterByTitleContains(array $todos, ?string $searchTerm): array
    {
        if (empty($todos) || empty($searchTerm)) {
            return [];
        }

        return array_values(array_filter(
            $todos,
            fn(Todo $todo) => stripos($todo->title, $searchTerm) !== false
        ));
    }
}
