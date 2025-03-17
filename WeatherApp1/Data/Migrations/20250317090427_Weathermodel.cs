using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherApp1.Data.Migrations
{
    /// <inheritdoc />
    public partial class Weathermodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherForecasts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTemperature = table.Column<int>(type: "int", nullable: false),
                    MinTemperature = table.Column<int>(type: "int", nullable: false),
                    WindSpeed = table.Column<int>(type: "int", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RainProbability = table.Column<int>(type: "int", nullable: false),
                    Sunrise = table.Column<TimeSpan>(type: "time", nullable: false),
                    Sunset = table.Column<TimeSpan>(type: "time", nullable: false),
                    MoonPhase = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherForecasts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherForecasts");
        }
    }
}
