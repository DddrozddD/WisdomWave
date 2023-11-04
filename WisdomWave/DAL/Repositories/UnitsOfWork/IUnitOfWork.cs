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
        CourseRepository CourseRepository { get; }
        LikeDislikeRepository LikeDislikeRepository { get; }
        ReviewRepository ReviewRepository { get; }
        SubscriptionRepository SubscriptionRepository { get; }
        LearnerUserToCourseRepository LearnerUserToCourseRepository { get; }
        AnswerRepository AnswerRepository { get; }
        ParagraphRepository ParagraphRepository { get; }
        QuestionRepository QuestionRepository { get; }
        SubQuestionRepository SubQuestionRepository { get; }
        TestRepository TestRepository { get; }
        UnitRepository UnitRepository { get; }
        CategoryRepository CategoryRepository { get; }
    }
}
