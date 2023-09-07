using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.UnitsOfWork
{
    public interface IUnitOfWork
    {
        AnswerRepository AnswerRepository { get; }
        QuestionRepository QuestionRepository { get; }
        ParagraphRepository ParagraphRepository { get; }
        SubQuestionRepository SubQuestionRepository { get; }
        UnitRepository UnitRepository { get; }
        TestRepository TestRepository { get; }
    }
}
