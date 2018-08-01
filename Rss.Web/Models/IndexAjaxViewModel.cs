namespace Rss.Web.Models
{
    public class IndexAjaxViewModel
    {
        public int TotalPages { get; set; }
        public SourceViewModel SourceViewModel { get; set; }

        public IndexAjaxViewModel()
        {
            SourceViewModel = new SourceViewModel();
        }
    }
}
