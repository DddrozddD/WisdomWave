using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class LikeDislike
    {
        public string userId { get; set; }
        public User User { get; set; }
        public int reviewId { get; set; }
        public Review Review { get; set; }
        public int IsLike { get; set; }
    }
}
