using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteAsync(Candidate entity)
        {
            _appDbContext.Candidates.Remove(entity);
            await Task.CompletedTask;
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _appDbContext.Candidates.Include(c => c.Skills).ThenInclude(cs => cs.Skill).ToListAsync();
        }

        public async Task<Candidate> GetByIdAsync(int id)
        {
            return await _appDbContext.Candidates.Include(c => c.Skills).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidate?> GetCandidateByEmailAsync(string email)
        {
            return await _appDbContext.Candidates.FirstOrDefaultAsync(c => c.Email.Equals(email));
        }

        public async Task<Candidate?> GetCandidateByPhoneAsync(string phone)
        {
            return await _appDbContext.Candidates.FirstOrDefaultAsync(c => c.Email.Equals(phone));
        }

        public async Task<IEnumerable<Candidate>> SearchCandidateAsync(string? name, List<string> skills)
        {
            var query = _appDbContext.Candidates.Include(c => c.Skills).ThenInclude(cs => cs.Skill).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.FullName.ToLower().Contains(name.ToLower()));
            }

            if(skills != null && skills.Any())
            {
                var okSkills = skills.Where(s => !string.IsNullOrEmpty(s)).Select(s => s!.ToLower()).ToList();

                if (okSkills.Any())
                {
                    query = query.Where(c => c.Skills.Any(cs => okSkills.Contains(cs.Skill.Name.ToLower())));
                }
            }

            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Candidate entity)
        {
            _appDbContext.Update(entity);
            await Task.CompletedTask;
            await _appDbContext.SaveChangesAsync();
        }
    }
}
