using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UnitService
    {
        private readonly IUnitOfWork unitOfWork;

        public UnitService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Unit>> GetAsyncs() => await unitOfWork.UnitRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Unit>> FindByConditionAsync(Expression<Func<Unit, bool>> predicat) => await this.unitOfWork.UnitRepository.FindByConditionAsync(predicat);
        public async Task<Unit> FindByConditionItemAsync(Expression<Func<Unit, bool>> predicat) => await this.unitOfWork.UnitRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Unit unit, int courseId) {
            Course course = await unitOfWork.CourseRepository.FindByConditionItemAsync(c => c.Id == courseId);

            unit.Course = course;
            unit.courseId = courseId;

            OperationDetails result = await unitOfWork.UnitRepository.CreateAsync(unit);

            if (result.IsError == false)
            {
                Unit newUnit = await unitOfWork.UnitRepository.FindByConditionItemAsync(u=>(u.UnitName == unit.UnitName)&&(u.DateOfCreate == unit.DateOfCreate)&&(u.number == unit.number)&&(u.courseId == unit.courseId));

                course.Units.ToList().Add(newUnit);

                IReadOnlyCollection<Unit> newUnits = new ReadOnlyCollection<Unit>(course.Units.ToList());
                course.Units = newUnits;
                await unitOfWork.CourseRepository.Update(course, courseId);
            }

            return result;
        }
        public async Task DeleteAsync(int id) => await unitOfWork.UnitRepository.Delete(id);
        public async Task EditAsync(int id, Unit unit) => await unitOfWork.UnitRepository.Update(unit, id);
    }
}
