using API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionGroup> QuestionGroups { get; set; }
    public DbSet<UserResult> UserResults { get; set; }
    public DbSet<DetailResult> DetailResults { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId);
        
        builder.Entity<Exam>()
            .HasMany(e => e.QuestionGroups)
            .WithOne(qg => qg.Exam)
            .HasForeignKey(qg => qg.ExamId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<QuestionGroup>()
            .HasMany(qg => qg.Questions)
            .WithOne(q => q.Group)
            .HasForeignKey(q => q.GroupId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<DetailResult>()
            .HasOne(dr => dr.UserResult)
            .WithMany(ur => ur.DetailResults)
            .HasForeignKey(dr => dr.UserResultId);
        
        builder.Entity<Question>()
            .Property(q => q.GroupId)
            .IsRequired();

        builder.Entity<QuestionGroup>()
            .Property(qg => qg.ExamId)
            .IsRequired();
        
        builder.Entity<Question>()
            .HasIndex(q => q.GroupId);

        builder.Entity<QuestionGroup>()
            .HasIndex(qg => qg.ExamId);
    }
}