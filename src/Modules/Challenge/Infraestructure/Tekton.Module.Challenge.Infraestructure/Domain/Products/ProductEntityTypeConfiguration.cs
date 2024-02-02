using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Domain.Products;

namespace Tekton.Module.Challenge.Infraestructure.Domain.Products
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products", "dbo");

            builder.HasKey(x => x.ProductId);
            builder.Property<string>("_name").HasColumnName("Name");
            builder.Property<int>("_stock").HasColumnName("Stock");
            builder.Property<string>("_description").HasColumnName("Description");
            builder.Property<int>("_status").HasColumnName("Status");
            builder.Property<double>("_price").HasColumnName("Price");
            


        }
    }
}
