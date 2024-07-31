using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CouriersManagementDb.Models
{
    // Generic class PaginatedList<T> that extends List<T> to support pagination functionality.
    public class PaginatedList<T> : List<T>
    {
        // Current page index.
        public int PageIndex { get; private set; }

        // Total number of pages available.
        public int TotalPages { get; private set; }

        // Constructor to create a paginated list.
        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex; // Set the current page index.
            TotalPages = (int)Math.Ceiling(count / (double)pageSize); // Calculate the total number of pages.

            this.AddRange(items); // Add the items to the current page.
        }

        // Property to check if there is a previous page.
        public bool HasPreviousPage => PageIndex > 1;

        // Property to check if there is a next page.
        public bool HasNextPage => PageIndex < TotalPages;

        // Static method to create an instance of PaginatedList<T> asynchronously.
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync(); // Count the total items in the source.
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync(); // Get the specific page of items.
            return new PaginatedList<T>(items, count, pageIndex, pageSize); // Return the new paginated list.
        }
    }
}
