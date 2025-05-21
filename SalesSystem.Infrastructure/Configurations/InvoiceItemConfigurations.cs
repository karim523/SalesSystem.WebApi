global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using SalesSystem.Domain.Entities;

namespace SalesSystem.Infrastructure.Configurations
{
    internal class InvoiceItemConfigurations : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.ToTable("InvoiceItems");

            builder.HasKey(ii => ii.Id);

            builder.Property(ii => ii.Id)
                .ValueGeneratedOnAdd();

            builder.Property(ii => ii.Quantity)
                .IsRequired();

            builder.Property(ii => ii.UnitPrice)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Ignore(ii => ii.TotalPrice); 

            builder.HasOne(ii => ii.Product)
                .WithMany()
                .HasForeignKey(ii => ii.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ii => ii.Invoice)
                .WithMany(i => i.Items)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
