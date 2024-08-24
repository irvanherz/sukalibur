using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Sukalibur.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "organizers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizers", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trip_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(50)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_categories", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    full_name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dob = table.Column<DateOnly>(type: "date", nullable: true),
                    gender = table.Column<int>(type: "int", nullable: false),
                    password = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "trips",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    title = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    organizer_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.id);
                    table.ForeignKey(
                        name: "FK_trips_organizers_organizer_id",
                        column: x => x.organizer_id,
                        principalTable: "organizers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trips_trip_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "trip_categories",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "organizers",
                columns: new[] { "id", "created_at", "email", "name", "phone", "role", "updated_at", "username" },
                values: new object[] { 1, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9634), "root@sukalibur.com", "Root", "14025", 0, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9634), "root" });

            migrationBuilder.InsertData(
                table: "trip_categories",
                columns: new[] { "id", "created_at", "description", "name", "updated_at" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9581), "", "Open Trip", new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9582) },
                    { 2, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9585), "", "Private Trip", new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9585) },
                    { 4, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9586), "", "Group Trip", new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9587) }
                });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "dob", "email", "full_name", "gender", "password", "phone", "role", "updated_at", "username" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(8666), null, "root@sukalibur.com", "Root", 2, "", "", 0, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(8667), "root" },
                    { 11, new DateTime(2024, 8, 1, 1, 9, 46, 930, DateTimeKind.Local).AddTicks(831), null, "Kathy_Cassin17@gmail.com", "Kathy Cassin", 0, "4B9dLkIPOn", "1-550-485-3107", 0, new DateTime(2023, 12, 28, 23, 21, 9, 856, DateTimeKind.Local).AddTicks(9302), "Kathy.Cassin" },
                    { 12, new DateTime(2024, 2, 1, 10, 25, 20, 740, DateTimeKind.Local).AddTicks(1579), null, "Pat37@yahoo.com", "Pat Ward", 0, "6wpS215xjs", "457-908-9371 x143", 0, new DateTime(2024, 2, 2, 19, 35, 28, 273, DateTimeKind.Local).AddTicks(5920), "Pat.Ward" },
                    { 13, new DateTime(2024, 8, 5, 5, 56, 50, 312, DateTimeKind.Local).AddTicks(401), null, "Katherine_Metz@yahoo.com", "Katherine Metz", 2, "OclAQd3RjW", "1-219-274-5419", 0, new DateTime(2024, 8, 4, 0, 13, 38, 674, DateTimeKind.Local).AddTicks(937), "Katherine_Metz" },
                    { 14, new DateTime(2024, 6, 13, 21, 26, 39, 628, DateTimeKind.Local).AddTicks(9066), null, "Jon.DAmore@hotmail.com", "Jon D'Amore", 1, "AWA1y12qAZ", "819.456.3904", 1, new DateTime(2023, 11, 9, 1, 22, 17, 571, DateTimeKind.Local).AddTicks(1390), "Jon27" },
                    { 15, new DateTime(2023, 11, 7, 8, 46, 56, 585, DateTimeKind.Local).AddTicks(6206), null, "Cassandra.Osinski31@hotmail.com", "Cassandra Osinski", 2, "XkSRjQdutA", "895.580.4415 x409", 1, new DateTime(2023, 10, 20, 3, 34, 31, 609, DateTimeKind.Local).AddTicks(6764), "Cassandra_Osinski" },
                    { 16, new DateTime(2024, 4, 12, 0, 41, 52, 58, DateTimeKind.Local).AddTicks(7103), null, "Rachael.Ondricka71@hotmail.com", "Rachael Ondricka", 0, "Kojt7sZKhK", "1-383-459-9083 x83191", 2, new DateTime(2024, 7, 4, 1, 6, 6, 528, DateTimeKind.Local).AddTicks(5940), "Rachael79" },
                    { 17, new DateTime(2023, 8, 28, 4, 47, 45, 116, DateTimeKind.Local).AddTicks(592), null, "Jonathan.Kunde62@yahoo.com", "Jonathan Kunde", 0, "tIqV_rqUur", "1-445-411-9376", 0, new DateTime(2024, 5, 7, 17, 26, 39, 545, DateTimeKind.Local).AddTicks(4631), "Jonathan.Kunde" },
                    { 18, new DateTime(2024, 2, 9, 20, 8, 12, 211, DateTimeKind.Local).AddTicks(8324), null, "Don_Block@gmail.com", "Don Block", 2, "8p0DGangFm", "(645) 409-2519 x84826", 0, new DateTime(2024, 6, 9, 13, 57, 22, 70, DateTimeKind.Local).AddTicks(667), "Don24" },
                    { 19, new DateTime(2024, 3, 10, 23, 10, 14, 447, DateTimeKind.Local).AddTicks(4681), null, "Kelli36@hotmail.com", "Kelli McGlynn", 0, "Hb7GcedScl", "677-911-6569", 2, new DateTime(2024, 4, 14, 18, 26, 4, 932, DateTimeKind.Local).AddTicks(4953), "Kelli74" },
                    { 20, new DateTime(2024, 4, 14, 7, 2, 4, 281, DateTimeKind.Local).AddTicks(5845), null, "Gustavo60@hotmail.com", "Gustavo Bartoletti", 2, "J2YGDCXpPD", "(695) 910-7777 x29275", 0, new DateTime(2024, 7, 29, 22, 44, 36, 801, DateTimeKind.Local).AddTicks(7437), "Gustavo41" },
                    { 21, new DateTime(2024, 1, 1, 13, 16, 29, 204, DateTimeKind.Local).AddTicks(2362), null, "Vincent_Herman53@gmail.com", "Vincent Herman", 1, "nTkqeFppW5", "(461) 304-8475", 1, new DateTime(2024, 8, 20, 20, 57, 18, 704, DateTimeKind.Local).AddTicks(8106), "Vincent70" },
                    { 22, new DateTime(2024, 8, 17, 7, 8, 15, 517, DateTimeKind.Local).AddTicks(5069), null, "Kent47@hotmail.com", "Kent Blick", 0, "UswhRhuf_j", "420-858-2501", 1, new DateTime(2024, 3, 10, 20, 6, 11, 11, DateTimeKind.Local).AddTicks(7742), "Kent_Blick" },
                    { 23, new DateTime(2023, 11, 28, 17, 7, 6, 953, DateTimeKind.Local).AddTicks(8559), null, "Randy84@gmail.com", "Randy Wilderman", 1, "tsi70Tuvrg", "655.288.7674 x92333", 0, new DateTime(2024, 1, 11, 2, 12, 4, 347, DateTimeKind.Local).AddTicks(5115), "Randy_Wilderman61" },
                    { 24, new DateTime(2024, 7, 9, 0, 13, 18, 341, DateTimeKind.Local).AddTicks(6216), null, "Carolyn83@hotmail.com", "Carolyn Koepp", 1, "lTrT3sJGY3", "(875) 872-2686 x225", 1, new DateTime(2024, 5, 17, 8, 38, 38, 39, DateTimeKind.Local).AddTicks(7841), "Carolyn64" },
                    { 25, new DateTime(2023, 10, 12, 14, 25, 27, 350, DateTimeKind.Local).AddTicks(6514), null, "Sara69@yahoo.com", "Sara Stanton", 1, "0LoIxe35dl", "(229) 911-7287", 1, new DateTime(2024, 5, 13, 15, 14, 20, 902, DateTimeKind.Local).AddTicks(2335), "Sara.Stanton57" },
                    { 26, new DateTime(2024, 4, 14, 9, 26, 16, 251, DateTimeKind.Local).AddTicks(5599), null, "Melinda83@gmail.com", "Melinda Wisozk", 0, "E8o2ahVW3o", "(715) 442-9793", 0, new DateTime(2023, 9, 4, 0, 3, 18, 468, DateTimeKind.Local).AddTicks(3523), "Melinda_Wisozk" },
                    { 27, new DateTime(2024, 5, 19, 9, 41, 45, 949, DateTimeKind.Local).AddTicks(2701), null, "Viola82@yahoo.com", "Viola O'Conner", 2, "taeTRzLkl7", "1-218-650-0027", 0, new DateTime(2023, 10, 30, 3, 47, 14, 816, DateTimeKind.Local).AddTicks(2763), "Viola21" },
                    { 28, new DateTime(2024, 5, 3, 9, 12, 6, 239, DateTimeKind.Local).AddTicks(4690), null, "Vera_Wiza@hotmail.com", "Vera Wiza", 2, "FED4OY4U8R", "(341) 544-9285 x66662", 1, new DateTime(2023, 11, 10, 12, 1, 3, 865, DateTimeKind.Local).AddTicks(1555), "Vera.Wiza" },
                    { 29, new DateTime(2024, 1, 28, 9, 10, 1, 798, DateTimeKind.Local).AddTicks(9831), null, "Kevin92@gmail.com", "Kevin Schiller", 0, "gwxOGg0pNw", "(611) 709-2828", 2, new DateTime(2023, 9, 5, 13, 33, 41, 63, DateTimeKind.Local).AddTicks(3778), "Kevin.Schiller" },
                    { 30, new DateTime(2024, 8, 7, 8, 54, 50, 398, DateTimeKind.Local).AddTicks(1300), null, "Ruth_Durgan27@yahoo.com", "Ruth Durgan", 1, "7Hqkn5Wpn_", "332-611-2017 x233", 2, new DateTime(2023, 8, 29, 7, 46, 4, 663, DateTimeKind.Local).AddTicks(4362), "Ruth.Durgan" },
                    { 31, new DateTime(2024, 7, 21, 13, 59, 0, 559, DateTimeKind.Local).AddTicks(7104), null, "Terrell88@yahoo.com", "Terrell Bosco", 0, "jHpNHhK7Up", "315-410-3279", 1, new DateTime(2024, 2, 10, 4, 14, 17, 521, DateTimeKind.Local).AddTicks(191), "Terrell_Bosco" },
                    { 32, new DateTime(2024, 2, 21, 2, 40, 6, 290, DateTimeKind.Local).AddTicks(2803), null, "Blanca.Rohan55@hotmail.com", "Blanca Rohan", 2, "ksKKTYLdhn", "662.823.5771 x2313", 1, new DateTime(2024, 3, 17, 9, 16, 50, 614, DateTimeKind.Local).AddTicks(7270), "Blanca.Rohan" },
                    { 33, new DateTime(2023, 10, 14, 4, 40, 14, 155, DateTimeKind.Local).AddTicks(6342), null, "Carroll.Baumbach15@hotmail.com", "Carroll Baumbach", 1, "twl9lKUsVh", "(915) 624-1624", 2, new DateTime(2024, 2, 15, 21, 32, 22, 648, DateTimeKind.Local).AddTicks(3167), "Carroll0" },
                    { 34, new DateTime(2023, 12, 9, 20, 56, 43, 349, DateTimeKind.Local).AddTicks(5615), null, "Allan.Beahan@hotmail.com", "Allan Beahan", 0, "Q8pfgrAZH2", "1-289-428-9051 x67983", 0, new DateTime(2024, 2, 24, 2, 9, 37, 125, DateTimeKind.Local).AddTicks(4319), "Allan.Beahan75" },
                    { 35, new DateTime(2024, 8, 5, 12, 57, 20, 116, DateTimeKind.Local).AddTicks(7366), null, "Blanca_Runte2@hotmail.com", "Blanca Runte", 1, "vKC6hUtKXg", "419-702-4120", 2, new DateTime(2024, 7, 24, 4, 41, 56, 187, DateTimeKind.Local).AddTicks(6651), "Blanca_Runte21" },
                    { 36, new DateTime(2023, 11, 4, 7, 7, 12, 848, DateTimeKind.Local).AddTicks(552), null, "Kate_Bashirian76@hotmail.com", "Kate Bashirian", 2, "rH4XZPkdah", "832-951-3889 x6450", 1, new DateTime(2024, 6, 17, 1, 19, 33, 761, DateTimeKind.Local).AddTicks(7505), "Kate_Bashirian" },
                    { 37, new DateTime(2024, 7, 5, 0, 40, 16, 474, DateTimeKind.Local).AddTicks(7360), null, "Brent27@yahoo.com", "Brent Kassulke", 2, "ycwe5vEeoL", "310.679.9716 x4094", 2, new DateTime(2023, 11, 16, 20, 51, 9, 484, DateTimeKind.Local).AddTicks(8412), "Brent78" },
                    { 38, new DateTime(2023, 11, 12, 7, 36, 0, 935, DateTimeKind.Local).AddTicks(1825), null, "Alex_Cole71@gmail.com", "Alex Cole", 2, "_9gDrP93n3", "697-306-4676 x8115", 1, new DateTime(2024, 2, 10, 15, 56, 55, 177, DateTimeKind.Local).AddTicks(4924), "Alex.Cole99" },
                    { 39, new DateTime(2023, 8, 31, 19, 7, 24, 863, DateTimeKind.Local).AddTicks(8784), null, "Susie31@gmail.com", "Susie Hoeger", 0, "NGt7aVIRn6", "(993) 717-4405", 0, new DateTime(2023, 8, 28, 1, 7, 16, 606, DateTimeKind.Local).AddTicks(1993), "Susie.Hoeger98" },
                    { 40, new DateTime(2023, 10, 16, 0, 41, 43, 548, DateTimeKind.Local).AddTicks(5762), null, "Dale.Murazik84@hotmail.com", "Dale Murazik", 0, "T1wbzhHzpL", "1-577-968-5813 x862", 1, new DateTime(2023, 12, 21, 9, 41, 50, 217, DateTimeKind.Local).AddTicks(9585), "Dale_Murazik" },
                    { 41, new DateTime(2024, 1, 10, 8, 4, 50, 471, DateTimeKind.Local).AddTicks(6496), null, "Zachary.Ward@gmail.com", "Zachary Ward", 0, "Pn2q3u56c5", "442-646-3404 x2822", 0, new DateTime(2024, 7, 16, 4, 9, 27, 30, DateTimeKind.Local).AddTicks(2458), "Zachary_Ward" },
                    { 42, new DateTime(2023, 10, 4, 2, 55, 59, 729, DateTimeKind.Local).AddTicks(8670), null, "Maria92@hotmail.com", "Maria Schumm", 1, "E2zY3rS9LA", "1-862-230-0653 x70506", 1, new DateTime(2023, 12, 11, 23, 50, 53, 469, DateTimeKind.Local).AddTicks(1623), "Maria33" },
                    { 43, new DateTime(2023, 12, 20, 6, 23, 43, 177, DateTimeKind.Local).AddTicks(9394), null, "Adrienne15@hotmail.com", "Adrienne King", 0, "wlQRZzOwkM", "(467) 514-3178 x79430", 2, new DateTime(2024, 5, 15, 3, 12, 8, 825, DateTimeKind.Local).AddTicks(9177), "Adrienne88" },
                    { 44, new DateTime(2023, 9, 20, 13, 39, 0, 120, DateTimeKind.Local).AddTicks(8227), null, "Edwin.Wilkinson98@hotmail.com", "Edwin Wilkinson", 1, "DN6yP1b8K_", "319.638.6408 x1161", 0, new DateTime(2024, 2, 16, 12, 33, 33, 6, DateTimeKind.Local).AddTicks(8582), "Edwin.Wilkinson" },
                    { 45, new DateTime(2023, 11, 9, 18, 15, 29, 793, DateTimeKind.Local).AddTicks(8389), null, "Cody_Cronin@yahoo.com", "Cody Cronin", 1, "KQGPVCVj64", "1-412-685-5327 x0677", 0, new DateTime(2023, 10, 18, 7, 25, 14, 88, DateTimeKind.Local).AddTicks(6475), "Cody.Cronin51" },
                    { 46, new DateTime(2023, 11, 3, 4, 55, 41, 879, DateTimeKind.Local).AddTicks(9378), null, "Donnie_Murray@hotmail.com", "Donnie Murray", 2, "viTkuXFfVG", "1-928-747-7305", 0, new DateTime(2023, 9, 26, 10, 20, 31, 999, DateTimeKind.Local).AddTicks(7805), "Donnie66" },
                    { 47, new DateTime(2023, 9, 26, 4, 12, 21, 578, DateTimeKind.Local).AddTicks(9673), null, "Natalie27@yahoo.com", "Natalie Fadel", 2, "Wy6_Aqztoq", "816.356.6349", 1, new DateTime(2023, 11, 3, 3, 38, 31, 441, DateTimeKind.Local).AddTicks(9202), "Natalie_Fadel" },
                    { 48, new DateTime(2023, 9, 21, 14, 32, 37, 433, DateTimeKind.Local).AddTicks(6904), null, "Casey_Harvey@hotmail.com", "Casey Harvey", 0, "xGcRyZoBSm", "953-429-5319 x31279", 1, new DateTime(2024, 8, 7, 17, 14, 44, 920, DateTimeKind.Local).AddTicks(7041), "Casey52" },
                    { 49, new DateTime(2023, 12, 25, 20, 46, 36, 203, DateTimeKind.Local).AddTicks(6412), null, "Carroll_Carter@yahoo.com", "Carroll Carter", 0, "m7scOxQzyy", "538.440.0338 x079", 0, new DateTime(2024, 5, 6, 8, 49, 25, 151, DateTimeKind.Local).AddTicks(461), "Carroll_Carter75" },
                    { 50, new DateTime(2024, 5, 12, 21, 1, 49, 725, DateTimeKind.Local).AddTicks(8631), null, "Felipe.Hegmann@gmail.com", "Felipe Hegmann", 2, "_jDjPvU5yz", "766-357-5046", 2, new DateTime(2024, 1, 20, 20, 55, 1, 943, DateTimeKind.Local).AddTicks(3644), "Felipe14" },
                    { 51, new DateTime(2024, 6, 27, 16, 33, 27, 31, DateTimeKind.Local).AddTicks(2557), null, "Ricky.Farrell@gmail.com", "Ricky Farrell", 1, "jsv0aLDpsM", "323.846.0196 x581", 2, new DateTime(2023, 12, 30, 1, 45, 44, 218, DateTimeKind.Local).AddTicks(8647), "Ricky.Farrell" },
                    { 52, new DateTime(2024, 5, 3, 21, 3, 47, 557, DateTimeKind.Local).AddTicks(853), null, "Antoinette_Batz18@yahoo.com", "Antoinette Batz", 0, "FgjLHDoWut", "1-539-427-2467", 0, new DateTime(2023, 10, 28, 5, 8, 27, 596, DateTimeKind.Local).AddTicks(9541), "Antoinette69" },
                    { 53, new DateTime(2024, 2, 26, 9, 54, 53, 487, DateTimeKind.Local).AddTicks(4066), null, "Jennifer_Pfeffer62@gmail.com", "Jennifer Pfeffer", 1, "ZEC5mnUCXm", "1-298-813-1171 x28362", 0, new DateTime(2024, 4, 24, 21, 58, 13, 118, DateTimeKind.Local).AddTicks(3707), "Jennifer_Pfeffer99" },
                    { 54, new DateTime(2024, 7, 13, 18, 24, 5, 481, DateTimeKind.Local).AddTicks(1663), null, "Lloyd34@gmail.com", "Lloyd Ullrich", 0, "aCw8JWsMP5", "1-683-380-4965 x0545", 2, new DateTime(2024, 1, 26, 10, 0, 13, 136, DateTimeKind.Local).AddTicks(3038), "Lloyd.Ullrich82" },
                    { 55, new DateTime(2024, 5, 30, 21, 16, 16, 564, DateTimeKind.Local).AddTicks(5036), null, "Sherman_Kshlerin@yahoo.com", "Sherman Kshlerin", 1, "wPu4lGDKtF", "982-980-2147 x53819", 0, new DateTime(2023, 10, 19, 20, 10, 30, 993, DateTimeKind.Local).AddTicks(9504), "Sherman_Kshlerin20" },
                    { 56, new DateTime(2024, 4, 5, 9, 14, 11, 488, DateTimeKind.Local).AddTicks(3497), null, "Yolanda.Harber40@hotmail.com", "Yolanda Harber", 0, "MFkdvziyFU", "479.693.2796", 1, new DateTime(2024, 1, 17, 13, 21, 34, 353, DateTimeKind.Local).AddTicks(6951), "Yolanda_Harber19" },
                    { 57, new DateTime(2024, 8, 16, 19, 47, 32, 941, DateTimeKind.Local).AddTicks(2314), null, "Jenny40@gmail.com", "Jenny Kuhlman", 2, "u3FJxGxb8L", "563-626-6294", 1, new DateTime(2023, 8, 29, 8, 17, 52, 480, DateTimeKind.Local).AddTicks(2877), "Jenny_Kuhlman" },
                    { 58, new DateTime(2024, 3, 17, 7, 35, 52, 456, DateTimeKind.Local).AddTicks(2995), null, "Sam.Connelly@yahoo.com", "Sam Connelly", 0, "LamtbNH3hM", "518-345-2905", 2, new DateTime(2024, 7, 28, 6, 1, 17, 542, DateTimeKind.Local).AddTicks(1985), "Sam60" },
                    { 59, new DateTime(2024, 3, 30, 18, 12, 50, 975, DateTimeKind.Local).AddTicks(4704), null, "Robert.Homenick15@gmail.com", "Robert Homenick", 0, "2gf46OgJOy", "862-964-4237 x69405", 0, new DateTime(2024, 8, 22, 11, 19, 45, 61, DateTimeKind.Local).AddTicks(5797), "Robert80" },
                    { 60, new DateTime(2023, 11, 4, 2, 11, 16, 329, DateTimeKind.Local).AddTicks(5397), null, "Maryann56@yahoo.com", "Maryann Kemmer", 1, "02fVzNKEnU", "282-498-7113", 0, new DateTime(2023, 12, 27, 10, 51, 14, 764, DateTimeKind.Local).AddTicks(5980), "Maryann_Kemmer" },
                    { 61, new DateTime(2024, 3, 5, 20, 12, 42, 272, DateTimeKind.Local).AddTicks(8873), null, "Rebecca8@hotmail.com", "Rebecca Runte", 2, "v_g_v8Fhlg", "(824) 561-3878", 0, new DateTime(2023, 9, 27, 20, 39, 56, 980, DateTimeKind.Local).AddTicks(9001), "Rebecca_Runte91" },
                    { 62, new DateTime(2024, 4, 23, 8, 6, 44, 73, DateTimeKind.Local).AddTicks(166), null, "Christy.Wehner@gmail.com", "Christy Wehner", 2, "wBDI7bFZYt", "(803) 505-0193 x514", 2, new DateTime(2024, 2, 19, 0, 57, 22, 291, DateTimeKind.Local).AddTicks(3548), "Christy14" },
                    { 63, new DateTime(2024, 4, 25, 20, 57, 4, 382, DateTimeKind.Local).AddTicks(9227), null, "Elisa.DuBuque@hotmail.com", "Elisa DuBuque", 2, "MPPiJEL8pR", "823.798.0737 x77010", 2, new DateTime(2024, 8, 4, 14, 12, 38, 920, DateTimeKind.Local).AddTicks(6048), "Elisa80" },
                    { 64, new DateTime(2024, 3, 21, 22, 48, 30, 595, DateTimeKind.Local).AddTicks(3794), null, "Dale_Prohaska39@hotmail.com", "Dale Prohaska", 1, "mLyuMFA2qa", "607-462-1425", 0, new DateTime(2023, 9, 13, 5, 34, 35, 840, DateTimeKind.Local).AddTicks(3810), "Dale49" },
                    { 65, new DateTime(2024, 2, 22, 1, 31, 59, 387, DateTimeKind.Local).AddTicks(8855), null, "Bill70@yahoo.com", "Bill Klein", 0, "Ay9cS7MqFB", "667.885.8690 x0324", 1, new DateTime(2023, 11, 6, 11, 18, 23, 291, DateTimeKind.Local).AddTicks(9338), "Bill_Klein" },
                    { 66, new DateTime(2023, 9, 2, 8, 56, 49, 985, DateTimeKind.Local).AddTicks(2690), null, "Luther.Fahey@gmail.com", "Luther Fahey", 1, "jqcyU9_4S9", "1-480-438-1713", 1, new DateTime(2024, 8, 11, 17, 2, 1, 390, DateTimeKind.Local).AddTicks(6084), "Luther_Fahey70" },
                    { 67, new DateTime(2024, 6, 6, 4, 43, 36, 406, DateTimeKind.Local).AddTicks(3934), null, "Misty_Lockman@hotmail.com", "Misty Lockman", 2, "Wy24_rGDei", "(582) 230-2275 x6840", 2, new DateTime(2023, 9, 10, 23, 9, 37, 888, DateTimeKind.Local).AddTicks(1187), "Misty39" },
                    { 68, new DateTime(2024, 3, 3, 2, 56, 43, 622, DateTimeKind.Local).AddTicks(9051), null, "Neal87@gmail.com", "Neal Klein", 1, "62WUrs2Zxp", "1-813-201-0094 x64638", 0, new DateTime(2024, 6, 28, 11, 14, 19, 415, DateTimeKind.Local).AddTicks(2352), "Neal_Klein19" },
                    { 69, new DateTime(2023, 10, 7, 13, 1, 30, 768, DateTimeKind.Local).AddTicks(3302), null, "Taylor.Barton@hotmail.com", "Taylor Barton", 0, "0fDIt8GVJO", "389-268-7831 x566", 2, new DateTime(2023, 11, 17, 8, 50, 26, 758, DateTimeKind.Local).AddTicks(3403), "Taylor.Barton" },
                    { 70, new DateTime(2023, 12, 27, 14, 31, 35, 503, DateTimeKind.Local).AddTicks(5336), null, "Abel_Herman88@gmail.com", "Abel Herman", 0, "iEKOsbFO8F", "525-718-4633", 0, new DateTime(2024, 6, 2, 21, 30, 24, 554, DateTimeKind.Local).AddTicks(1287), "Abel.Herman14" },
                    { 71, new DateTime(2024, 8, 24, 8, 23, 25, 367, DateTimeKind.Local).AddTicks(651), null, "Jeffrey_Treutel@yahoo.com", "Jeffrey Treutel", 0, "vFA5A9PrRe", "345-454-6578", 1, new DateTime(2024, 1, 5, 16, 37, 31, 246, DateTimeKind.Local).AddTicks(9952), "Jeffrey53" },
                    { 72, new DateTime(2023, 10, 10, 8, 23, 49, 222, DateTimeKind.Local).AddTicks(9813), null, "Leigh.Stamm85@gmail.com", "Leigh Stamm", 0, "Et_OP2GKmU", "361-880-6266 x9698", 1, new DateTime(2023, 12, 7, 18, 45, 41, 550, DateTimeKind.Local).AddTicks(8143), "Leigh.Stamm" },
                    { 73, new DateTime(2023, 12, 29, 15, 47, 34, 710, DateTimeKind.Local).AddTicks(9616), null, "Willie.Abshire@hotmail.com", "Willie Abshire", 0, "uZAP19xBTB", "763.772.3369 x079", 1, new DateTime(2023, 10, 4, 2, 33, 47, 430, DateTimeKind.Local).AddTicks(1865), "Willie24" },
                    { 74, new DateTime(2023, 11, 17, 9, 47, 41, 161, DateTimeKind.Local).AddTicks(8133), null, "Dustin.Schmitt0@gmail.com", "Dustin Schmitt", 2, "TluHl42rxE", "1-577-296-1444", 1, new DateTime(2024, 8, 21, 23, 58, 57, 255, DateTimeKind.Local).AddTicks(2674), "Dustin_Schmitt41" },
                    { 75, new DateTime(2023, 10, 6, 0, 15, 35, 402, DateTimeKind.Local).AddTicks(4225), null, "Garrett88@gmail.com", "Garrett Kautzer", 2, "xbCX9pFsY6", "(287) 332-4006 x25102", 2, new DateTime(2024, 1, 23, 3, 30, 40, 141, DateTimeKind.Local).AddTicks(5570), "Garrett25" },
                    { 76, new DateTime(2023, 10, 28, 9, 23, 10, 133, DateTimeKind.Local).AddTicks(7869), null, "Randolph.Senger@gmail.com", "Randolph Senger", 2, "CuctiNtwgu", "1-690-246-8003 x302", 1, new DateTime(2024, 7, 29, 15, 8, 4, 150, DateTimeKind.Local).AddTicks(7414), "Randolph.Senger" },
                    { 77, new DateTime(2024, 6, 13, 2, 12, 50, 836, DateTimeKind.Local).AddTicks(1196), null, "Robyn70@hotmail.com", "Robyn Koch", 2, "0H8aA_ed_h", "483.652.9986 x901", 1, new DateTime(2023, 10, 24, 3, 19, 49, 178, DateTimeKind.Local).AddTicks(766), "Robyn98" },
                    { 78, new DateTime(2023, 9, 9, 18, 12, 19, 372, DateTimeKind.Local).AddTicks(3799), null, "Lucille.Baumbach@hotmail.com", "Lucille Baumbach", 0, "9kV2cqOvRM", "(489) 659-7034", 0, new DateTime(2024, 1, 20, 2, 35, 14, 676, DateTimeKind.Local).AddTicks(6767), "Lucille59" },
                    { 79, new DateTime(2024, 1, 13, 18, 42, 27, 864, DateTimeKind.Local).AddTicks(797), null, "Alison_Schroeder@yahoo.com", "Alison Schroeder", 1, "xAC0Q2Koj3", "684.432.8696 x420", 2, new DateTime(2023, 10, 19, 0, 19, 14, 168, DateTimeKind.Local).AddTicks(8533), "Alison_Schroeder" },
                    { 80, new DateTime(2023, 12, 31, 8, 55, 10, 134, DateTimeKind.Local).AddTicks(2971), null, "Wesley.Ferry31@yahoo.com", "Wesley Ferry", 2, "S2WI8tkxC0", "808.403.2264", 0, new DateTime(2024, 5, 19, 12, 53, 22, 239, DateTimeKind.Local).AddTicks(9259), "Wesley.Ferry" },
                    { 81, new DateTime(2023, 12, 28, 1, 15, 4, 32, DateTimeKind.Local).AddTicks(4760), null, "Jon_Spinka@hotmail.com", "Jon Spinka", 0, "jmGrbwkAgb", "1-744-445-4560", 2, new DateTime(2024, 5, 7, 18, 58, 23, 689, DateTimeKind.Local).AddTicks(3911), "Jon.Spinka43" },
                    { 82, new DateTime(2023, 10, 5, 8, 34, 42, 664, DateTimeKind.Local).AddTicks(9036), null, "Steven19@gmail.com", "Steven Leannon", 0, "9UyaIf2xoW", "288.960.7707 x1972", 2, new DateTime(2024, 5, 16, 13, 7, 34, 939, DateTimeKind.Local).AddTicks(3349), "Steven50" },
                    { 83, new DateTime(2024, 4, 14, 15, 21, 59, 486, DateTimeKind.Local).AddTicks(9900), null, "Nettie.White3@hotmail.com", "Nettie White", 2, "5KX9CxBMw1", "792-321-0535", 1, new DateTime(2024, 5, 20, 11, 53, 40, 530, DateTimeKind.Local).AddTicks(2780), "Nettie.White42" },
                    { 84, new DateTime(2024, 6, 17, 6, 32, 18, 267, DateTimeKind.Local).AddTicks(1617), null, "Marsha53@hotmail.com", "Marsha Lubowitz", 1, "T7obodXcDG", "909-363-1483 x7577", 2, new DateTime(2023, 10, 23, 16, 13, 13, 643, DateTimeKind.Local).AddTicks(2794), "Marsha82" },
                    { 85, new DateTime(2023, 11, 5, 14, 31, 19, 914, DateTimeKind.Local).AddTicks(7776), null, "Robin_Okuneva@yahoo.com", "Robin Okuneva", 0, "aWv45La4Vs", "430-374-8840", 0, new DateTime(2023, 12, 2, 17, 7, 38, 52, DateTimeKind.Local).AddTicks(868), "Robin_Okuneva21" },
                    { 86, new DateTime(2024, 3, 18, 22, 32, 5, 2, DateTimeKind.Local).AddTicks(660), null, "Grant.Gleichner@yahoo.com", "Grant Gleichner", 2, "YoB1DI_yGo", "(988) 870-9777 x639", 2, new DateTime(2023, 9, 7, 20, 15, 22, 436, DateTimeKind.Local).AddTicks(3349), "Grant.Gleichner" },
                    { 87, new DateTime(2023, 11, 24, 17, 55, 58, 901, DateTimeKind.Local).AddTicks(6135), null, "Courtney.Kreiger@hotmail.com", "Courtney Kreiger", 0, "Dr_MaxnxTR", "1-242-462-1423", 2, new DateTime(2023, 10, 31, 3, 55, 54, 942, DateTimeKind.Local).AddTicks(336), "Courtney.Kreiger79" },
                    { 88, new DateTime(2024, 4, 13, 12, 38, 21, 355, DateTimeKind.Local).AddTicks(7319), null, "Virgil_Gorczany73@yahoo.com", "Virgil Gorczany", 0, "xrzIjpvTTH", "(814) 486-0904", 1, new DateTime(2024, 2, 5, 15, 33, 7, 641, DateTimeKind.Local).AddTicks(41), "Virgil_Gorczany10" },
                    { 89, new DateTime(2024, 4, 18, 21, 14, 47, 652, DateTimeKind.Local).AddTicks(511), null, "Arturo.Macejkovic@yahoo.com", "Arturo Macejkovic", 0, "2LTlSVy_lP", "805.440.7508", 1, new DateTime(2023, 10, 15, 3, 6, 21, 317, DateTimeKind.Local).AddTicks(5322), "Arturo.Macejkovic" },
                    { 90, new DateTime(2024, 7, 12, 7, 50, 22, 490, DateTimeKind.Local).AddTicks(8459), null, "Kristina95@gmail.com", "Kristina Olson", 2, "JZML032l8h", "(688) 705-7172 x01437", 0, new DateTime(2023, 8, 31, 10, 56, 52, 341, DateTimeKind.Local).AddTicks(9660), "Kristina79" },
                    { 91, new DateTime(2024, 1, 31, 5, 16, 52, 731, DateTimeKind.Local).AddTicks(1874), null, "Mark.Bartoletti@yahoo.com", "Mark Bartoletti", 1, "oMFEhhllIl", "348.823.5001 x5518", 1, new DateTime(2023, 12, 5, 19, 59, 59, 886, DateTimeKind.Local).AddTicks(2943), "Mark.Bartoletti78" },
                    { 92, new DateTime(2024, 6, 7, 14, 42, 24, 976, DateTimeKind.Local).AddTicks(6384), null, "Brad_Doyle2@hotmail.com", "Brad Doyle", 2, "nQMpni5gFN", "1-771-262-6563", 2, new DateTime(2024, 3, 23, 8, 59, 24, 889, DateTimeKind.Local).AddTicks(216), "Brad_Doyle" },
                    { 93, new DateTime(2024, 3, 31, 22, 25, 39, 765, DateTimeKind.Local).AddTicks(3786), null, "Heidi.Walter@hotmail.com", "Heidi Walter", 2, "8MGUl3xn1T", "1-957-946-8302 x98502", 0, new DateTime(2023, 12, 18, 1, 50, 24, 681, DateTimeKind.Local).AddTicks(1290), "Heidi79" },
                    { 94, new DateTime(2024, 8, 5, 3, 51, 18, 876, DateTimeKind.Local).AddTicks(7281), null, "William_Schamberger@hotmail.com", "William Schamberger", 1, "AlMAQplOKm", "(277) 460-7025 x54405", 2, new DateTime(2023, 12, 6, 12, 33, 42, 516, DateTimeKind.Local).AddTicks(8730), "William_Schamberger31" },
                    { 95, new DateTime(2023, 10, 10, 0, 54, 56, 322, DateTimeKind.Local).AddTicks(537), null, "Jodi.Hettinger@yahoo.com", "Jodi Hettinger", 0, "XTATlpvvaK", "1-753-858-1139 x64032", 0, new DateTime(2024, 1, 25, 17, 48, 16, 574, DateTimeKind.Local).AddTicks(1138), "Jodi.Hettinger" },
                    { 96, new DateTime(2024, 8, 14, 3, 17, 25, 575, DateTimeKind.Local).AddTicks(3863), null, "Laverne_Borer51@hotmail.com", "Laverne Borer", 2, "GnswLef8Lw", "827.621.1595 x506", 0, new DateTime(2024, 3, 17, 5, 41, 56, 50, DateTimeKind.Local).AddTicks(9935), "Laverne_Borer" },
                    { 97, new DateTime(2023, 10, 7, 6, 6, 2, 912, DateTimeKind.Local).AddTicks(7919), null, "Tyrone.Runolfsson55@gmail.com", "Tyrone Runolfsson", 1, "7gs6_NbInA", "513.244.9359", 2, new DateTime(2023, 12, 27, 21, 55, 10, 299, DateTimeKind.Local).AddTicks(4075), "Tyrone21" },
                    { 98, new DateTime(2023, 9, 1, 18, 39, 22, 183, DateTimeKind.Local).AddTicks(5207), null, "Pearl.Sauer40@gmail.com", "Pearl Sauer", 2, "BnPn7ah5Hk", "398-674-7745 x97800", 1, new DateTime(2023, 8, 31, 16, 14, 26, 607, DateTimeKind.Local).AddTicks(5488), "Pearl_Sauer" },
                    { 99, new DateTime(2023, 12, 12, 17, 53, 41, 915, DateTimeKind.Local).AddTicks(6277), null, "Taylor_Sawayn82@gmail.com", "Taylor Sawayn", 2, "Vez3Qg90MQ", "544.899.2931 x00079", 0, new DateTime(2023, 11, 27, 15, 35, 36, 178, DateTimeKind.Local).AddTicks(1894), "Taylor_Sawayn70" },
                    { 100, new DateTime(2023, 11, 21, 23, 58, 25, 958, DateTimeKind.Local).AddTicks(7389), null, "Lucia_Cummings70@hotmail.com", "Lucia Cummings", 1, "V2lAhKua6a", "782-731-1634 x87814", 1, new DateTime(2024, 6, 18, 7, 15, 58, 719, DateTimeKind.Local).AddTicks(9568), "Lucia.Cummings" },
                    { 101, new DateTime(2024, 8, 12, 8, 52, 10, 367, DateTimeKind.Local).AddTicks(9441), null, "Israel_Carter58@hotmail.com", "Israel Carter", 2, "B9UDFHKS3f", "1-710-688-9474", 0, new DateTime(2023, 10, 16, 4, 15, 59, 753, DateTimeKind.Local).AddTicks(6933), "Israel80" },
                    { 102, new DateTime(2024, 8, 19, 20, 43, 9, 500, DateTimeKind.Local).AddTicks(3298), null, "Rachael57@yahoo.com", "Rachael Kovacek", 1, "aRskDmlVoP", "1-595-293-2772", 1, new DateTime(2024, 5, 28, 10, 12, 1, 901, DateTimeKind.Local).AddTicks(9271), "Rachael96" },
                    { 103, new DateTime(2023, 10, 23, 1, 46, 25, 55, DateTimeKind.Local).AddTicks(4386), null, "Joanne.Koss@gmail.com", "Joanne Koss", 0, "7UJgTKUdlW", "967.797.4198 x7070", 1, new DateTime(2024, 2, 24, 3, 53, 9, 528, DateTimeKind.Local).AddTicks(9416), "Joanne.Koss21" },
                    { 104, new DateTime(2024, 1, 11, 19, 20, 15, 528, DateTimeKind.Local).AddTicks(2347), null, "Kent77@yahoo.com", "Kent Morissette", 1, "wDa3uFy2gq", "1-957-405-3144 x5949", 1, new DateTime(2024, 7, 24, 9, 4, 22, 975, DateTimeKind.Local).AddTicks(6033), "Kent_Morissette" },
                    { 105, new DateTime(2024, 2, 7, 11, 6, 3, 875, DateTimeKind.Local).AddTicks(8897), null, "Tanya_Koch@yahoo.com", "Tanya Koch", 1, "Md0Wfdh0cD", "977.421.1339", 1, new DateTime(2023, 12, 2, 17, 2, 57, 110, DateTimeKind.Local).AddTicks(1865), "Tanya.Koch48" },
                    { 106, new DateTime(2023, 12, 10, 12, 50, 5, 410, DateTimeKind.Local).AddTicks(181), null, "Heather87@gmail.com", "Heather Block", 1, "prY7dOgOvm", "(745) 324-4344 x35953", 2, new DateTime(2024, 6, 13, 21, 26, 17, 331, DateTimeKind.Local).AddTicks(5995), "Heather58" },
                    { 107, new DateTime(2023, 11, 24, 14, 27, 13, 201, DateTimeKind.Local).AddTicks(9899), null, "Lindsey.Kuhic@hotmail.com", "Lindsey Kuhic", 2, "mDAolLDswR", "1-266-675-6394", 0, new DateTime(2023, 10, 11, 1, 55, 42, 246, DateTimeKind.Local).AddTicks(1432), "Lindsey.Kuhic83" },
                    { 108, new DateTime(2023, 11, 28, 11, 51, 9, 622, DateTimeKind.Local).AddTicks(749), null, "Kim_Pfannerstill@hotmail.com", "Kim Pfannerstill", 2, "XGGgyVlJLy", "1-450-998-1623", 1, new DateTime(2023, 10, 23, 7, 55, 4, 161, DateTimeKind.Local).AddTicks(9088), "Kim_Pfannerstill" },
                    { 109, new DateTime(2024, 3, 18, 17, 33, 35, 561, DateTimeKind.Local).AddTicks(1076), null, "Marvin_Okuneva@yahoo.com", "Marvin Okuneva", 0, "XC4NxhNQld", "1-992-588-8934 x160", 0, new DateTime(2024, 2, 20, 9, 39, 17, 375, DateTimeKind.Local).AddTicks(2005), "Marvin.Okuneva" },
                    { 110, new DateTime(2023, 11, 19, 2, 22, 35, 587, DateTimeKind.Local).AddTicks(5573), null, "Carla77@gmail.com", "Carla Wilkinson", 2, "y2DcjxaC6p", "(915) 780-2854 x7713", 0, new DateTime(2023, 11, 14, 4, 16, 59, 994, DateTimeKind.Local).AddTicks(7060), "Carla.Wilkinson" }
                });

            migrationBuilder.InsertData(
                table: "trips",
                columns: new[] { "id", "category_id", "created_at", "description", "organizer_id", "title", "updated_at" },
                values: new object[] { 1, 1, new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9662), "Main Trip Description", 1, "Main Trip", new DateTime(2024, 8, 24, 2, 20, 45, 296, DateTimeKind.Utc).AddTicks(9663) });

            migrationBuilder.CreateIndex(
                name: "IX_trips_category_id",
                table: "trips",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_organizer_id",
                table: "trips",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trips");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "organizers");

            migrationBuilder.DropTable(
                name: "trip_categories");
        }
    }
}
