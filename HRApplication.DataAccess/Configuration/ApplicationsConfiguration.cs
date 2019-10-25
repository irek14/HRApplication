using HRApplication.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITBoom_MSS.DataAccess.Configuration
{
    public class ApplicationsConfiguration : IEntityTypeConfiguration<Applications>
    {
        public void Configure(EntityTypeBuilder<Applications> builder)
        {
            builder.ToTable("Applications");

            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.CreateOn).HasColumnType("date");

            builder.Property(e => e.CurrentApplicationStateName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(e => e.CvfileName)
                .HasColumnName("CVFileName")
                .HasMaxLength(255);

            builder.HasOne(d => d.CreatedBy)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.CreatedById)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_Users");

            builder.HasOne(d => d.Offer)
                .WithMany(p => p.Applications)
                .HasForeignKey(d => d.OfferId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Applications_Offers");
        }
    }
}