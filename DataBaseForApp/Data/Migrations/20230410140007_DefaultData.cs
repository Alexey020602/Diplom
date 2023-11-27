using DataBase.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataBase.Data.Migrations
{
    /// <inheritdoc />
    public partial class DefaultData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Добавление факультетов
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {1, "ИФИО"}
                );
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {2, "ИНПРОТЕХ"}
            );
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {3, "ФРТ"}
            );
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {4, "ФЭЛ"}
            );
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {5, "ФКТИ"}
            );
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {6, "ФЭА"}
            );
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {7, "ФИБС"}
            );
            migrationBuilder.InsertData(
                table:"Faculties",
                columns: new[]{"Id", "Name"},
                values: new object[] {8, "ГФ"}
            );

            //Подразделения
            migrationBuilder.InsertData(
                table:"Divisions", 
                columns: new[] {"Id", "FullName", "ShortName", "FacultyId", "Contacts", "Site" }, 
                values: new object[] {1, "Кафедра автоматизации и процессов управления", "АПУ", 5, "Контактные данные кафедры", "https://some.ru"}
                );

            migrationBuilder.InsertData(
                table: "Divisions",
                columns: new[] { "Id", "FullName", "ShortName", "FacultyId", "Contacts", "Site" },
                values: new object[] { 2, "Кафедра какая-то", "каф", 2, "Контактные данные кафедры", "https://some2.ru" }
                );

            //Тип партнера
            migrationBuilder.InsertData(
                table:"PartnerTypes",
                columns:new[]{"Id", "Name"},
                values: new object[]{1, "НИИ"}
                );
            migrationBuilder.InsertData(
                table: "PartnerTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "ВУЗ" }
                );

            //Партнеры
            migrationBuilder.InsertData(
                table: "Partners",
                columns: new []
                {
                    nameof(Partner.Id),
                    nameof(Partner.FullName),
                    nameof(Partner.ShortName),
                    nameof(Partner.Address),
                    nameof(Partner.Site),
                    "PartnerTypeId"
                },
                values: new object[]
                {
                    1,
                    "Полное имя 1",
                    "Короткое имя 1",
                    "Какой-то адрес 1",
                    "www.site.ru",
                    1
                }
            );
            migrationBuilder.InsertData(
                table: "Partners",
                columns: new []
                {
                    nameof(Partner.Id),
                    nameof(Partner.FullName),
                    nameof(Partner.ShortName),
                    nameof(Partner.Address),
                    nameof(Partner.Site),
                    "PartnerTypeId"
                },
                values: new object[]
                {
                    2,
                    "Полное имя 2",
                    "Короткое имя 2",
                    "Какой-то адрес 2",
                    "www.site2.ru",
                    2
                }
            );
            migrationBuilder.InsertData(
                table: "Partners",
                columns: new []
                {
                    nameof(Partner.Id),
                    nameof(Partner.FullName),
                    nameof(Partner.ShortName),
                    nameof(Partner.Address),
                    nameof(Partner.Site),
                    "PartnerTypeId"
                },
                values: new object[]
                {
                    3,
                    "Полное имя 3",
                    "Короткое имя 3",
                    "Какой-то адрес 3",
                    "www.site3.ru",
                    1
                }
            );
            migrationBuilder.InsertData(
                table: "Partners",
                columns: new []
                {
                    nameof(Partner.Id),
                    nameof(Partner.FullName),
                    nameof(Partner.ShortName),
                    nameof(Partner.Address),
                    nameof(Partner.Site),
                    "PartnerTypeId"
                },
                values: new object[]
                {
                    4,
                    "Полное имя 4",
                    "Короткое имя 4",
                    "Какой-то адрес 4",
                    "www.site4.ru",
                    2
                }
            );

            //Тип взаимодействия
            migrationBuilder.InsertData(
                table: "InteractionTypes",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Тип взаимодействия" }
                    );
            //Взаимодействие
            //migrationBuilder.InsertData(
            //    table: "Interactions",
            //    columns: new[] { "Id", "PartnerId", "DivisionId", "InteractionTypeId", "Theme", "ContactCode", "SigningDateTime", "BeginingDateTime", "EndingDateTime" },
            //    values: new object[] { 1, 2, 1, 1, "Тема взаимодействия", "43454644", new DateTime(2023, 02, 11), new DateTime(2023, 3,3), new DateTime(2024, 3, 3)}
            //        );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table:"PartnerTypes",
                keyColumn:"Id",
                keyValue:1
            );
            migrationBuilder.DeleteData(
                table:"Partner",
                keyColumn:"Id",
                keyValue:1
            );
            migrationBuilder.DeleteData(
                table:"Partner",
                keyColumn:"Id",
                keyValue:2
            );
            migrationBuilder.DeleteData(
                table:"Partner",
                keyColumn:"Id",
                keyValue:3
            );
            migrationBuilder.DeleteData(
                table:"Partner",
                keyColumn:"Id",
                keyValue:4
            );
        }
    }
}
