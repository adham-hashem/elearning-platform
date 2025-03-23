using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

public partial class Report
{
    [Key]
    public Guid ReportId { get; set; }

    public Guid AdminId { get; set; }

    [StringLength(50)]
    public string ReportType { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime GeneratedDate { get; set; }

    [StringLength(255)]
    public string? ReportFilePath { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("Reports")]
    public virtual AspNetUser Admin { get; set; } = null!;
}
