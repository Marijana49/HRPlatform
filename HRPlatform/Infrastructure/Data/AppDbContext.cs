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

            //seed data for Skill
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "C#"},    
                new Skill { Id = 2, Name = "React"},    
                new Skill { Id = 3, Name = "Java"},    
                new Skill { Id = 4, Name = "Python"},    
                new Skill { Id = 5, Name = "English"},    
                new Skill { Id = 6, Name = "German"},    
                new Skill { Id = 7, Name = "Spanish"}    
            );

            //seed data for Candidate
            modelBuilder.Entity<Candidate>().HasData(
                new Candidate { Id = 1, FullName = "Marko Markovic", BirthDate = new DateOnly(1999, 12, 12), ContactNumber = "065498752", Email = "marokom@gmail.com"},
                new Candidate { Id = 2, FullName = "Jovan Jovanovic", BirthDate = new DateOnly(2001, 6, 4), ContactNumber = "068023654", Email = "jovan@gmail.com"},
                new Candidate { Id = 3, FullName = "Ana Smith", BirthDate = new DateOnly(2000, 8, 7), ContactNumber = "3265410", Email = "smithana@gmail.com"},
                new Candidate { Id = 4, FullName = "Kate Brown", BirthDate = new DateOnly(1994, 6, 5), ContactNumber = "852741036", Email = "kate@gmail.com"},
                new Candidate { Id = 5, FullName = "Marija Matic", BirthDate = new DateOnly(1998, 1, 1), ContactNumber = "06452310", Email = "mmarija@gmail.com"},
                new Candidate { Id = 6, FullName = "Paul Blue", BirthDate = new DateOnly(2000, 10, 9), ContactNumber = "9872015", Email = "paul123@gmail.com"}
            );

            //seed data for CandidateSkill
            modelBuilder.Entity<CandidateSkill>().HasData(
                new CandidateSkill { CandidateId = 1, SkillId = 1 },
                new CandidateSkill { CandidateId = 1, SkillId = 2 },
                new CandidateSkill { CandidateId = 2, SkillId = 2 },
                new CandidateSkill { CandidateId = 5, SkillId = 7 },
                new CandidateSkill { CandidateId = 4, SkillId = 6 }
            );
        }
    }
}
