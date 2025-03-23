using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class Level
{
    [Key]
    public Guid LevelId { get; set; }

    [StringLength(50)]
    public string LevelName { get; set; } = null!;

    [StringLength(200)]
    public string? Description { get; set; }

    [InverseProperty("Level")]
    public virtual ICollection<Term> Terms { get; set; } = new List<Term>();
}
