using Microsoft.EntityFrameworkCore;
using Pilllar.Vocal.Domain;
using System;

namespace Pilllar.Vocal.Data.Repositories
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(
            new User { Id = Guid.NewGuid(), Name = "Zeus", Email = "zeus@gmailcom", Password = "$2$10$sL6A0FTqeDA8F4tjYkj3Fupcd4q6NrjabEq1CH8WyirRJNIfNEAxa", Role = "Manager" },
            new User { Id = Guid.NewGuid(), Name = "Atena", Email = "atena@teste.com", Password = "$2$10$sL6A0FTqeDA8F4tjYkj3Fupcd4q6NrjabEq1CH8WyirRJNIfNEAxa", Role = "Employee" }
            );
        }
    }
}
