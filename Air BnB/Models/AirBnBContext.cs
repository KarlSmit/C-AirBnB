using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Air_BnB.Models
{
    public class AirBnBContext : DbContext
    {

        public DbSet<Landlord> Landlords { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localDb)\\MSSQLLocalDB;Initial Catalog=AirBnB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework");
        }

        protected override void OnModelCreating(ModelBuilder seedBuilder)
        {
            seedBuilder.Entity<Landlord>()
              .Property(p => p.FirstName)
              .HasMaxLength(100);
            seedBuilder.Entity<Landlord>().Ignore(t => t.FullName);

            seedBuilder.Entity<Landlord>()
              .HasData(new Landlord
              {
                  Id = 1,
                  FirstName = "Pieter",
                  LastName = "Pinda",
              },
              new Landlord
              {
                  Id = 2,
                  FirstName = "Saradin",
                  LastName = "Sombrero",
              },
              new Landlord
              {
                  Id = 3,
                  FirstName = "Stanley",
                  LastName = "Mes",
              }
              );

            seedBuilder.Entity<Location>()
               .HasData(new
               {
                   Id = 1,
                   Name = "Almere"
               },
               new
               {
                   Id = 2,
                   Name = "Weesp"
               },
                new
                {
                    Id = 3,
                    Name = "Wervershoof"
                },
                new
                {
                    Id = 4,
                    Name = "Amsterdam"
                },
                new
                {
                    Id = 5,
                    Name = "Diemen-Zuid"
                }
               );

            seedBuilder.Entity<Room>()
               .HasData(new
               {
                   Id = 1,
                   Name = "Kuiltjes Kamer",
                   StreetName = "KuilengrachtStraat",
                   StreetNumber = 63,
                   PostalCode = "1521 PZ",
                   Price = 450.89,
                   PersonAmount = 4,
                   OwnedById = 1,
                   LocationId = 4
               },
               new
               {
                   Id = 2,
                   Name = "Dorpjes Kamer",
                   StreetName = "Dorpweg",
                   StreetNumber = 108,
                   PostalCode = "1152 KG",
                   Price = 180.33,
                   PersonAmount = 2,
                   OwnedById = 2,
                   LocationId = 3
               },
               new
               {
                   Id = 3,
                   Name = "Kamer bij de Pontjes",
                   StreetName = "Ponterweg",
                   StreetNumber = 13,
                   PostalCode = "1004 AS",
                   Price = 79.99,
                   PersonAmount = 1,
                   OwnedById = 3,
                   LocationId = 5
               },
               new
               {
                   Id = 4,
                   Name = "Brinky Kamer(Niet het koekje)",
                   StreetName = "BrinkStraat",
                   StreetNumber = 160,
                   PostalCode = "1012 AB",
                   Price = 140.50,
                   PersonAmount = 2,
                   OwnedById = 1,
                   LocationId = 2
               },
               new
               {
                   Id = 5,
                   Name = "Fruitkamer",
                   StreetName = "Abrikozenhof",
                   StreetNumber = 4,
                   PostalCode = "1326 HA",
                   Price = 90.75,
                   PersonAmount = 2,
                   OwnedById = 2,
                   LocationId = 1
               });

            seedBuilder.Entity<Customer>()
               .HasData(new
               {
                   Id = 1,
                   FirstName = "Roy",
                   LastName = "Smit",
                   PassportNumber = "AA000000001",
                   City = "Wervereesp",
                   StreetName = "Pastaweg",
                   StreetNumber = 12,
                   PostalCode = "0212 PS",
                   FullName = "Roy Smit"
               },
               new
               {
                   Id = 2,
                   FirstName = "Karl",
                   LastName = "Koper",
                   PassportNumber = "BB000000002",
                   City = "Weesperhoof",
                   StreetName = "LinguiniStraat",
                   StreetNumber = 86,
                   PostalCode = "2152 LG",
                   FullName = "Karl Koper"
               },
                new
                {
                    Id = 3,
                    FirstName = "Max",
                    LastName = "Kopersmit",
                    PassportNumber = "CC000000003",
                    City = "Alweershoof",
                    StreetName = "Macaronihof",
                    StreetNumber = 5,
                    PostalCode = "1462 KG",
                    FullName = "Max Kopersmit"
                });
            seedBuilder.Entity<Reservation>()
               .HasData(new
              {
                   Id = 1,
                   StartDate =new DateTime (2022,1,6),
                   EndDate = new DateTime(2022, 1, 8),
                   RoomId = 1,
                   CustomerId = 1

               },
                 new
               {
                   Id = 2,
                   StartDate = new DateTime(2022, 1, 14),
                   EndDate = new DateTime(2022, 1, 17),
                   RoomId = 2,
                   CustomerId = 1
               },
                  new
               {
                   Id = 3,
                   StartDate = new DateTime(2022, 1, 9),
                   EndDate = new DateTime(2022, 1, 12),
                   RoomId = 1,
                   CustomerId = 2
               },
                  new
               {
                   Id = 4,
                   StartDate = new DateTime(2022, 1, 14),
                   EndDate = new DateTime(2022, 1, 17),
                   RoomId = 3,
                   CustomerId = 2
               },
                  new
               {
                   Id = 5,
                   StartDate = new DateTime(2022, 1, 18),
                   EndDate = new DateTime(2022, 1, 19),
                   RoomId = 2,
                   CustomerId = 3
               },
                  new
               {
                   Id = 6,
                   StartDate = new DateTime(2022, 1, 23),
                   EndDate = new DateTime(2022, 1, 25),
                   RoomId = 3,
                   CustomerId = 3
               },
                  new
               {
                   Id = 7,
                   StartDate = new DateTime(2022, 1, 26),
                   EndDate = new DateTime(2022, 1, 28),
                   RoomId = 3,
                   CustomerId = 1
               });

            base.OnModelCreating(seedBuilder);
        }
    }
}
