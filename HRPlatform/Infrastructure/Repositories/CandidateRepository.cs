using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace Infrastructure.Repositories
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _appDbContext;

        public CandidateRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Candidate entity)
        {
            await _appDbContext.Candidates.AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public Task AddCandidateSkillAsync(int canditateId, int skillId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(Candidate entity)
        {
            _appDbContext.Candidates.Remove(entity);
            await Task.CompletedTask;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _appDbContext.Candidates.ToListAsync();
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await _appDbContext.Candidates.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidate?> GetCandidateByEmailAsync(string email)
        {
            return await _appDbContext.Candidates.FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public Task RemoveCandidateSkillAsync(int canditateId, int skillId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candidate>> SearchCandidateAsync(string name, List<string> skills)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Candidate entity)
        {
            _appDbContext.Update(entity);
            await Task.CompletedTask;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task UpdateCandidateSkilss(int canditateId, string skillName)
        {
            var candidateSkill = await _appDbContext.Skills.FirstOrDefaultAsync(s => s.Name == skillName);
            if (candidateSkill != null)
            {
                throw new Exception($"Skill {skillName} doesn't exist!");
            }

            var existing = await _appDbContext.CandidatesSkill.FirstOrDefaultAsync(cs => cs.CandidateId == canditateId && cs.SkillId == candidateSkill.Id);
            if(existing == null)
            {
                var newSkill = new CandidateSkill
                {
                    CandidateId = canditateId,
                    SkillId = candidateSkill.Id
                };
                _appDbContext.CandidatesSkill.Add(newSkill);
                await _appDbContext.SaveChangesAsync();
            }
        }
    }
}
