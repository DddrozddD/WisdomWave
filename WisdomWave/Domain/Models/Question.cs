using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public class Question
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int? testId { get; set; }
        public Test Test { get; set; }
        public IReadOnlyCollection<Answer> Answers { get; set; }
        public IReadOnlyCollection<SubQuestion> SubQuestions { get; set; }
    }
}
