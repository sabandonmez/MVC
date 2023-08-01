using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repositories.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            
            builder.HasKey(p=>p.ProductId);
            builder.Property(p=>p.ProductName).IsRequired();
            builder.Property(p=>p.Price).IsRequired();
            builder.HasData(

         new Product() { ProductId = 1, CategoryId = 4,ImageUrl="/images/1.jpg", ProductName = "Güneş Gözlüğü", Price = 1700 ,ShowCase=false},
         new Product() { ProductId = 2, CategoryId = 2,ImageUrl="/images/2.jpg", ProductName = "Oyun Kolu", Price = 1000 ,ShowCase=false},
         new Product() { ProductId = 3, CategoryId = 3,ImageUrl="/images/3.jpg", ProductName = "Ruj", Price = 500 ,ShowCase=false},
         new Product() { ProductId = 4, CategoryId = 2,ImageUrl="/images/4.jpg", ProductName = "Bmw Oyuncak Araba", Price = 7000 ,ShowCase=false},
         new Product() { ProductId = 5, CategoryId = 2,ImageUrl="/images/5.jpg", ProductName = "Kulaklık", Price = 1000 ,ShowCase=false},
         new Product() { ProductId = 6, CategoryId = 2,ImageUrl="/images/6.jpg", ProductName = "Kamera", Price = 9800 ,ShowCase=false},
         new Product() { ProductId = 7, CategoryId = 2,ImageUrl="/images/7.jpg", ProductName = "Apple Laptop", Price = 35000 ,ShowCase=false},
         new Product() { ProductId = 8, CategoryId = 2,ImageUrl="/images/8.jpg", ProductName = "Asus Laptop", Price = 14000 ,ShowCase=false},
         new Product() { ProductId = 9, CategoryId = 2,ImageUrl="/images/9.jpg", ProductName = "Samsung Monitör", Price = 4800 ,ShowCase=false},
         new Product() { ProductId = 10, CategoryId = 2,ImageUrl="/images/10.jpg", ProductName = "MSI Monitör", Price = 6999 ,ShowCase=false},
         new Product() { ProductId = 11, CategoryId = 4,ImageUrl="/images/11.jpg", ProductName = "Pandora Bileklik", Price = 1345 ,ShowCase=true},
         new Product() { ProductId = 12, CategoryId = 1,ImageUrl="/images/12.jpg", ProductName = "Atomik Alışkanlıklar", Price = 125 ,ShowCase=true},
         new Product() { ProductId = 13, CategoryId = 1,ImageUrl="/images/13.jpg", ProductName = "Altın Yay", Price = 145 ,ShowCase=true}

            );
        }
    }
}