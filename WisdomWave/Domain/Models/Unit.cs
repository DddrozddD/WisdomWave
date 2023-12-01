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
        public int Number  { get; set; }
        public string UnitName { get; set; }
        public string DateOfCreate { get; set; }
        public IReadOnlyCollection<Page>? Pages { get; set; }
        public IReadOnlyCollection<Test>? Tests { get; set; }
        public int? courseId { get; set; }
        public Course Course { get; set; }
        public IReadOnlyCollection<WwUser>? PassedUnitUsers { get; set; }
    }
}
