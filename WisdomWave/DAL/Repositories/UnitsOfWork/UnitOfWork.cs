using Domain.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public UnitOfWork(CourseRepository courseRepository, LikeDislikeRepository likeDislikeRepository, ReviewRepository reviewRepository,
            SubscriptionRepository subscriptionRepository, LearnerUserToCourseRepository learnerUserToCourseRepository, AnswerRepository answerRepository, ParagraphRepository paragraphRepository,
            QuestionRepository questionRepository, SubQuestionRepository subQuestionRepository, TestRepository testRepository, UnitRepository unitRepository, CategoryRepository categoryRepository,
            PageRepository pageRepository, RatingRepository ratingRepository)
        {
            CourseRepository = courseRepository;
            LikeDislikeRepository = likeDislikeRepository;
            ReviewRepository = reviewRepository;
            SubscriptionRepository = subscriptionRepository;
            LearnerUserToCourseRepository = learnerUserToCourseRepository;
            AnswerRepository = answerRepository;
            ParagraphRepository = paragraphRepository;
            QuestionRepository = questionRepository;
            SubQuestionRepository = subQuestionRepository;
            TestRepository = testRepository;
            UnitRepository = unitRepository;
            CategoryRepository = categoryRepository;
            PageRepository = pageRepository;
            RatingRepository = ratingRepository;
        }

        public CourseRepository CourseRepository { get; }

        public LikeDislikeRepository LikeDislikeRepository { get; }

        public ReviewRepository ReviewRepository { get; }

        public SubscriptionRepository SubscriptionRepository { get; }

        public LearnerUserToCourseRepository LearnerUserToCourseRepository { get; }

        public AnswerRepository AnswerRepository { get; }

        public ParagraphRepository ParagraphRepository { get; }

        public QuestionRepository QuestionRepository { get; }

        public SubQuestionRepository SubQuestionRepository { get; }

        public TestRepository TestRepository { get; }

        public UnitRepository UnitRepository { get; }
        public CategoryRepository CategoryRepository { get; }
        public PageRepository PageRepository { get; }
        public RatingRepository RatingRepository { get; }

    }
}
