using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class Term
{
    [Key]
    public Guid TermId { get; set; }

    public Guid LevelId { get; set; }

    [StringLength(50)]
    public string TermName { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    [InverseProperty("Term")]
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    [ForeignKey("LevelId")]
    [InverseProperty("Terms")]
    public virtual Level Level { get; set; } = null!;
}
