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

            if (!selectedSkill.Any())
            {
                throw new KeyNotFoundException("Skills not found!");
            }

            var existingMail = await _candidateRepository.GetCandidateByEmailAsync(candidateDTO.Email);
            if (existingMail != null)
            {
                throw new InvalidOperationException("Email already exists!");
            }

            var existingContactNumber = await _candidateRepository.GetCandidateByPhoneAsync(candidateDTO.ContactNumber);
            if(existingContactNumber != null)
            {
                throw new InvalidOperationException("Contact number already exists!");
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

        public async Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync()
        {
            var candidates = await _candidateRepository.GetAllAsync();

            if (!candidates.Any())
            {
                throw new KeyNotFoundException("No candidates!");
            }

            return candidates.Select(c => new CandidateDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                BirthDate = c.BirthDate,
                ContactNumber = c.ContactNumber,
                Email = c.Email,
                Skills = c.Skills.Select(s => s.Skill.Name).ToList()
            }).ToList();
        }

        public async Task RemoveCandidateAsync(CandidateForRemove candidate)
        {
            var candidateForRemove = await _candidateRepository.GetByIdAsync(candidate.Id);

            if (candidateForRemove == null)
            {
                throw new KeyNotFoundException("Candidate doesn't exist!");
            }

            await _candidateRepository.DeleteAsync(candidateForRemove);
        }

        public async Task RemoveCandidateSkillAsync(int candidateId, string skillName)
        {
            var skillForRemoving = await _skillRepository.GetSkillByName(skillName);

            if (skillForRemoving == null)
            {
                throw new KeyNotFoundException($"Skill {skillName} doesn't exist!");
            }

            var candidate = await _candidateRepository.GetByIdAsync(candidateId);

            if (candidate == null)
            {
                throw new KeyNotFoundException("Candidate not found!");
            }

            var hasSkill = candidate.Skills.FirstOrDefault(s => s.SkillId == skillForRemoving.Id);

            if (hasSkill != null)
            {
                candidate.Skills.Remove(hasSkill);
                await _candidateRepository.UpdateAsync(candidate);
            }
        }

        public async Task<IEnumerable<CandidateDTO>> SearchCandidateAsync(string? name, List<string?> skillNames)
        {
            var candidates = await _candidateRepository.SearchCandidateAsync(name, skillNames);

            var searchedCandidate = candidates.Select(c => new CandidateDTO
            {
                Id = c.Id,
                FullName = c.FullName,
                BirthDate = c.BirthDate,
                ContactNumber = c.ContactNumber,
                Email = c.Email,
                Skills = c.Skills.Select(s => s.Skill.Name).ToList()
            }).ToList();

            if (!searchedCandidate.Any())
            {
                throw new KeyNotFoundException("Candidate not found!");
            }

            return searchedCandidate;
        }

        public async Task UpdateCandidateSkillAsync(int candidateId, string skillName)
        {
            var newSkill = await _skillRepository.GetSkillByName(skillName);

            if (newSkill == null)
            {
                throw new KeyNotFoundException($"Skill {skillName} doesn't exist!");
            }

            var candidate = await _candidateRepository.GetByIdAsync(candidateId);

            if (candidate == null)
            {
                throw new KeyNotFoundException("Candidate not found!");
            }

            if (candidate.Skills == null)
            {
                candidate.Skills = new List<CandidateSkill>();
            }

            bool alreadyHasSkill = candidate.Skills.Any(s => s.SkillId == newSkill.Id);

            if (!alreadyHasSkill)
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
