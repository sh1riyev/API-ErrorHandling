using System;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
	public class CityConfiguration:BaseConfiguration<City>
    {

        public override void Configure(EntityTypeBuilder<City> builder)
        {
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            base.Configure(builder);
        }

    }
}

