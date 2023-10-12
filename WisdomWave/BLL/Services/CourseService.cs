using DAL.Models;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CourseService
    {
        private readonly IUnitOfWork unitOfWork;

        public CourseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Course>> GetAsyncs() => await unitOfWork.CourseRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Course>> FindByConditionAsync(Expression<Func<Course, bool>> predicat) => await this.unitOfWork.CourseRepository.FindByConditionAsync(predicat);
        public async Task<Course> FindByConditionItemAsync(Expression<Func<Course, bool>> predicat) => await this.unitOfWork.CourseRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Course course) => await unitOfWork.CourseRepository.CreateAsync(course);
        public async Task DeleteAsync(int id) => await unitOfWork.CourseRepository.Delete(id);
        public async Task EditAsync(int id, Course course) => await unitOfWork.CourseRepository.Update(course, id);
        public async Task<IReadOnlyCollection<Course>> FindAllLearningCoursesForUser(string userId)
        {
            List<LearnerUserToCourse> allCourses = (await unitOfWork.LearnerUserToCourseRepository.FindByConditionAsync(x => x.userId == userId)).ToList();
            List<Course> courses = new List<Course>();

            for(int i = 0; i < allCourses.Count; i++)
            {
                LearnerUserToCourse thisC =  allCourses.First();
                courses.Add(await unitOfWork.CourseRepository.FindByConditionItemAsync(c => c.Id == thisC.courseId));
                allCourses.Remove(thisC);
            }

            return courses;
        }
    }
}
