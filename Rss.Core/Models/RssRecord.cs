using System;
using System.ComponentModel.DataAnnotations;

namespace Rss.Core.Models
{
    public sealed class RssRecord
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(512)]
        public string Title { get; set; }

        [Required]
        public DateTimeOffset PublishDate { get; set; }

        public string Description { get; set; }

        public string NewsUrl { get; set; }

        public string SourceUrl { get; set; }

        public string NameSource { get; set; }
    }
}
