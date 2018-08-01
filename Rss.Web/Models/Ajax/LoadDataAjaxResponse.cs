using System.Collections.Generic;
using Newtonsoft.Json;

namespace Rss.Web.Models.Ajax
{
    public sealed class LoadDataAjaxResponse
    {
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }

        [JsonProperty("news")]
        public List<NewsAjax> News { get; set; }

        public LoadDataAjaxResponse()
        {
            News=new List<NewsAjax>();
        }
    }

    public sealed class NewsAjax
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("source")]
        public string Source  { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
    }
}
