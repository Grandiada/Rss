using System.Collections.Generic;
using System.Linq;

namespace Rss.Web.Models
{
    public sealed class SourceViewModel
    {
        public List<Source> Sources { get; set; }

        public Source GetSelected => Sources.Single(i => i.IsSelected);

        public SourceViewModel()
        {
            Sources = new List<Source>
            {
                new Source { Id = 0, Name = "Все", IsSelected = true}
            };
        }

        public void Select(string name)
        {
            foreach (var source in Sources)
            {
                if (source.Name.Equals(name))
                    source.IsSelected = true;
                else
                    source.IsSelected = false;
            }
        }
    }
    public sealed class Source
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}
