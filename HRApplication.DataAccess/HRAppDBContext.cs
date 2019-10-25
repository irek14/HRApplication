using System;
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
        public virtual DbSet<Applicationss> Applicationss { get; set; }
        public virtual DbSet<ContractTypes> ContractTypes { get; set; }
        public virtual DbSet<Offers> Offers { get; set; }
        public virtual DbSet<UserRoles> UserRoles { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=HRAppDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<ApplicationStates>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ApplicationStatusHistory>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Application)
                    .WithMany(p => p.ApplicationStatusHistory)
                    .HasForeignKey(d => d.ApplicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationStatusHistory_Applicationss");

                entity.HasOne(d => d.ApplicationState)
                    .WithMany(p => p.ApplicationStatusHistory)
                    .HasForeignKey(d => d.ApplicationStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationStatusHistory_ApplicationStates");
            });

            modelBuilder.Entity<Applicationss>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreateOn).HasColumnType("date");

                entity.Property(e => e.CurrentApplicationStateName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.CvfileName)
                    .HasColumnName("CVFileName")
                    .HasMaxLength(255);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.Applicationss)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicationss_Users");

                entity.HasOne(d => d.Offer)
                    .WithMany(p => p.Applicationss)
                    .HasForeignKey(d => d.OfferId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Applicationss_Offers");
            });

            modelBuilder.Entity<ContractTypes>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ContractTypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Offers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.HoursPerWeek).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Position).HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ContractType)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.ContractTypeId)
                    .HasConstraintName("FK_Offers_ContractTypes");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.Offers)
                    .HasForeignKey(d => d.CreatedById)
                    .HasConstraintName("FK_Offers_Users");
            });

            modelBuilder.Entity<UserRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_UserRoles");
            });
        }
    }
}
