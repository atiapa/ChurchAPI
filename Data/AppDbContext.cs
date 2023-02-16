using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChurchApi.Models;
using OmatrackApi.Models;

namespace ChurchApi.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<MemberLogin> MemberLogin { get; set; }
        public DbSet<HomeCellGroup> Homecell_Tbl { get; set; }
        public DbSet<AcademicLevel> academicLevel_Tbl { get; set; }
        public DbSet<BibleStudyGroup> biblestudygroup_tbl { get; set; }
        public DbSet<PositionInChurch> position_Tbl { get; set; }
        public DbSet<Groups> groups_Tbl { get; set; }
        public DbSet<MemberRegistration> Membership_Tbl { get; set; }
        public DbSet<Ministries> ministries_tbl { get; set; }
        public DbSet<Churches> churchDetail_tbl { get; set; }
        public DbSet<ChurchService> Services_Tbl { get; set; }
         public DbSet<Titles> Titles_Tbl { get; set; }
        public DbSet<AdminUser> User_Tbl { get; set; }
        //public DbSet<Document> Documents { get; set; }
        //public DbSet<Bank> Banks { get; set; }
        //public DbSet<Stat> Stats { get; set; }
        //public DbSet<Stat> Stats { get; set; }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries()
                        .Where(x => x.State == EntityState.Added)
                        .Select(x => x.Entity).OfType<IAuditable>())

            {
                entry.CreatedAt = DateTime.UtcNow;
                entry.ModifiedAt = DateTime.UtcNow;
            }

            foreach (var entry in ChangeTracker.Entries()
             .Where(x => x.State == EntityState.Modified)
             .Select(x => x.Entity)
             .OfType<IAuditable>())
            { entry.ModifiedAt = DateTime.UtcNow; }

            return base.SaveChanges();
        }
    }
}
