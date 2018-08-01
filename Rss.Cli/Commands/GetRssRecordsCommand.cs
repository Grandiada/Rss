using CodeHollow.FeedReader;
using Microsoft.Extensions.Configuration;
using Rss.Core;
using Rss.Core.Models;
using Rss.Core.Models.Context;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Rss.Cli.Commands
{
    class GetRssRecordsCommand
    {
        private static RssRecordService _recordService;

        private static IReadOnlyList<RssSource> _rssSources = new List<RssSource>
        {
            new RssSource
            {
                SourceName = "Хабрахабр",
                SourceUrl = "https://habr.com/rss/interesting/"
            },
            new RssSource
            {
                SourceName = "Интерфакс",
                SourceUrl = "https://www.interfax.by/news/feed"
            }
        };

        private sealed class RssSource
        {
            public string SourceName { get; set; }
            public string SourceUrl { get; set; }
        }

        public static async Task RunAsync(CancellationToken cancellationToken)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Func<RssDbContext> contextFactory =
                () => new RssDbContext(config.GetConnectionString("DefaultConnection"));

            _recordService = new RssRecordService(contextFactory);


            foreach (var rssSource in _rssSources)
            {
                Console.WriteLine(rssSource.SourceName);
                var itemsSaved = 0;
                var itemsReaded = 0;

                var text = await Helpers.DownloadAsync(CodeHollow.FeedReader.FeedReader.GetAbsoluteUrl(rssSource.SourceUrl));

                if (text.StartsWith("\n"))
                    text = text.Remove(0, 1);

                var feed = FeedReader.ReadFromString(text);

                foreach (var feedItem in feed.Items)
                {
                    try
                    {
                        await _recordService.AddAsync(new RssRecord()
                        {
                            PublishDate =
                                    !string.IsNullOrEmpty(feedItem.PublishingDateString)
                                        ? DateTimeOffset.Parse(feedItem.PublishingDateString)
                                        : throw new InvalidOperationException("No publish date"),

                            SourceUrl = rssSource.SourceUrl,
                            NameSource = rssSource.SourceName,
                            Description = feedItem.Description,
                            NewsUrl = feedItem.Id,

                            Title = !string.IsNullOrEmpty(feedItem.Title)
                                    ? feedItem.Title
                                    : throw new InvalidOperationException("No publish date")
                        },
                            cancellationToken);

                        itemsSaved++;
                    }
                    catch (DbUpdateException e)
                    {
                        if (!(e.InnerException is SqlException exception && exception.ErrorCode == -2146232060))
                        //Console.WriteLine($"{feedItem.PublishingDateString} already exist in db");
                        {
                            Console.WriteLine(e);
                            throw;
                        }

                    }
                }

                Console.WriteLine($"items readed: {feed.Items.Count}");
                Console.WriteLine($"items saved: {itemsSaved}");
            }

        }
    }
}
