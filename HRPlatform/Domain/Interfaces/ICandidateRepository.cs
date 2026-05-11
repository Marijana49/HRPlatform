using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<IEnumerable<Candidate>> SearchCandidateAsync(string? name, List<string> skills);
        Task<Candidate?> GetCandidateByEmailAsync(string email);
        Task<Candidate?> GetCandidateByPhoneAsync(string phone);
    }
}
