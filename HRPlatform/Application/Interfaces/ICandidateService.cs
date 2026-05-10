using Application.DTOs;

namespace Application.Interfaces
{
    public interface ICandidateService
    {
        Task CreateCandidateAsync(CandidateDTO candidateDTO);
        Task UpdateCandidateSkillAsync(int candidateId, string skillName);
    }
}
