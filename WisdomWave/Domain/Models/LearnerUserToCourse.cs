using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LearnerUserToCourse
    {
        public string userId { get; set; }
        public User User { get; set; }
        public int courseId { get; set; }
        public Course Course { get; set; }
    }
}
