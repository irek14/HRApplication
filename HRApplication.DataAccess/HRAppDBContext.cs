using System;
using ITBoom_MSS.DataAccess.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HRApplication.DataAccess.Entities
{
    public partial class HRAppDBContext : DbContext
    {
        public HRAppDBContext()
        {
        }

        public HRAppDBContext(DbContextOptions<HRAppDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationStates> ApplicationStates { get; set; }
        public virtual DbSet<ApplicationStatusHistory> ApplicationStatusHistory { get; set; }
        public virtual DbSet<Applications> Applicationss { get; set; }
        public virtual DbSet<ContractTypes> ContractTypes { get; set; }
        public virtual DbSet<Offers> Offers { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ApplicationsConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationStatesConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationStatusHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new ContractTypesConfiguration());
            modelBuilder.ApplyConfiguration(new OffersConfiguration());
            modelBuilder.ApplyConfiguration(new UserRolesConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
        }
    }
}
