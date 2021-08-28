using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    // Context : Northwind database'deki tablolar ile proje classlarını bağlamak için kullanılır
    public class NorthwindContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //override on diyip enter bastık
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Northwind;Trusted_Connection=true");
            //Sqlserver kullan dedik. @ yapma nedeni \ düzgün algılasın diye
            //(localdb)\MSSQLLocalDB bizim sql server adresimiz
            //Database=Northwind databasedeki nortwind klasörüne bak
            //Trusted_Connection=true girerken kullancı adı şifre isteme
        }

        public DbSet<Product> Products { get; set; } //prop tab tab yazarak getirdik
        public DbSet<Category> Categories { get; set; }//Databasedeki Category benim Categories'İme eşit dedik
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder) //override on enter diyerek açılıyor
        //                                                                   //Dbset arka planda böyle bir metod çalıştırırarak eşleştirme yapar
        //                                                                   //Eğer bizim class ismi ile database deki tablo ismi farklı ise bu metodu kullanarak kendimiz eşleştirme yaparız
        //{
        //    modelBuilder.Entity<Car>().ToTable("Cars"); //Bizim Car sınıfımızı Databasedeki Cars tablosu ile eşleştir dedik
        //    modelBuilder.Entity<Car>().Property(p=>p.Id).HasColumnName("CarsId"); //Bizim Id ile Databasedeki cars'ın id sini eşleştirdik. Bu şekilde bizde olan tüm kolonları eşleştiriyoruz
        //}
    }
}
