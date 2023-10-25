using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class SendGridSenderOptions
    {
        public string UserMail { get; set; }
        public string SendGridKey { get; set; }
    }
}
