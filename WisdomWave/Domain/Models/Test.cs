using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; } // DateOfCreate = DateOfUpdate
        public int? unitId { get; set; }
        public Unit Unit { get; set; }
        public IReadOnlyCollection<Question> Questions { get; set; }
    }
}
