using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pilllar.Admin.WebApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("f6b322ea-6b72-45c0-8913-faa770115953"), "zeus@gmailcom", "Zeus", "$2$10$sL6A0FTqeDA8F4tjYkj3Fupcd4q6NrjabEq1CH8WyirRJNIfNEAxa", "Manager" },
                    { new Guid("68ae7e00-a5f5-4821-b921-a6cf80ff1623"), "atena@teste.com", "Atena", "$2$10$sL6A0FTqeDA8F4tjYkj3Fupcd4q6NrjabEq1CH8WyirRJNIfNEAxa", "Employee" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
