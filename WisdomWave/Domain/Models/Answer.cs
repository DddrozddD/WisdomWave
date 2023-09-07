using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
<<<<<<< Updated upstream
    internal class Answer
=======
    public class Answer
>>>>>>> Stashed changes
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; }
        public int SubQuestionId { get; set; }
        public int QuestionId { get; set; }
        public SubQuestion SubQuestion { get; set; }
        public Question Question { get; set; }
    }
}
