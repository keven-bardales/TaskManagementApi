using System;

namespace TaskManagementApi.Features.Tasks.Domain.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public DateTime? DueDate { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime UpdatedAtUtc { get; private set; }

        // EF Core constructor
        protected TaskEntity() { }

        // Domain constructor
        public TaskEntity(string title, string? description = null, DateTime? dueDate = null)
        {
            Id = Guid.NewGuid();
            SetTitle(title);
            Description = description;
            IsCompleted = false;
            DueDate = dueDate;
            CreatedAtUtc = DateTime.UtcNow;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        // Domain methods for encapsulation
        public void SetTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty", nameof(title));

            Title = title;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void SetDescription(string? description)
        {
            Description = description;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void SetDueDate(DateTime? dueDate)
        {
            DueDate = dueDate;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void MarkAsCompleted()
        {
            IsCompleted = true;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void MarkAsIncomplete()
        {
            IsCompleted = false;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void Update(string? title = null, string? description = null, bool? isCompleted = null, DateTime? dueDate = null)
        {
            if (!string.IsNullOrWhiteSpace(title))
                SetTitle(title);

            if (description != null)
                SetDescription(description);

            if (isCompleted.HasValue)
            {
                if (isCompleted.Value)
                    MarkAsCompleted();
                else
                    MarkAsIncomplete();
            }

            if (dueDate.HasValue)
                SetDueDate(dueDate);
        }
    }
}