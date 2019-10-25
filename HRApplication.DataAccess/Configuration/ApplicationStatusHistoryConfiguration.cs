using HRApplication.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITBoom_MSS.DataAccess.Configuration
{
    public class ApplicationStatusHistoryConfiguration : IEntityTypeConfiguration<ApplicationStatusHistory>
    {
        public void Configure(EntityTypeBuilder<ApplicationStatusHistory> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.Date).HasColumnType("date");

            builder.HasOne(d => d.Application)
                .WithMany(p => p.ApplicationStatusHistory)
                .HasForeignKey(d => d.ApplicationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationStatusHistory_Applicationss");

            builder.HasOne(d => d.ApplicationState)
                .WithMany(p => p.ApplicationStatusHistory)
                .HasForeignKey(d => d.ApplicationStateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ApplicationStatusHistory_ApplicationStates");
        }
    }
}