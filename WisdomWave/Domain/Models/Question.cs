﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
<<<<<<< Updated upstream
    internal class Question
=======
    public class Question
>>>>>>> Stashed changes
    {
        public int Id { get; set; }
        public string QuestionName { get; set; }
        public string QuestionText { get; set; }
        public string QuestionType { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}
