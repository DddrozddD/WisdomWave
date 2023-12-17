using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Rating
    {
        public int courseId { get; set; }
        public Course Course { get; set; }
        public string userId { get; set; }
        public WwUser User { get; set; }
        public double Value { get; set; }
    }
}
