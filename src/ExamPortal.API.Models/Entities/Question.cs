#nullable enable 

using System;
using System.Collections.Generic;

namespace ExamPortal.API.Models.Entities;

/// <summary>
/// Represents a question within an exam, including its text, type, marks, and related options and answers.
/// </summary>
public class Question
{
    /// <summary>
    /// Gets or sets the unique identifier for the question.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the exam identifier this question belongs to.
    /// </summary>
    public int ExamId { get; set; }

    /// <summary>
    /// Gets or sets the exam entity this question belongs to.
    /// </summary>
    public required Exam Exam { get; set; }

    /// <summary>
    /// Gets or sets the text of the question.
    /// </summary>
    public required string QuestionText { get; set; }

    /// <summary>
    /// Gets or sets the question type (e.g., "MCQ", "Descriptive").
    /// </summary>
    public required string QuestionType { get; set; }

    /// <summary>
    /// Gets or sets the marks assigned to this question.
    /// </summary>

    public int Marks { get; set; }

    /// <summary>
    /// Gets or sets the creation date of the question.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date when the question was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the question.
    /// </summary>
    public int? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who last updated the question.
    /// </summary>
    public int? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the topic of the question (optional).
    /// </summary>
    public string? Topic { get; set; }

    /// <summary>
    /// Gets or sets the difficulty level of the question (optional).
    /// </summary>
    public string? DifficultyLevel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the question is deleted.
    /// </summary>
    public bool? IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets the collection of options for this question.
    /// </summary>
    public ICollection<QuestionOption> Options { get; set; } = new List<QuestionOption>();

    /// <summary>
    /// Gets or sets the collection of answers for this question.
    /// </summary>
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
