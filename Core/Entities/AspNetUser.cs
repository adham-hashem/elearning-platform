using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

[Index("NormalizedEmail", Name = "EmailIndex")]
[Index("NationalId", Name = "IX_AspNetUsers_NationalId")]
public partial class AspNetUser
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(14)]
    public string NationalId { get; set; } = null!;

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    public DateOnly? BirthDate { get; set; }

    [StringLength(15)]
    public string PhoneNumber { get; set; } = null!;

    [StringLength(200)]
    public string Address { get; set; } = null!;

    [StringLength(256)]
    public string? UserName { get; set; }

    [StringLength(256)]
    public string? NormalizedUserName { get; set; }

    [StringLength(256)]
    public string? Email { get; set; }

    [StringLength(256)]
    public string? NormalizedEmail { get; set; }

    public bool EmailConfirmed { get; set; }

    public string? PasswordHash { get; set; }

    public string? SecurityStamp { get; set; }

    public string? ConcurrencyStamp { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public DateTimeOffset? LockoutEnd { get; set; }

    public bool LockoutEnabled { get; set; }

    public int AccessFailedCount { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; } = new List<AspNetUserClaim>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; } = new List<AspNetUserLogin>();

    [InverseProperty("User")]
    public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; } = new List<AspNetUserToken>();

    [InverseProperty("Professor")]
    public virtual ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();

    [InverseProperty("Demonstrator")]
    public virtual ICollection<CourseSection> CourseSections { get; set; } = new List<CourseSection>();

    [InverseProperty("Professor")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    [InverseProperty("Admin")]
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    [InverseProperty("Student")]
    public virtual ICollection<StudentQuizSubmission> StudentQuizSubmissions { get; set; } = new List<StudentQuizSubmission>();

    [ForeignKey("ProfessorId")]
    [InverseProperty("Professors")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<AspNetRole> Roles { get; set; } = new List<AspNetRole>();
}
