using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task AddCandidateSkillAsync(int canditateId, int skillId);
        Task UpdateCandidateSkilss(Candidate canditate, string skillName);
        Task RemoveCandidateSkillAsync(Candidate canditateId, int skillId);
        Task<IEnumerable<Candidate>> SearchCandidateAsync(string name, List<string> skills);
    }
}
