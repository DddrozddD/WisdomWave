using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Page
    {
        public int Id { get; set; }
        public string PageName { get; set; }
        public string? PhotoLinks { get; set; }
        public string? VideoLinks { get; set; }
        public string DateOfCreate { get; set; }
        public int? unitId { get; set; }
        public Unit Unit { get; set; }
        public IReadOnlyCollection<WwUser>? PassedPageUsers { get; set; }
        public IReadOnlyCollection<Paragraph>? Paragraphs { get; set; }
    }
}
