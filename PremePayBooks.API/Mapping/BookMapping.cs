using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PremePayBooks.API.Models;

namespace PremePayBooks.API.Mapping
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder.HasOne(o => o.Order)
                .WithMany(b => b.Books);

            builder.ToTable("Books");
        }
    }
}