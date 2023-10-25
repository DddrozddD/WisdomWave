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
        public int AgreeForSub { get; set; }
        public string FavouriteTheme { get; set; }
        public string Value { get; set; }
        public string userId { get; set; }
        public User User { get; set; }
    }
}
