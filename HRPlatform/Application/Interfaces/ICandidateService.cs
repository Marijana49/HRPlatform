using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICandidateService
    {
        Task CreateCandidateAsync(CandidateDTO candidateDTO);
        Task UpdateCandidateSkillAsync(int candidateId, string skillName);
        Task RemoveCandidateSkillAsync(int candidateId, string skillName);
        Task RemoveCandidateAsync(CandidateForRemove candidate);
        Task<IEnumerable<CandidateDTO>> SearchCandidateAsync(string? name, List<string?> skillNames);
    }
}
