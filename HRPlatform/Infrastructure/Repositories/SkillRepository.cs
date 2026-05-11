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
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Skill entity)
        {
            _appDbContext.Skills.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _appDbContext.Skills.ToListAsync();
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _appDbContext.Skills.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Skill?> GetSkillByName(string skillName)
        {
            return await _appDbContext.Skills.Where(s => skillName.ToLower() == s.Name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<List<Skill>> GetSkillsByName(List<string> skillNames)
        {
            var lowerNames = skillNames.Select(n => n.ToLower()).ToList();
            return await _appDbContext.Skills.Where(s => lowerNames.Contains(s.Name.ToLower())).ToListAsync();
        }

        public async Task UpdateAsync(Skill entity)
        {
            _appDbContext.Skills.Update(entity);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
