using System;
using System.Collections.Generic;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DB;

public partial class ElearningDbContext : DbContext
{
    public ElearningDbContext()
    {
    }

    public ElearningDbContext(DbContextOptions<ElearningDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseMaterial> CourseMaterials { get; set; }

    public virtual DbSet<CourseSection> CourseSections { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Quiz> Quizzes { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<StudentQuizSubmission> StudentQuizSubmissions { get; set; }

    public virtual DbSet<Term> Terms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=ElearningPlatform;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasMany(d => d.Courses).WithMany(p => p.Professors)
                .UsingEntity<Dictionary<string, object>>(
                    "ProfessorCourse",
                    r => r.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Professor__Cours__02084FDA"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__Professor__Profe__01142BA1"),
                    j =>
                    {
                        j.HasKey("ProfessorId", "CourseId").HasName("PK__Professo__FC918E534367786E");
                        j.ToTable("ProfessorCourses");
                        j.HasIndex(new[] { "ProfessorId" }, "IX_ProfessorCourses_ProfessorId");
                    });

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71A77BFE4A8E");

            entity.Property(e => e.CourseId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Term).WithMany(p => p.Courses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Courses__TermId__797309D9");
        });

        modelBuilder.Entity<CourseMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__CourseMa__C50610F7081236FA");

            entity.Property(e => e.MaterialId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.UploadDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseMaterials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseMat__Cours__0C85DE4D");

            entity.HasOne(d => d.Professor).WithMany(p => p.CourseMaterials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseMat__Profe__0D7A0286");
        });

        modelBuilder.Entity<CourseSection>(entity =>
        {
            entity.HasKey(e => e.SectionId).HasName("PK__CourseSe__80EF087250D7D786");

            entity.Property(e => e.SectionId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseSections)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseSec__Cours__7D439ABD");

            entity.HasOne(d => d.Demonstrator).WithMany(p => p.CourseSections)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CourseSec__Demon__7E37BEF6");
        });

        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.LevelId).HasName("PK__Levels__09F03C26A37B3ABC");

            entity.Property(e => e.LevelId).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.QuizId).HasName("PK__Quizzes__8B42AE8EE2C3570B");

            entity.Property(e => e.QuizId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Course).WithMany(p => p.Quizzes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quizzes__CourseI__114A936A");

            entity.HasOne(d => d.Professor).WithMany(p => p.Quizzes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Quizzes__Profess__123EB7A3");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Reports__D5BD48058BF4F1E6");

            entity.Property(e => e.ReportId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.GeneratedDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Admin).WithMany(p => p.Reports)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reports__AdminId__1CBC4616");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("PK__StudentC__5E57FC831903C394");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Cours__06CD04F7");

            entity.HasOne(d => d.Section).WithMany(p => p.StudentCourses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Secti__07C12930");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentCo__Stude__05D8E0BE");
        });

        modelBuilder.Entity<StudentQuizSubmission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("PK__StudentQ__449EE125C6D706FE");

            entity.Property(e => e.SubmissionId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SubmissionDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Quiz).WithMany(p => p.StudentQuizSubmissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__QuizI__17036CC0");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentQuizSubmissions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__StudentQu__Stude__17F790F9");
        });

        modelBuilder.Entity<Term>(entity =>
        {
            entity.HasKey(e => e.TermId).HasName("PK__Terms__410A21A57568D433");

            entity.Property(e => e.TermId).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Level).WithMany(p => p.Terms)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Terms__LevelId__73BA3083");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
