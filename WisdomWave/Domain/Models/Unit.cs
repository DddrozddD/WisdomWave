using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Unit
    {
        public int Id { get; set; }
        public int number  { get; set; }
        public string UnitName { get; set; }
        public int DateOfCreate { get; set; }
        public IReadOnlyCollection<Paragraph> Paragraphs { get; set; }
        public IReadOnlyCollection<Test> Tests { get; set; }
        public int? courseId { get; set; }
        public Course Course { get; set; }
    }
}
