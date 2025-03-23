using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class Quiz
{
    [Key]
    public Guid QuizId { get; set; }

    public Guid CourseId { get; set; }

    public Guid ProfessorId { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DueDate { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    public decimal TotalPoints { get; set; }

    [ForeignKey("CourseId")]
    [InverseProperty("Quizzes")]
    public virtual Course Course { get; set; } = null!;

    [ForeignKey("ProfessorId")]
    [InverseProperty("Quizzes")]
    public virtual AspNetUser Professor { get; set; } = null!;

    [InverseProperty("Quiz")]
    public virtual ICollection<StudentQuizSubmission> StudentQuizSubmissions { get; set; } = new List<StudentQuizSubmission>();
}
