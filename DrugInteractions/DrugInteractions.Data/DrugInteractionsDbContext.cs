using DrugInteractions.Data.Models.Brands;
using DrugInteractions.Data.Models.Drugs;
using DrugInteractions.Data.Models.IntermediateTables;
using DrugInteractions.Data.Models.SideEffects;
using DrugInteractions.Data.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DrugInteractions.Data
{
    public class DrugInteractionsDbContext : IdentityDbContext<User>
    {
        public DrugInteractionsDbContext(DbContextOptions<DrugInteractionsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Drug> Drugs { get; set; }

        public DbSet<DrugGroup> DrugGroups { get; set; }

        public DbSet<SideEffect> SideEffects { get; set; }

        public DbSet<SideEffectGroup> SideEffectGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .Entity<SideEffect>()
                .HasOne(eff => eff.SideEffectGroup)
                .WithMany(g => g.SideEffects)
                .HasForeignKey(eff => eff.SideEffectGroupId);

            builder
                .Entity<SideEffect>()
                .HasOne(eff => eff.Admin)
                .WithMany(u => u.SideEffects)
                .HasForeignKey(eff => eff.AdminId);

            builder
                .Entity<Drug>()
                .HasOne(d => d.Representative)
                .WithMany(u => u.Drugs)
                .HasForeignKey(d => d.RepresentativeId);

            builder
                .Entity<Drug>()
                .HasOne(d => d.DrugGroup)
                .WithMany(dg => dg.Drugs)
                .HasForeignKey(d => d.DrugGroupId);

            builder
                .Entity<Drug>()
                .HasOne(d => d.Brand)
                .WithMany(b => b.Drugs)
                .HasForeignKey(d => d.BrandId);

            builder
                .Entity<Brand>()
                .HasOne(b => b.Admin)
                .WithMany(u => u.Brands)
                .HasForeignKey(b => b.AdminId);

            builder
                .Entity<DrugGroup>()
                .HasOne(dg => dg.Admin)
                .WithMany(u => u.DrugGroups)
                .HasForeignKey(dg => dg.AdminId);

            builder
                .Entity<SideEffectGroup>()
                .HasOne(dg => dg.Admin)
                .WithMany(u => u.SideEffectGroups)
                .HasForeignKey(dg => dg.AdminId);

            builder
                .Entity<DrugSideEffect>()
                .HasKey(d => new { d.DrugId, d.SideEffectId });

            builder
                .Entity<DrugSideEffect>()
                .HasOne(dseff => dseff.Drug)
                .WithMany(d => d.SideEffects)
                .HasForeignKey(dseff => dseff.DrugId);

            builder
                .Entity<DrugSideEffect>()
                .HasOne(dseff => dseff.SideEffect)
                .WithMany(seff => seff.Drugs)
                .HasForeignKey(dseff => dseff.SideEffectId);

            base.OnModelCreating(builder);
        }
    }
}
