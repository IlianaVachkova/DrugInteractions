using DrugInteractions.Data;
using DrugInteractions.Data.Models.Brands;
using System.Threading.Tasks;

namespace DrugInteractions.Services.Admin.Implementations
{
    public class AdminBrandsService : IAdminBrandsService
    {
        private readonly DrugInteractionsDbContext db;

        public AdminBrandsService(DrugInteractionsDbContext db)
        {
            this.db = db;
        }

        public async Task CreateAsync(Brand model)
        {
            this.db.Add(model);

            await this.db.SaveChangesAsync();
        }
    }
}
