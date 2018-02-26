using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CollegeGrades.Migrations
{
    public partial class Mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cycles",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Semester = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cycles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AttendedSubjects",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    AccountID = table.Column<string>(nullable: true),
                    CycleID = table.Column<string>(nullable: true),
                    SubjectID = table.Column<string>(nullable: true),
                    TeacherID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendedSubjects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AttendedSubjects_Accounts_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Accounts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendedSubjects_Cycles_CycleID",
                        column: x => x.CycleID,
                        principalTable: "Cycles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendedSubjects_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendedSubjects_Teachers_TeacherID",
                        column: x => x.TeacherID,
                        principalTable: "Teachers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    AttendedSubjectID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Scores_AttendedSubjects_AttendedSubjectID",
                        column: x => x.AttendedSubjectID,
                        principalTable: "AttendedSubjects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttendedSubjects_AccountID",
                table: "AttendedSubjects",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_AttendedSubjects_CycleID",
                table: "AttendedSubjects",
                column: "CycleID");

            migrationBuilder.CreateIndex(
                name: "IX_AttendedSubjects_SubjectID",
                table: "AttendedSubjects",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_AttendedSubjects_TeacherID",
                table: "AttendedSubjects",
                column: "TeacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_AttendedSubjectID",
                table: "Scores",
                column: "AttendedSubjectID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "AttendedSubjects");

            migrationBuilder.DropTable(
                name: "Cycles");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");
        }
    }
}
