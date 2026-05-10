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
                throw new Exception("Skill already exists!");
            }

            var newSkill = new Skill
            {
                Name = skillDTO.Name,
            };

            await _skillRepository.AddAsync(newSkill);
        }
    }
}
