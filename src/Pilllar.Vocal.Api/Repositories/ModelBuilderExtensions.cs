using Microsoft.EntityFrameworkCore;
using Pilllar.Vocal.Models;
using System;

namespace Pilllar.Vocal.Repositories
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
            new User {
                //Id = Guid.NewGuid(), 
                Id = Guid.Parse("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                Name = "Zeus", 
                Email = "zeus@gmailcom", 
                Password = "$2$10$sL6A0FTqeDA8F4tjYkj3Fupcd4q6NrjabEq1CH8WyirRJNIfNEAxa", 
                Role = "Manager" 
            },
            new User {
                //Id = Guid.NewGuid(), 
                Id = Guid.Parse("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                Name = "Atena", Email = "atena@teste.com", 
                Password = "$2$10$sL6A0FTqeDA8F4tjYkj3Fupcd4q6NrjabEq1CH8WyirRJNIfNEAxa", 
                Role = "Employee" }
            );
        }
    }
}
