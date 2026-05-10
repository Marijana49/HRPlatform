using Domain.Models;

namespace Domain.Interfaces
{
    public interface ISkillRepository : IRepository<Skill>
    {
        Task<List<Skill>> GetSkillsByName(List<string> skillNames);
        Task<Skill?> GetSkillByName(string skillName);
    }
}