using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

[Index("StudentId", Name = "IX_StudentQuizSubmissions_StudentId")]
public partial class StudentQuizSubmission
{
    [Key]
    public Guid SubmissionId { get; set; }

    public Guid QuizId { get; set; }

    public Guid StudentId { get; set; }

    [StringLength(255)]
    public string? SubmissionFilePath { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime SubmissionDate { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal? Grade { get; set; }

    [StringLength(500)]
    public string? Feedback { get; set; }

    [ForeignKey("QuizId")]
    [InverseProperty("StudentQuizSubmissions")]
    public virtual Quiz Quiz { get; set; } = null!;

    [ForeignKey("StudentId")]
    [InverseProperty("StudentQuizSubmissions")]
    public virtual AspNetUser Student { get; set; } = null!;
}
