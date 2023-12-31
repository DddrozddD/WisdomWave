﻿using DAL.Models;
using DAL.Repositories;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
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


        public async Task<OperationDetails> CreateAsync(Unit unit, int courseId)
        {
            Course course = await unitOfWork.CourseRepository.FindByConditionItemAsync(c => c.Id == courseId);

            unit.Course = course;
            unit.courseId = courseId;

            OperationDetails result = await unitOfWork.UnitRepository.CreateAsync(unit);

            return result;
        }

        public async Task<OperationDetails> CheckUser(Unit unit, WwUser user)
        {

            List<WwUser> new_users_list = new List<WwUser>();

            if (unit.PassedUnitUsers == null)
            {
                new_users_list.Add(user);
                unit.PassedUnitUsers = new_users_list;
                await unitOfWork.UnitRepository.Update(unit, unit.Id);
                return new OperationDetails { IsError = false };
            }
            else
            {
                if (unit.PassedUnitUsers.ToList().Any(u => u.Id == user.Id))
                {
                    return new OperationDetails { IsError = true };
                }
                else
                {
                    new_users_list = unit.PassedUnitUsers.ToList();
                    new_users_list.Add(user);
                    unit.PassedUnitUsers = new_users_list;
                    await unitOfWork.UnitRepository.Update(unit, unit.Id);
                    return new OperationDetails { IsError = false };
                }
            }

        }

        public async Task DeleteAsync(int id) => await unitOfWork.UnitRepository.Delete(id);
        public async Task<OperationDetails> EditAsync(int id, Unit unit) => await unitOfWork.UnitRepository.Update(unit, id);
    }
}
