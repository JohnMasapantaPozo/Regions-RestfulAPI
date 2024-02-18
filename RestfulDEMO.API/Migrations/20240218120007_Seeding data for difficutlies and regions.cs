using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestfulDEMO.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdatafordifficutliesandregions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("58dfb073-0e35-4b40-ada5-efd3b36974eb"), "Medium" },
                    { new Guid("7b0c6301-0a03-4e1b-9021-8419ab0ef7d4"), "Hard" },
                    { new Guid("c66e04fd-ba85-423f-8314-2cae09983a59"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("4c55f1bf-2af7-4463-8b55-e991e7377c4f"), "OSL", "Oslo", "http/oslo/picture" },
                    { new Guid("8671f7ab-9c94-44ba-bdd4-b3d9e0dc4261"), "STV", "Stavanger", "http/stavanger/picture" },
                    { new Guid("e4e46541-1ce3-4ffe-8f89-f07f0259450f"), "BRG", "Bergen", "http/bergen/picture" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("58dfb073-0e35-4b40-ada5-efd3b36974eb"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7b0c6301-0a03-4e1b-9021-8419ab0ef7d4"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("c66e04fd-ba85-423f-8314-2cae09983a59"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4c55f1bf-2af7-4463-8b55-e991e7377c4f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("8671f7ab-9c94-44ba-bdd4-b3d9e0dc4261"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e4e46541-1ce3-4ffe-8f89-f07f0259450f"));
        }
    }
}
