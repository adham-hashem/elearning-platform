using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class CourseMaterial
{
    [Key]
    public Guid MaterialId { get; set; }

    public Guid CourseId { get; set; }

    public Guid ProfessorId { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [StringLength(255)]
    public string FilePath { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime UploadDate { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("CourseMaterials")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("ProfessorId")]
    [InverseProperty("CourseMaterials")]
    public virtual AspNetUser Professor { get; set; } = null!;
}
