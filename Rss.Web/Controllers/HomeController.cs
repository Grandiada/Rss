using System;
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

        [HttpGet]
        public async Task<IActionResult> IndexGet(CancellationToken cancellationToken)
        {
            var records = await _recordService.GetAsync(cancellationToken);

            if (records == null)
                throw new InvalidOperationException();

            var viewModel = new IndexPostViewModel
            {
                Pages = (int)Math.Ceiling(records.Count * 0.1)
            };

            foreach (var record in records)
            {
                viewModel.News.Add(new News()
                {
                    Tittle = record.Title,
                    Description = record.Description,
                    Source = record.NameSource,
                    PublishDate = record.PublishDate
                });
            }
            return View("IndexPost", viewModel);
        }
    }
}
