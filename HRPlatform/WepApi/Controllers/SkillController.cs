using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpPost("addSkill")]
        public async Task<IActionResult> CreateNewSkill(SkillDTO skillDTO)
        {
            try
            {
                await _skillService.CreateSkillAsync(skillDTO);
                return Ok(new { message = $"Skill {skillDTO.Name} created!" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("skills")]
        public async Task<IActionResult> GetAllCandidates()
        {
            var skills = await _skillService.GetAllAsync();
            return Ok(skills);
        }
    }
}
