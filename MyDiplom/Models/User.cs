using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyDiplom.Models;

public partial class User
{
    public int UserId { get; set; }

    //public string? Username { get; set; }

    //public string? PasswordHash { get; set; }

    //public int RoleId { get; set; }

    //public string? Email { get; set; }

    //public string? Phone { get; set; }
    [Required(ErrorMessage = "Имя пользователя обязательно.")]
    [StringLength(50, ErrorMessage = "Имя пользователя не должно превышать 50 символов.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "Пароль обязателен.")]
    [StringLength(100, ErrorMessage = "Пароль должен содержать от 6 до 100 символов.", MinimumLength = 6)]
    public string? PasswordHash { get; set; }

    public int RoleId { get; set; }

    [Required(ErrorMessage = "Электронная почта обязательна.")]
    [EmailAddress(ErrorMessage = "Некорректный формат электронной почты.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Телефон обязателен.")]
    [RegularExpression(@"^\+?[0-9]{10,15}$", ErrorMessage = "Некорректный номер телефона.")]
    public string? Phone { get; set; }

    public byte[] Photo { get; set; } // если храните само изображение


    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Message> MessageRecipients { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();

    public virtual ICollection<ObservationJournal> ObservationJournals { get; set; } = new List<ObservationJournal>();

    public virtual Role Role { get; set; } = null!;
}
