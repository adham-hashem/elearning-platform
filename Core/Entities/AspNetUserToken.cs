﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Models;

[PrimaryKey("UserId", "LoginProvider", "Name")]
public partial class AspNetUserToken
{
    [Key]
    public Guid UserId { get; set; }

    [Key]
    public string LoginProvider { get; set; } = null!;

    [Key]
    public string Name { get; set; } = null!;

    public string? Value { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AspNetUserTokens")]
    public virtual AspNetUser User { get; set; } = null!;
}
