using System;
using System.Collections.Generic;

namespace Rss.Web.Models
{
    public class IndexPostViewModel
    {
        public List<News> News { get; set; }
        public int Pages { get; set; }

        public IndexPostViewModel()
        {
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
}
