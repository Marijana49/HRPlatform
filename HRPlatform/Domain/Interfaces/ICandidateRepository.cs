using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task AddCandidateSkillAsync(int canditateId, int skillId);
        Task UpdateCandidateSkilss(int canditateId, string skillName);
        Task RemoveCandidateSkillAsync(int canditateId, int skillId);
        Task<IEnumerable<Candidate>> SearchCandidateAsync(string name, List<string> skills);
        Task<Candidate?> GetCandidateByEmailAsync(string email);
    }
}
