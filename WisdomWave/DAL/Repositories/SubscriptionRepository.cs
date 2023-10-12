using DAL.Context;
using DAL.Models;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription>
    {
        public SubscriptionRepository(WwContext context) : base(context) { }

        public async Task Delete(int id)
        {
            var subscription = await this.Entities.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);
            if (subscription != null)
            {
                this.Entities.Remove(subscription);
            }
            await _context.SaveChangesAsync();

        }

        public async Task<OperationDetails> Update(Subscription subscription, int Id)
        {
            var model = this.Entities.Where(m => m.Id == Id).First();
           model.FavouriteTheme = subscription.FavouriteTheme;
            model.userId = subscription.userId;
            model.AgreeForSub = subscription.AgreeForSub;
            model.Value = subscription.Value;
            model.User = subscription.User;

            this._context.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            await _context.SaveChangesAsync();

            return new OperationDetails() { IsError = false };
        }
    }
}