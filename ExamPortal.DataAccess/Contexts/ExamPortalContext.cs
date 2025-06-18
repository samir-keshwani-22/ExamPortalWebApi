using ExamPortal.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExamPortal.DataAccess.Contexts;

public partial class ExamPortalContext : DbContext
{
    public ExamPortalContext()
    {
    }
    public ExamPortalContext(DbContextOptions<ExamPortalContext> options)
        : base(options)
    {
    }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<Answer> Answers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Name=ConnectionStrings:my_connection",
                x => x.MigrationsAssembly("ExamPortal.Migrations")
            );
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Exam>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.TotalMarks).HasDefaultValue(0);
        });

        modelBuilder.Entity<Question>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            }
        );

        modelBuilder.Entity<QuestionOption>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Answer>()
           .HasOne(a => a.SelectedOption)
           .WithMany()
           .HasForeignKey(a => a.SelectedOptionId)
           .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<QuestionOption>()
            .HasOne(qo => qo.Question)
            .WithMany(q => q.Options)
            .HasForeignKey(qo => qo.QuestionId);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
