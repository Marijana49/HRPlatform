using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SkillRepository : ISkillRepository
    {
        private readonly AppDbContext _appDbContext;

        public SkillRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddAsync(Skill entity)
        {
            await _appDbContext.Skills.AddAsync(entity);
        }

        public async Task DeleteAsync(Skill entity)
        {
            _appDbContext.Skills.Remove(entity);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _appDbContext.Skills.ToListAsync();
        }

        public Task<Skill> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Skill entity)
        {
            _appDbContext.Skills.Update(entity);
            await Task.CompletedTask;
        }
    }
}
