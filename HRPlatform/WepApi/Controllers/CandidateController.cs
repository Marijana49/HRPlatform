using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidateController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCandidate(CandidateDTO dto)
        {
            await _candidateService.CreateCandidateAsync(dto);
            return Ok(new { message = "New candidate succesfully added!" });
        }

        [HttpPatch("update")]
        public async Task<IActionResult> UpdateCandidateSkill(int candidateId, string skillName)
        {
            await _candidateService.UpdateCandidateSkillAsync(candidateId, skillName);
            return Ok(new { message = "Skill updated!" });
        }

        [HttpPatch("remove")]
        public async Task<IActionResult> RemoveCandidateSkill(int candidateId, string skillName)
        {
            await _candidateService.RemoveCandidateSkillAsync(candidateId, skillName);
            return Ok(new { message = "Skill removed!" });
        }
    }
}
