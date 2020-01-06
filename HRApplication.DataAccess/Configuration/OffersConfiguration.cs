using HRApplication.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITBoom_MSS.DataAccess.Configuration
{
    public class OffersConfiguration : IEntityTypeConfiguration<Offers>
    {
        public void Configure(EntityTypeBuilder<Offers> builder)
        {
            builder.HasKey(e => e.Id);
            //builder.Property(e => e.Id)
            //    .HasColumnName("ISU_Id")
            //    .HasDefaultValueSql("NEXT VALUE FOR shared.OfferNumber");

            builder.HasIndex(p => p.EndDate);

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CreatedOn).HasColumnType("date");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(e => e.EndDate).HasColumnType("date");

            builder.Property(e => e.HoursPerWeek).HasColumnType("decimal(18, 0)");

            builder.Property(e => e.Position).HasMaxLength(255);

            builder.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasOne(d => d.ContractType)
                .WithMany(p => p.Offers)
                .HasForeignKey(d => d.ContractTypeId)
                .HasConstraintName("FK_Offers_ContractTypes");

            builder.HasOne(d => d.CreatedBy)
                .WithMany(p => p.Offers)
                .HasForeignKey(d => d.CreatedById)
                .HasConstraintName("FK_Offers_Users");
        }
    }
}