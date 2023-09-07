using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.UnitsOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitRepository UnitRepository { get; }
        public ParagraphRepository ParagraphRepository { get; }
        public TestRepository TestRepository { get; }
        public QuestionRepository QuestionRepository{ get; }
        public SubQuestionRepository SubQuestionRepository{ get; }
        public AnswerRepository AnswerRepository { get; }

        public UnitOfWork(UnitRepository unitRepository, ParagraphRepository paragraphRepository, TestRepository testRepository,
            QuestionRepository questionRepository, SubQuestionRepository subQuestionRepository, AnswerRepository answerRepository)
        {
            UnitRepository = unitRepository;
            ParagraphRepository = paragraphRepository;
            TestRepository = testRepository;
            QuestionRepository = questionRepository;
            SubQuestionRepository = subQuestionRepository;
            AnswerRepository = answerRepository;
        }


    }
}
