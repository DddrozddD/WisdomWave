using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
<<<<<<< Updated upstream
    internal class SubQuestion
=======
    public class SubQuestion
>>>>>>> Stashed changes
    {
        public int Id { get; set; }
        public string SubQuestionText { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}
