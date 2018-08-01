using System;
using System.Collections.Generic;

namespace Rss.Web.Models
{
    public class IndexPostViewModel
    {
        public List<News> News { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SourceViewModel SourceViewModel { get; set; }
        public SortViewModel SortViewModel { get; set; }
        public IndexPostViewModel()
        {
            SourceViewModel = new SourceViewModel();
            News = new List<News>();
        }
    }

    public sealed class News
    {
        public string Source { get; set; }
        public string Tittle { get; set; }
        public string Description { get; set; }
        public DateTimeOffset PublishDate { get; set; }
    }

    public sealed class PageViewModel
    {
        public int PageNumber { get; }
        public int TotalPages { get; }

        public PageViewModel(int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }

    public sealed class SortViewModel
    {
        public SortState SortType { get; }

        public SortViewModel(SortState sortOrder)
        {
            SortType = sortOrder;
        }
    }

    public enum SortState
    {
        DateOrder,
        SourceOrder
    }
}
