using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MatissHW.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_updated_epoch = table.Column<int>(type: "int", nullable: false),
                    Temp_c = table.Column<double>(type: "float", nullable: false),
                    Wind_kph = table.Column<double>(type: "float", nullable: false),
                    Wind_degree = table.Column<int>(type: "int", nullable: false),
                    Wind_dir = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherReport", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherReport");
        }
    }
}
