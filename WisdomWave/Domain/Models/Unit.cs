using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
<<<<<<< Updated upstream
    internal class Unit
=======
    public class Unit
>>>>>>> Stashed changes
    {
        public int Id { get; set; }
        public int number  { get; set; }
        public string UnitName { get; set; }
        public int DateOfCreate { get; set; }
        // public Paragraphs Paragraph { get; set; }
        // public Tests Test { get; set; }
        public string CourseId { get; set; }
        public Course Course { get; set; }
    }
}
