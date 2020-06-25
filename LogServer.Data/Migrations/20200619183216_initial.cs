using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LogServer.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogEvents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    Level = table.Column<string>(nullable: true),
                    RenderedMessage = table.Column<string>(nullable: true),
                    MessageTemplate = table.Column<string>(type: "VARCHAR(255)", nullable: true),
                    Properties = table.Column<string>(type: "VARCHAR(4000)", nullable: true),
                    Exception = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEvents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogEvents");
        }
    }
}
