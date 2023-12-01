using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string TextReview { get; set; }
        public string userId { get; set; }
        public WwUser User { get; set; }
        public int? courseId { get; set; }
        public Course Course { get; set; }
        public IReadOnlyCollection<LikeDislike> LikesDislikes { get; set;}
    }
}
