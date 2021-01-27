using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class EntityRefactorWithBaseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "WeatherCondition",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "WeatherCondition",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateModified",
                table: "WeatherCondition",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "WeatherCondition",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "WeatherCondition",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TrailPhoto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "TrailPhoto",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateModified",
                table: "TrailPhoto",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TrailPhoto",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TrailPhoto",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TrailheadLocation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "TrailheadLocation",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateModified",
                table: "TrailheadLocation",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "TrailheadLocation",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "TrailheadLocation",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "InternationalAddress",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "InternationalAddress",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateModified",
                table: "InternationalAddress",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "InternationalAddress",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "InternationalAddress",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "EventPhotoGps",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateCreated",
                table: "EventPhotoGps",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateModified",
                table: "EventPhotoGps",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "EventPhotoGps",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "EventPhotoGps",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "WeatherCondition");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "WeatherCondition");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "WeatherCondition");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "WeatherCondition");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "WeatherCondition");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TrailPhoto");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "TrailPhoto");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "TrailPhoto");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TrailPhoto");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TrailPhoto");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TrailheadLocation");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "TrailheadLocation");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "TrailheadLocation");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "TrailheadLocation");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "TrailheadLocation");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "InternationalAddress");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "InternationalAddress");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "InternationalAddress");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InternationalAddress");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "InternationalAddress");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EventPhotoGps");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "EventPhotoGps");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "EventPhotoGps");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "EventPhotoGps");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EventPhotoGps");
        }
    }
}
