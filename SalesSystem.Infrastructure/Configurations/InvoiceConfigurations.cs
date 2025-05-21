namespace SalesSystem.Infrastructure.Configurations
{
    internal class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(i => i.Date)
                .IsRequired();

            builder.Property(i => i.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.CustomerName)
                .HasMaxLength(100);

            builder.Property(i => i.SubTotal)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.DiscountPercentage)
                .HasColumnType("decimal(5,2)")
                .IsRequired();

            builder.Property(i => i.Type)
            .HasConversion<int>()
            .IsRequired();

            builder.Ignore(i => i.TotalAmount);
            builder.Ignore(i => i.Discount);
            builder.HasMany(i => i.Items)
                .WithOne(ii => ii.Invoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
