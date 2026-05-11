using Application.DTOs;

namespace Application.Interfaces
{
    public interface ISkillService
    {
        Task CreateSkillAsync(SkillDTO skillDTO);
        Task <IEnumerable<SkillDTO>> GetAllAsync();
    }
}
