using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int Agree_for_sub { get; set; }
        public string Favourite_theme { get; set; }
        public string Value { get; set; }
        public string userId { get; set; }
        public User User { get; set; }
    }
}
