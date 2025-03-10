using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDiplom.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoPathToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__760965CCD9AB1AEE", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    password_hash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__B9BE370F1DAB6CEA", x => x.user_id);
                    table.ForeignKey(
                        name: "FK__Users__role_id__398D8EEE",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employee_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    position = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Employee__C52E0BA8954291E8", x => x.employee_id);
                    table.ForeignKey(
                        name: "FK__Employees__user___5070F446",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    group_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    teacher_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Groups__D57795A0B1E93FC1", x => x.group_id);
                    table.ForeignKey(
                        name: "FK__Groups__teacher___3C69FB99",
                        column: x => x.teacher_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    message_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_id = table.Column<int>(type: "int", nullable: true),
                    recipient_id = table.Column<int>(type: "int", nullable: true),
                    subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    timestamp = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Messages__0BBF6EE6E42724BD", x => x.message_id);
                    table.ForeignKey(
                        name: "FK__Messages__recipi__49C3F6B7",
                        column: x => x.recipient_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                    table.ForeignKey(
                        name: "FK__Messages__sender__48CFD27E",
                        column: x => x.sender_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Children",
                columns: table => new
                {
                    child_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    group_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Children__015ADC054B6A4798", x => x.child_id);
                    table.ForeignKey(
                        name: "FK__Children__group___403A8C7D",
                        column: x => x.group_id,
                        principalTable: "Groups",
                        principalColumn: "group_id");
                    table.ForeignKey(
                        name: "FK__Children__parent__3F466844",
                        column: x => x.parent_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    schedule_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    group_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    activity = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Schedule__C46A8A6F7BD34F61", x => x.schedule_id);
                    table.ForeignKey(
                        name: "FK__Schedule__group___4316F928",
                        column: x => x.group_id,
                        principalTable: "Groups",
                        principalColumn: "group_id");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    attendance_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    child_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Attendan__20D6A96866568E90", x => x.attendance_id);
                    table.ForeignKey(
                        name: "FK__Attendanc__child__45F365D3",
                        column: x => x.child_id,
                        principalTable: "Children",
                        principalColumn: "child_id");
                });

            migrationBuilder.CreateTable(
                name: "ObservationJournal",
                columns: table => new
                {
                    journal_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    child_id = table.Column<int>(type: "int", nullable: true),
                    date = table.Column<DateOnly>(type: "date", nullable: true),
                    observation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    teacher_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Observat__9894D298C99604E5", x => x.journal_id);
                    table.ForeignKey(
                        name: "FK__Observati__child__4CA06362",
                        column: x => x.child_id,
                        principalTable: "Children",
                        principalColumn: "child_id");
                    table.ForeignKey(
                        name: "FK__Observati__teach__4D94879B",
                        column: x => x.teacher_id,
                        principalTable: "Users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_child_id",
                table: "Attendance",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "IX_Children_group_id",
                table: "Children",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Children_parent_id",
                table: "Children",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_user_id",
                table: "Employees",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_teacher_id",
                table: "Groups",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_recipient_id",
                table: "Messages",
                column: "recipient_id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_sender_id",
                table: "Messages",
                column: "sender_id");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationJournal_child_id",
                table: "ObservationJournal",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "IX_ObservationJournal_teacher_id",
                table: "ObservationJournal",
                column: "teacher_id");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_group_id",
                table: "Schedule",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_role_id",
                table: "Users",
                column: "role_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "ObservationJournal");

            migrationBuilder.DropTable(
                name: "Schedule");

            migrationBuilder.DropTable(
                name: "Children");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
