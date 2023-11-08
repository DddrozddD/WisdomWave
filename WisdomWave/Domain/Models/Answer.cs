using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public class Answer
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        public string AnswerText { get; set; }
        public int? subQuestionId { get; set; }
        public SubQuestion SubQuestion { get; set; }
        public int? questionId { get; set; }
        public Question Question { get; set; }
    }
}
