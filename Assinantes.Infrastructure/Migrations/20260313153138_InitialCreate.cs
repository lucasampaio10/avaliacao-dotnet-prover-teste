using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assinantes.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assinantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NomeCompleto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DataInicioAssinatura = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Plano = table.Column<int>(type: "int", nullable: false),
                    ValorMensal = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assinantes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assinantes_Email",
                table: "Assinantes",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assinantes");
        }
    }
}
