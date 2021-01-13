using System;
using System.Collections.Generic;
using MaxWell.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MaxWell.Shared.Models;
using MaxWell.Shared.Models;

namespace MaxWell.Server.Migrations
{
    public partial class IngredientMigration : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Ingredient", columns: table => new
            {
                IngredientId = table.Column<int>(nullable: false).Annotation("Sqlite:Autoincrement", true).Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                CreateDateTime = table.Column<DateTime>(nullable: true),
                PersonId = table.Column<int>(nullable: true),
                ImageUrl = table.Column<string>(nullable: true)
            }, constraints: table => { table.PrimaryKey("PK_Ingredient", x => x.IngredientId); });

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredient");
        }
    }
}
