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

        [HttpGet("candidates")]
        public async Task<IActionResult> GetAllCandidates()
        {
            var candidates = await _candidateService.GetAllCandidatesAsync();
            return Ok(candidates);
        }

        [HttpPost("addCandidate")]
        public async Task<IActionResult> AddNewCandidate(CandidateDTO dto)
        {
            try
            {
                await _candidateService.CreateCandidateAsync(dto);
                return Ok(new { message = "New candidate succesfully added!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("updateSkill")]
        public async Task<IActionResult> UpdateCandidateSkill(int candidateId, string skillName)
        {
            try
            {
                await _candidateService.UpdateCandidateSkillAsync(candidateId, skillName);
                return Ok(new { message = "Skill updated!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch("removeSkill")]
        public async Task<IActionResult> RemoveCandidateSkill(int candidateId, string skillName)
        {
            try
            {
                await _candidateService.RemoveCandidateSkillAsync(candidateId, skillName);
                return Ok(new { message = "Skill removed!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("deleteCandidate")]
        public async Task<IActionResult> DeleteCandidate([FromBody]CandidateForRemove candidate)
        {
            try
            {
                await _candidateService.RemoveCandidateAsync(candidate);
                return Ok(new { message = "Candidate removed!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("searchCandidate")]
        public async Task<IActionResult> SearchCandidate([FromQuery] string? candidateName, [FromQuery] List<string?> skillNames)
        {
            try
            {
                await _candidateService.SearchCandidateAsync(candidateName, skillNames);
                return Ok(new { message = "Succesufully search!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
