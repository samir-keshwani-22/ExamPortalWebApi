using System.Collections.Generic;

namespace ExamPortal.API.Models.Common;
/// <summary>
/// Represents a paged response containing paging information and a list of items.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedResponse<T>
{
    /// <summary>
    /// Paging information (pageIndex, pageSize, totalRecords, totalPages)
    /// </summary>
    public Paging Paging { get; set; } = new();

    /// <summary>
    /// List of paged items (e.g., exams, questions)
    /// </summary>
    public List<T> Items { get; set; } = new();
}

