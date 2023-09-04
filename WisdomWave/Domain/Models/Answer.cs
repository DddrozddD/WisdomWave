using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    internal class Answer
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
