using DAL.Models;
using DAL.Repositories.UnitsOfWork;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PageService
    {
        private readonly IUnitOfWork unitOfWork;

        public PageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IReadOnlyCollection<Page>> GetAsyncs() => await unitOfWork.PageRepository.GetAllAsync();
        public async Task<IReadOnlyCollection<Page>> FindByConditionAsync(Expression<Func<Page, bool>> predicat) => await this.unitOfWork.PageRepository.FindByConditionAsync(predicat);
        public async Task<Page> FindByConditionItemAsync(Expression<Func<Page, bool>> predicat) => await this.unitOfWork.PageRepository.FindByConditionItemAsync(predicat);
        public async Task<OperationDetails> CreateAsync(Page page, int unitId)
        {
            Unit unit = await unitOfWork.UnitRepository.FindByConditionItemAsync(c => c.Id == unitId);

            page.Unit = unit;
            page.unitId = unitId;

            OperationDetails result = await unitOfWork.PageRepository.CreateAsync(page);

            
            return result;
        }
        public async Task<OperationDetails> CheckUser(Page page, WwUser user)
        {

            List<WwUser> new_users_list = new List<WwUser>();

            if (page.PassedPageUsers == null)
            {
                new_users_list.Add(user);
                page.PassedPageUsers = new_users_list;
                await unitOfWork.PageRepository.Update(page, page.Id);
                return new OperationDetails { IsError = false };
            }
            else
            {
                if (page.PassedPageUsers.ToList().Any(u => u.Id == user.Id))
                {
                    return new OperationDetails { IsError = true };
                }
                else
                {
                    new_users_list = page.PassedPageUsers.ToList();
                    new_users_list.Add(user);
                    page.PassedPageUsers = new_users_list;
                    await unitOfWork.PageRepository.Update(page, page.Id);
                    return new OperationDetails { IsError = false };
                }
            }
        }
        public async Task DeleteAsync(int id) => await unitOfWork.PageRepository.Delete(id);
        public async Task EditAsync(int id, Page Page) => await unitOfWork.PageRepository.Update(Page, id);
    }
}
