using System;

namespace ExamPortal.API.Models.Entities;

/// <summary>
/// Represents an option for a specific exam question.
/// </summary>
public class QuestionOption
{
    /// <summary>
    /// Gets or sets the unique identifier for the option.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the related question.
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Gets or sets the related question entity.
    /// </summary>
    public Question Question { get; set; }

    /// <summary>
    /// Gets or sets the text for this option.
    /// </summary>
    public string OptionText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this option is the correct answer.
    /// </summary>
    public bool IsCorrect { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the option is deleted.
    /// </summary>
    public bool? IsDeleted { get; set; } = false;

    /// <summary>
    /// Gets or sets the creation date of the option.
    /// </summary>

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date when the option was last updated.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who created the option.
    /// </summary>
    public int? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the ID of the user who last updated the option.
    /// </summary>
    public int? UpdatedBy { get; set; }

}
