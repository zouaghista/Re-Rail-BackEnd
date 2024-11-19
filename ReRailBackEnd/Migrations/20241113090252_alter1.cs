using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReRailBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class alter1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackPoint_Images_trackSnapShotId",
                table: "TrackPoint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackPoint",
                table: "TrackPoint");

            migrationBuilder.RenameTable(
                name: "TrackPoint",
                newName: "TrackPoints");

            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Images",
                newName: "CreationDate");

            migrationBuilder.RenameIndex(
                name: "IX_TrackPoint_trackSnapShotId",
                table: "TrackPoints",
                newName: "IX_TrackPoints_trackSnapShotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackPoints",
                table: "TrackPoints",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackPoints_Images_trackSnapShotId",
                table: "TrackPoints",
                column: "trackSnapShotId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrackPoints_Images_trackSnapShotId",
                table: "TrackPoints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackPoints",
                table: "TrackPoints");

            migrationBuilder.RenameTable(
                name: "TrackPoints",
                newName: "TrackPoint");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Images",
                newName: "DateTime");

            migrationBuilder.RenameIndex(
                name: "IX_TrackPoints_trackSnapShotId",
                table: "TrackPoint",
                newName: "IX_TrackPoint_trackSnapShotId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackPoint",
                table: "TrackPoint",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TrackPoint_Images_trackSnapShotId",
                table: "TrackPoint",
                column: "trackSnapShotId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
