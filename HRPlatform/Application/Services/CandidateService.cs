using Application.DTOs;
using Application.Interfaces;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly ISkillRepository _skillRepository;

        public CandidateService(ICandidateRepository candidateRepository, ISkillRepository skillRepository)
        {
            _candidateRepository = candidateRepository;
            _skillRepository = skillRepository;
        }

        public async Task CreateCandidateAsync(CandidateDTO candidateDTO)
        {
            var candidateSkills = candidateDTO.Skills.Distinct().ToList();
            var selectedSkill = await _skillRepository.GetSkillsByName(candidateSkills);

            if(!selectedSkill.Any()) 
            {
                throw new Exception("Skills not found!");
            }

            var newCandidate = new Candidate
            {
                FullName = candidateDTO.FullName,
                BirthDate = candidateDTO.BirthDate,
                ContactNumber = candidateDTO.ContactNumber,
                Email = candidateDTO.Email,
                Skills = selectedSkill.Select(s => new CandidateSkill { SkillId = s.Id }).ToList(),
            };

            await _candidateRepository.AddAsync(newCandidate);
        }

        public async Task UpdateCandidateSkillAsync(int candidateId, string skillName)
        {
            var newSkill = await _skillRepository.GetSkillByName(skillName);

            if(newSkill == null)
            {
                throw new Exception($"Skill {newSkill} doesn't exist!");
            }

            var candidate = await _candidateRepository.GetByIdAsync(candidateId);
            bool alreadyHasSkill = candidate.Skills.Any(s => s.SkillId == newSkill.Id);

            if(!alreadyHasSkill)
            {
                candidate.Skills.Add(new CandidateSkill
                {
                    CandidateId = candidateId,
                    SkillId = newSkill.Id,
                });
            }

            await _candidateRepository.UpdateAsync(candidate);
        }
    }
}
