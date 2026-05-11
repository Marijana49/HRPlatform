using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task CreateSkillAsync(SkillDTO skillDTO)
        {
            var existingSkill = await _skillRepository.GetSkillByName(skillDTO.Name);

            if (existingSkill != null)
            {
                throw new KeyNotFoundException("Skill already exists!");
            }

            var newSkill = new Skill
            {
                Name = skillDTO.Name,
            };

            await _skillRepository.AddAsync(newSkill);
        }

        public async Task<IEnumerable<SkillDTO>> GetAllAsync()
        {
            var skills = await _skillRepository.GetAllAsync();

            if(!skills.Any())
            {
                throw new KeyNotFoundException("No skills!");
            }

            return skills.Select(c => new SkillDTO
            {
                Name = c.Name,
            }).ToList();
        }
    }
}
