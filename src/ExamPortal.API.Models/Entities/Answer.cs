#nullable enable
namespace ExamPortal.API.Models.Entities;

/// <summary>
/// Represents an answer provided for a specific question in an exam.
/// </summary>
public class Answer
{
    /// <summary>
    /// Gets or sets the unique identifier for the answer.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the related question.
    /// </summary>
    public int QuestionId { get; set; }

    /// <summary>
    /// Gets or sets the related question entity.
    /// </summary>
    public required Question Question { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the selected option (if any).
    /// </summary>
    public int? SelectedOptionId { get; set; }

    /// <summary>
    /// Gets or sets the selected option for the answer (if any).
    /// </summary>
    public QuestionOption? SelectedOption { get; set; }
}
