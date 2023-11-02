using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Paragraph
    {
        public int Id { get; set; }
        public string ParagraphName { get; set; }
        public string ParagraphText { get; set; }
        public string PhotoLinks { get; set; }
        public string VideoLinks { get; set; }
        public int? unitID { get; set; }
        public Unit Unit {get; set; }
        public IReadOnlyCollection<User> PassedParagraphUsers { get; set; }
    }
}
