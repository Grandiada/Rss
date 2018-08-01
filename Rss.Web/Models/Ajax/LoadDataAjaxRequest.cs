using Newtonsoft.Json;

namespace Rss.Web.Models.Ajax
{
    public class LoadDataAjaxRequest
    {
        [JsonProperty("sourceName")]
        public string SourceName { get; set; }
        [JsonProperty("page")]
        public int Page { get; set; }
        [JsonProperty("sortOrder")]
        public SortState SortOrder { get; set; }
    }
}
