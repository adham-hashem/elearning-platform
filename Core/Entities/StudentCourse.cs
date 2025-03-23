using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

[PrimaryKey("StudentId", "CourseId")]
[Index("StudentId", Name = "IX_StudentCourses_StudentId")]
public partial class StudentCourse
{
    [Key]
    public Guid StudentId { get; set; }

    [Key]
    public Guid CourseId { get; set; }

    public Guid SectionId { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Degree { get; set; }

    [Column(TypeName = "decimal(4, 2)")]
    public decimal? GradePoints { get; set; }

    public bool PassStatus { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("StudentCourses")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("SectionId")]
    [InverseProperty("StudentCourses")]
    public virtual CourseSection Section { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentCourses")]
    public virtual AspNetUser Student { get; set; } = null!;
}
