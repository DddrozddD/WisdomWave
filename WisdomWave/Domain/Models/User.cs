using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public int Age { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string Rating_u { get; set; }
        public string Photo_u { get; set; }
        public string subscriptionId { get; set; }
        public Subscription Subscription { get; set; }
    }
}
