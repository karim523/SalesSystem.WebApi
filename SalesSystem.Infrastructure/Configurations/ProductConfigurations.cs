namespace SalesSystem.Infrastructure.Configurations
{
    class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.QuantityAvailable)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(p => p.PurchasePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.SalePrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.ReorderThreshold)
                .IsRequired()
                .HasDefaultValue(0);

        }
    }
}
