using DrugInteractions.Data;
using DrugInteractions.Data.Models.SideEffects;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin.Implementations
{
    public class AdminSideEffectGroupsService : IAdminSideEffectGroupsService
    {
        private readonly DrugInteractionsDbContext db;

        public AdminSideEffectGroupsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(SideEffectGroup model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }
    }
}
