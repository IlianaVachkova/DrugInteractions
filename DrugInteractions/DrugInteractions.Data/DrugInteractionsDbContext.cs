using DrugInteractions.Data.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrugInteractions.Data
{
    public class DrugInteractionsDbContext : IdentityDbContext<User>
    {
        public DrugInteractionsDbContext(DbContextOptions<DrugInteractionsDbContext> options)
            : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
