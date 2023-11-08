using DAL.Models;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class TestService
    {
        private readonly IUnitOfWork unitOfWork;
        

        public TestService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Test>> GetAsyncs() => await unitOfWork.TestRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Test>> FindByConditionAsync(Expression<Func<Test, bool>> predicat) => await this.unitOfWork.TestRepository.FindByConditionAsync(predicat);
        public async Task<Test> FindByConditionItemAsync(Expression<Func<Test, bool>> predicat) => await this.unitOfWork.TestRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Test test, int unitId)
        {
            Unit unit = await unitOfWork.UnitRepository.FindByConditionItemAsync(c => c.Id == unitId);

            test.Unit = unit;
            test.unitId = unitId;

            OperationDetails result = await unitOfWork.TestRepository.CreateAsync(test);

            /*if (result.IsError == false)
            {
                Test newTest = await unitOfWork.TestRepository.FindByConditionItemAsync(t => (t.Id == test.Id) && (t.Name == test.Name) && (t.DateOfCreate == test.DateOfCreate) && (t.Description == test.Description));

                unit.Tests.ToList().Add(newTest);

                IReadOnlyCollection<Test> newTests = new ReadOnlyCollection<Test>(unit.Tests.ToList());
                unit.Tests = newTests;
                await unitOfWork.UnitRepository.Update(unit, unitId);
            }*/

            return result;
        }
       public async Task<OperationDetails> CheckUser(Test test, User user)
        {

            List<User> new_users_list = new List<User>(); 

            if(test.PassedTestUsers == null)
            {
                new_users_list.Add(user);
                test.PassedTestUsers = new_users_list;
                await unitOfWork.TestRepository.Update(test, test.Id);
                return new OperationDetails { IsError = false };
            }
            else
            {
                if(test.PassedTestUsers.ToList().Any(u=> u.Id == user.Id ) ) { 
                    return new OperationDetails { IsError = true };
                }
                else
                {
                    new_users_list = test.PassedTestUsers.ToList();
                    new_users_list.Add(user);
                    test.PassedTestUsers= new_users_list;
                    await unitOfWork.TestRepository.Update(test,test.Id);
                    return new OperationDetails { IsError = false };
                }
            }
            

            
        }
        public async Task DeleteAsync(int id) => await unitOfWork.TestRepository.Delete(id);
        public async Task EditAsync(int id, Test test) => await unitOfWork.TestRepository.Update(test, id);
    }
}
