using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

[Index("CourseId", Name = "IX_CourseSections_CourseId")]
public partial class CourseSection
{
    [Key]
    public Guid SectionId { get; set; }

    public Guid CourseId { get; set; }

    public Guid DemonstratorId { get; set; }

    [StringLength(10)]
    public string SectionNumber { get; set; } = null!;

    [ForeignKey("CourseId")]
    [InverseProperty("CourseSections")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("DemonstratorId")]
    [InverseProperty("CourseSections")]
    public virtual AspNetUser Demonstrator { get; set; } = null!;

    [InverseProperty("Section")]
    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
