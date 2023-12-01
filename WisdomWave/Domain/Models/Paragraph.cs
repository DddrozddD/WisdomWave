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
        public string? ParagraphName { get; set; }
        public string? ParagraphText { get; set; }
        public int pageId { get; set; }
        public Page Page { get; set; }
        
    }
}
