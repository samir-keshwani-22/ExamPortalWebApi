using System;
using System.Collections.Generic;

namespace ExamPortal.API.Models.Entities;

/// <summary>
/// Represents an exam entity in the system.
/// </summary>
public class Exam
{
    /// <summary>
    /// Gets or sets the unique identifier for the exam.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the exam.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the exam.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the duration of the exam in minutes.
    /// </summary>
    public TimeSpan DurationMinutes { get; set; }

    /// <summary>
    /// Gets or sets the total marks for the exam.
    /// </summary>
    public int? TotalMarks { get; set; }

    /// <summary>
    /// Gets or sets the start date of the exam.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of the exam.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the exam.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date when the exam was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the exam.
    /// </summary>
    public int? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who last updated the exam.
    /// </summary>
    public int? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who deleted the exam.
    /// </summary>
    public int? DeletedBy { get; set; }

    /// <summary>
    /// Gets or sets the date when the exam was deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the exam is deleted.
    /// </summary>
    public bool? IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets the collection of questions associated with the exam.
    /// </summary>

    public ICollection<Question> Questions { get; set; }
}
