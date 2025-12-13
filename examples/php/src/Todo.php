<?php

namespace TodoFilter;

/**
 * Todo model
 * Represents a single todo item
 */
class Todo
{
    public int $id;
    public string $title;
    public string $status;
    public \DateTime $createdAt;

    public function __construct(
        int $id,
        string $title,
        string $status,
        ?\DateTime $createdAt = null
    ) {
        $this->id = $id;
        $this->title = $title;
        $this->status = $status;
        $this->createdAt = $createdAt ?? new \DateTime();
    }
}
