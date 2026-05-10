using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public async Task<IEnumerable<Candidate>> SearchCandidateAsync(string name)
        {
            return await _appDbContext.Candidates.Where(c => c.FullName.Contains(name)).ToListAsync();
        }

        public async Task UpdateAsync(Candidate entity)
        {
            _appDbContext.Update(entity);
            await Task.CompletedTask;
            await _appDbContext.SaveChangesAsync();
        }    
    }
}
