using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace MyDiplom.Models;

public partial class ChildrenContext : DbContext
{
    private static ChildrenContext _context;
    public ChildrenContext()
    {
    }

    public ChildrenContext(DbContextOptions<ChildrenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<ObservationJournal> ObservationJournals { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=LAPTOP-SPORL359\\MSSQLSERVER1;initial catalog=Children;integrated security=True;encrypt=False;");

    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasKey(e => e.AttendanceId).HasName("PK__Attendan__20D6A96866568E90");

            entity.ToTable("Attendance");

            entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");
            entity.Property(e => e.ChildId).HasColumnName("child_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");

            entity.HasOne(d => d.Child).WithMany(p => p.Attendances)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__Attendanc__child__45F365D3");
        });


        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.ChildId).HasName("PK__Children__015ADC054B6A4798");

            entity.Property(e => e.ChildId).HasColumnName("child_id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");

            entity.HasOne(d => d.Group).WithMany(p => p.Children)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Children__group___403A8C7D");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Children__parent__3F466844");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA8954291E8");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .HasColumnName("position");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Employees)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Employees__user___5070F446");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GroupId).HasName("PK__Groups__D57795A0B1E93FC1");

            entity.Property(e => e.GroupId).HasColumnName("group_id");
            entity.Property(e => e.GroupName)
                .HasMaxLength(50)
                .HasColumnName("group_name");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Groups)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Groups__teacher___3C69FB99");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__0BBF6EE6E42724BD");

            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.RecipientId).HasColumnName("recipient_id");
            entity.Property(e => e.SenderId).HasColumnName("sender_id");
            entity.Property(e => e.Subject).HasColumnName("subject");
            entity.Property(e => e.Timestamp)
                .IsUnicode(false)
                .HasColumnName("timestamp");

            entity.HasOne(d => d.Recipient).WithMany(p => p.MessageRecipients)
                .HasForeignKey(d => d.RecipientId)
                .HasConstraintName("FK__Messages__recipi__49C3F6B7");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Messages__sender__48CFD27E");
        });

        modelBuilder.Entity<ObservationJournal>(entity =>
        {
            entity.HasKey(e => e.JournalId).HasName("PK__Observat__9894D298C99604E5");

            entity.ToTable("ObservationJournal");

            entity.Property(e => e.JournalId).HasColumnName("journal_id");
            entity.Property(e => e.ChildId).HasColumnName("child_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Observation).HasColumnName("observation");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");

            entity.HasOne(d => d.Child).WithMany(p => p.ObservationJournals)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__Observati__child__4CA06362");

            entity.HasOne(d => d.Teacher).WithMany(p => p.ObservationJournals)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__Observati__teach__4D94879B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__760965CCD9AB1AEE");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.ScheduleId).HasName("PK__Schedule__C46A8A6F7BD34F61");

            entity.ToTable("Schedule");

            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.Activity)
                .HasMaxLength(255)
                .HasColumnName("activity");
            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .HasColumnName("date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.GroupId).HasColumnName("group_id");

            entity.HasOne(d => d.Group).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__Schedule__group___4316F928");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__B9BE370F1DAB6CEA");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");



            entity.Property(e => e.Photo).HasColumnName("Photo")
                .IsRequired(false); // Укажите, если это поле необязательное




            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__role_id__398D8EEE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    public static ChildrenContext GetContext()
    {
        if (_context == null)

            _context = new ChildrenContext();

        return _context;
    }


    public User GetUser(string username, string password)
    {
        if (!string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password))
        {
            return Users.FirstOrDefault(i => i.Username == username && i.PasswordHash == password);
        }
        return null;


    }

    public User AddUser(User user)
    {
        var context = GetContext();

        try
        {
            // Проверка на существование пользователя с таким именем
            if (context.Users.Any(u => u.Username == user.Username))
            {
                throw new ArgumentException("Username already exists.");
            }

            // Добавление пользователя в контекст
            context.Users.Add(user);

            // Сохранение изменений в базе данных
            context.SaveChanges();

            // Возврат добавленного пользователя
            return user;
        }
        catch (Exception ex)
        {
            // Обработка ошибок (например, логирование)
            Console.Error.WriteLine($"Error adding user: {ex.Message}");
            throw; // Переброс исключения для обработки на более высоком уровне
        }
    }
    public List<Role> GetAllRoles()
    {
        var context = GetContext();
        return context.Roles.ToList();
    }

    public List<Role> GetRoles()
    {
        var  db = GetContext();
        return db.Roles.ToList(); // Получаем все роли из таблицы Roles
    }

    public Role GetRoleByName(string roleName)
    {
        var db = GetContext();
        return db.Roles.FirstOrDefault(r => r.RoleName == roleName); // Ищем роль по имени
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

