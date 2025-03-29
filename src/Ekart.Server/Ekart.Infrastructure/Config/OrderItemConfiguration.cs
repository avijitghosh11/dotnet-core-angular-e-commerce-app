﻿using Ekart.Core.Entites.OrderAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Ekart.Infrastructure.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(x => x.ItemOrdered, o => o.WithOwner());
            builder.Property(x => x.Price).HasColumnType("decimal(18,2)");
        }
    }
}
