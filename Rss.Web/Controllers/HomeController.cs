using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rss.Core;
using Rss.Web.Models;

namespace Rss.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly RssRecordService _recordService;

        public HomeController(RssRecordService recordService)
        {
            _recordService = recordService;
        }

        private readonly int _pageSize = 10;

        public async Task<IActionResult> IndexGet(string sourceName, CancellationToken cancellationToken, int page = 1, SortState sortOrder = SortState.DateOrder)
        {
            var records = await _recordService.GetAsync(cancellationToken);
            var sourceViewModel = new SourceViewModel();
            var index = 1;

            foreach (var source in records.Select(i => i.NameSource).GroupBy(i => i))
            {
                sourceViewModel.Sources.Add(new Source
                {
                    Id = index,
                    Name = source.Key
                });

                index++;
            }
            if (!string.IsNullOrEmpty(sourceName) && !sourceName.Equals("Все"))
            {
                records = records.Where(i => i.NameSource == sourceName).ToList();
                sourceViewModel.Select(sourceName);
            }

            switch (sortOrder)
            {
                case SortState.SourceOrder:
                    records = records.OrderByDescending(i => i.NameSource).ToList();
                    break;
                default:
                    records = records.OrderByDescending(i => i.PublishDate).ToList();
                    break;
            }

            var count = records.Count;
            var news = records.Skip((page - 1) * _pageSize).Take(_pageSize).ToList();


            var viewModel = new IndexPostViewModel
            {
                PageViewModel = new PageViewModel(count, page, _pageSize),
                News = news.Select(i => new News
                {
                    Source = i.NameSource,
                    PublishDate = i.PublishDate,
                    Tittle = i.Title,
                    Description = i.Description
                }).ToList(),
                SourceViewModel = sourceViewModel,
                SortViewModel = new SortViewModel(sortOrder)
            };

            return View("IndexPost", viewModel);
        }
    }
}
