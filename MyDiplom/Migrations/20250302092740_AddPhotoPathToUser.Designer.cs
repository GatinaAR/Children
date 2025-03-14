﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyDiplom.Models;

#nullable disable

namespace MyDiplom.Migrations
{
    [DbContext(typeof(ChildrenContext))]
    [Migration("20250302092740_AddPhotoPathToUser")]
    partial class AddPhotoPathToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyDiplom.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("attendance_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceId"));

                    b.Property<int?>("ChildId")
                        .HasColumnType("int")
                        .HasColumnName("child_id");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("status");

                    b.HasKey("AttendanceId")
                        .HasName("PK__Attendan__20D6A96866568E90");

                    b.HasIndex("ChildId");

                    b.ToTable("Attendance", (string)null);
                });

            modelBuilder.Entity("MyDiplom.Models.Child", b =>
                {
                    b.Property<int>("ChildId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("child_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChildId"));

                    b.Property<DateOnly?>("Birthdate")
                        .HasColumnType("date")
                        .HasColumnName("birthdate");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int")
                        .HasColumnName("parent_id");

                    b.HasKey("ChildId")
                        .HasName("PK__Children__015ADC054B6A4798");

                    b.HasIndex("GroupId");

                    b.HasIndex("ParentId");

                    b.ToTable("Children");
                });

            modelBuilder.Entity("MyDiplom.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("employee_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("last_name");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone");

                    b.Property<string>("Position")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("position");

                    b.Property<int?>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.HasKey("EmployeeId")
                        .HasName("PK__Employee__C52E0BA8954291E8");

                    b.HasIndex("UserId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("MyDiplom.Models.Group", b =>
                {
                    b.Property<int>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupId"));

                    b.Property<string>("GroupName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("group_name");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int")
                        .HasColumnName("teacher_id");

                    b.HasKey("GroupId")
                        .HasName("PK__Groups__D57795A0B1E93FC1");

                    b.HasIndex("TeacherId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("MyDiplom.Models.Message", b =>
                {
                    b.Property<int>("MessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("message_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MessageId"));

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<int?>("RecipientId")
                        .HasColumnType("int")
                        .HasColumnName("recipient_id");

                    b.Property<int?>("SenderId")
                        .HasColumnType("int")
                        .HasColumnName("sender_id");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("subject");

                    b.Property<string>("Timestamp")
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("timestamp");

                    b.HasKey("MessageId")
                        .HasName("PK__Messages__0BBF6EE6E42724BD");

                    b.HasIndex("RecipientId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("MyDiplom.Models.ObservationJournal", b =>
                {
                    b.Property<int>("JournalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("journal_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JournalId"));

                    b.Property<int?>("ChildId")
                        .HasColumnType("int")
                        .HasColumnName("child_id");

                    b.Property<DateOnly?>("Date")
                        .HasColumnType("date")
                        .HasColumnName("date");

                    b.Property<string>("Observation")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("observation");

                    b.Property<int?>("TeacherId")
                        .HasColumnType("int")
                        .HasColumnName("teacher_id");

                    b.HasKey("JournalId")
                        .HasName("PK__Observat__9894D298C99604E5");

                    b.HasIndex("ChildId");

                    b.HasIndex("TeacherId");

                    b.ToTable("ObservationJournal", (string)null);
                });

            modelBuilder.Entity("MyDiplom.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles__760965CCD9AB1AEE");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("MyDiplom.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("schedule_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScheduleId"));

                    b.Property<string>("Activity")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("activity");

                    b.Property<string>("Date")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("date");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int")
                        .HasColumnName("group_id");

                    b.HasKey("ScheduleId")
                        .HasName("PK__Schedule__C46A8A6F7BD34F61");

                    b.HasIndex("GroupId");

                    b.ToTable("Schedule", (string)null);
                });

            modelBuilder.Entity("MyDiplom.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("password_hash");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("phone");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<string>("Username")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("username");

                    b.HasKey("UserId")
                        .HasName("PK__Users__B9BE370F1DAB6CEA");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("MyDiplom.Models.Attendance", b =>
                {
                    b.HasOne("MyDiplom.Models.Child", "Child")
                        .WithMany("Attendances")
                        .HasForeignKey("ChildId")
                        .HasConstraintName("FK__Attendanc__child__45F365D3");

                    b.Navigation("Child");
                });

            modelBuilder.Entity("MyDiplom.Models.Child", b =>
                {
                    b.HasOne("MyDiplom.Models.Group", "Group")
                        .WithMany("Children")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__Children__group___403A8C7D");

                    b.HasOne("MyDiplom.Models.User", "Parent")
                        .WithMany("Children")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK__Children__parent__3F466844");

                    b.Navigation("Group");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("MyDiplom.Models.Employee", b =>
                {
                    b.HasOne("MyDiplom.Models.User", "User")
                        .WithMany("Employees")
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK__Employees__user___5070F446");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MyDiplom.Models.Group", b =>
                {
                    b.HasOne("MyDiplom.Models.User", "Teacher")
                        .WithMany("Groups")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK__Groups__teacher___3C69FB99");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyDiplom.Models.Message", b =>
                {
                    b.HasOne("MyDiplom.Models.User", "Recipient")
                        .WithMany("MessageRecipients")
                        .HasForeignKey("RecipientId")
                        .HasConstraintName("FK__Messages__recipi__49C3F6B7");

                    b.HasOne("MyDiplom.Models.User", "Sender")
                        .WithMany("MessageSenders")
                        .HasForeignKey("SenderId")
                        .HasConstraintName("FK__Messages__sender__48CFD27E");

                    b.Navigation("Recipient");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("MyDiplom.Models.ObservationJournal", b =>
                {
                    b.HasOne("MyDiplom.Models.Child", "Child")
                        .WithMany("ObservationJournals")
                        .HasForeignKey("ChildId")
                        .HasConstraintName("FK__Observati__child__4CA06362");

                    b.HasOne("MyDiplom.Models.User", "Teacher")
                        .WithMany("ObservationJournals")
                        .HasForeignKey("TeacherId")
                        .HasConstraintName("FK__Observati__teach__4D94879B");

                    b.Navigation("Child");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("MyDiplom.Models.Schedule", b =>
                {
                    b.HasOne("MyDiplom.Models.Group", "Group")
                        .WithMany("Schedules")
                        .HasForeignKey("GroupId")
                        .HasConstraintName("FK__Schedule__group___4316F928");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("MyDiplom.Models.User", b =>
                {
                    b.HasOne("MyDiplom.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__Users__role_id__398D8EEE");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MyDiplom.Models.Child", b =>
                {
                    b.Navigation("Attendances");

                    b.Navigation("ObservationJournals");
                });

            modelBuilder.Entity("MyDiplom.Models.Group", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Schedules");
                });

            modelBuilder.Entity("MyDiplom.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("MyDiplom.Models.User", b =>
                {
                    b.Navigation("Children");

                    b.Navigation("Employees");

                    b.Navigation("Groups");

                    b.Navigation("MessageRecipients");

                    b.Navigation("MessageSenders");

                    b.Navigation("ObservationJournals");
                });
#pragma warning restore 612, 618
        }
    }
}
