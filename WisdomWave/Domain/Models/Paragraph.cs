using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Paragraph
    {
        public int Id { get; set; }
        public string ParagraphName { get; set; }
        public string ParagraphText { get; set; }
        public string PhotoLinks { get; set; }
        public string video_links { get; set; }
        public int UnitsID { get; set; }
        public Unit Unit {get; set; }
    }
}
