using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<CandidateSkill> CandidatesSkill { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.FullName).IsRequired();
                entity.Property(c => c.BirthDate).IsRequired();
                entity.Property(c => c.ContactNumber).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.HasIndex(c => c.Email).IsUnique();

                entity.HasMany(c => c.Skills).WithOne(cs => cs.Candidate).HasForeignKey(cs => cs.CandidateId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasKey(s => s.Id);
                entity.Property(s => s.Name).IsRequired();
                entity.HasIndex(s => s.Name).IsUnique();

                entity.HasMany(s => s.CandidateSkills).WithOne(cs => cs.Skill).HasForeignKey(cs => cs.SkillId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CandidateSkill>(entity =>
            {
                entity.HasKey(cs => new { cs.CandidateId, cs.SkillId });
                entity.HasOne(cs => cs.Candidate).WithMany(c => c.Skills).HasForeignKey(cs => cs.CandidateId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(cs => cs.Skill).WithMany(c => c.CandidateSkills).HasForeignKey(cs => cs.SkillId).OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
