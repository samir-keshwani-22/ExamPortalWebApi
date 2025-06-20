using System.Collections.Generic;

namespace ExamPortal.API.Models.Common
{
    /// <summary>
    /// Represents a paged result with a list of items and the total count of available items.
    /// </summary>
    /// <typeparam name="T">The type of items in the result.</typeparam>
    public class PagedResult<T>
    {
        /// <summary>
        /// Gets or sets the total number of items available.
        /// </summary>
        public int TotalCount { get; set; }
        
        /// <summary>
        /// Gets or sets the list of items for the current page.
        /// </summary>
        public List<T> Items { get; set; } = new();
    }
}