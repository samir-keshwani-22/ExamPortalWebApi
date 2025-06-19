using System.ComponentModel.DataAnnotations;

namespace ExamPortal.API.Models.Common;

public class Paging
{
    /// <summary>
    /// Current page index (1-based)
    /// </summary>
    [Range(1, long.MaxValue, ErrorMessage = "PageIndex must be greater than 0")]
    public long PageIndex { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
    public long PageSize { get; set; }

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public long TotalRecords { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages { get; set; }

}
