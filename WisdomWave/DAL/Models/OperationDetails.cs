using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class OperationDetails
    {

        public bool Succeeded { get; }
        public string[] Errors { get; }

        public OperationDetails(bool succeeded, string[] errors)
        {
            Succeeded = succeeded;
            Errors = errors;
        }

        public string Message { get; set; }
        public Exception exception { get; set; }
        public bool IsError { get; set; }
    }
}
