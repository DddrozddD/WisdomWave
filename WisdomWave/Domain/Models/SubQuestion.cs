using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public class SubQuestion
    {
        public int Id { get; set; }
        public string SubQuestionText { get; set; }
        public int? questionId { get; set; }
        public Question Question { get; set; }
        public IReadOnlyCollection<Answer> Answers { get; set; }
    }
}
