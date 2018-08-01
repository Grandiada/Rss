using System;

namespace Rss.Core.Models
{
    public sealed class RssRecord
    {
        public int Id { get; set; }

        public string Tittle { get; set; }

        public DateTimeOffset PublishDate { get; set; }

        public string Description { get; set; }

        public string NewsUrl { get; set; }

        public string SourceUrl { get; set; }

        public string NameSource { get; set; }
    }
}
