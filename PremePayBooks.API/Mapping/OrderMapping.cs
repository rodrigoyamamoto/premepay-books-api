using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PremePayBooks.API.Models;

namespace PremePayBooks.API.Mapping
{
    public class OrderMapping : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            // 1 : N => Order : Book

            builder.HasMany(o => o.Books)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId);

            builder.ToTable("Orders");
        }
    }
}