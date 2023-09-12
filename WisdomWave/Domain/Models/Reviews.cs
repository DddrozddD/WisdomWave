using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        public string text_review { get; set; }
        public string userId { get; set; }
        public User User { get; set; }
    }
}
