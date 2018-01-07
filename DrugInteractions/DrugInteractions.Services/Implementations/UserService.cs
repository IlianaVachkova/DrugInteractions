using AutoMapper.QueryableExtensions;
using DrugInteractions.Data;
using DrugInteractions.Services.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly DrugInteractionsDbContext db;

        public UserService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task<UserProfileServiceModel> ProfileAsync(string id)
        { 
            return await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserProfileServiceModel>(new { userId = id })
                .FirstOrDefaultAsync();
        }
    }
}
