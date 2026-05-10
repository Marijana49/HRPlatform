using Domain.Models;

namespace Domain.Interfaces
{
    public interface ICandidateRepository : IRepository<Candidate>
    {
        Task<IEnumerable<Candidate>> SearchCandidateAsync(string name);
        Task<Candidate?> GetCandidateByEmailAsync(string email);
    }
}
