using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Asset___Vuln_Manager_.API.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)// This method defines the operations to apply the migration, such as creating tables, adding columns, etc.
        {
            migrationBuilder.CreateTable( 
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IpAddress = table.Column<string>(type: "TEXT", nullable: false),
                    LastSeen = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Criticality = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                }); // This code creates a new table called "Assets" with the specified columns and sets the primary key on the "Id" column. The "Id" column is configured to auto-increment, meaning it will automatically generate a unique value for each new record inserted into the table.
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");
        }
    }
}
