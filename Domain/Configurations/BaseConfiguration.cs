﻿using System;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public BaseConfiguration()
        {

        }
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.IsDeleted).HasDefaultValue(false);
        }
    }
}

