using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public static class BllConfiguration
    {
        public static void ConfigurationService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CourseRepository>();
            serviceCollection.AddTransient<LearnerUserToCourseRepository>();
            serviceCollection.AddTransient<LikeDislikeRepository>();
            serviceCollection.AddTransient<ReviewRepository>();
            serviceCollection.AddTransient<SubscriptionRepository>();
            serviceCollection.AddTransient<AnswerRepository>();
            serviceCollection.AddTransient<ParagraphRepository>();
            serviceCollection.AddTransient<QuestionRepository>();
            serviceCollection.AddTransient<SubQuestionRepository>();
            serviceCollection.AddTransient<TestRepository>();
            serviceCollection.AddTransient<UnitRepository>();
            serviceCollection.AddTransient<CategoryRepository>();
            serviceCollection.AddTransient<PageRepository>();
            serviceCollection.AddTransient<RatingRepository>();
        }
    }
}
