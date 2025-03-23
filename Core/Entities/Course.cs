using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

[Index("CourseCode", Name = "UQ__Courses__FC00E00052D14E75", IsUnique = true)]
public partial class Course
{
    [Key]
    public Guid CourseId { get; set; }

    [StringLength(20)]
    public string CourseCode { get; set; } = null!;

    [StringLength(100)]
    public string CourseName { get; set; } = null!;

    public int CreditHours { get; set; }

    [StringLength(20)]
    public string DayOfWeek { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    [StringLength(100)]
    public string Location { get; set; } = null!;

    public Guid TermId { get; set; }

    [InverseProperty("Course")]
    public virtual ICollection<CourseMaterial> CourseMaterials { get; set; } = new List<CourseMaterial>();

    [InverseProperty("Course")]
    public virtual ICollection<CourseSection> CourseSections { get; set; } = new List<CourseSection>();

    [InverseProperty("Course")]
    public virtual ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();

    [InverseProperty("Course")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    [ForeignKey("TermId")]
    [InverseProperty("Courses")]
    public virtual Term Term { get; set; } = null!;

    [ForeignKey("CourseId")]
    [InverseProperty("Courses")]
    public virtual ICollection<AspNetUser> Professors { get; set; } = new List<AspNetUser>();
}
