using HRApplication.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ITBoom_MSS.DataAccess.Configuration
{
    public class ContractTypesConfiguration : IEntityTypeConfiguration<ContractTypes>
    {
        public void Configure(EntityTypeBuilder<ContractTypes> builder)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();

            builder.Property(e => e.ContractTypeName)
                .IsRequired()
                .HasMaxLength(255);
        }
    }
}