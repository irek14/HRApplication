using HRApplication.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITBoom_MSS.DataAccess.Configuration
{
    public class ApplicationStatesConfiguration : IEntityTypeConfiguration<ApplicationStates>
    {
        public void Configure(EntityTypeBuilder<ApplicationStates> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.StatusName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}