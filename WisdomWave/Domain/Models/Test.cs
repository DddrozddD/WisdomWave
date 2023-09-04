using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Test
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DateOfCreate { get; set; } // DateOfCreate = DateOfUpdate
        public int UnitId { get; set; }
        public Unit Unit { get; set; }
        //public Questions Question { get; set; }
    }
}
