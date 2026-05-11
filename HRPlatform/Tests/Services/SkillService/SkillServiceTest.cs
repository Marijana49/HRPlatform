using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Tests.Services.SkillService
{
    public class SkillServiceTest
    {
        private Mock<ISkillRepository> _skillRepo;
        private Application.Services.SkillService _skillService;

        [SetUp]
        public void SetUp()
        {
            _skillRepo = new Mock<ISkillRepository>();
            _skillService = new Application.Services.SkillService(_skillRepo.Object);
        }

        [Test]
        public async Task CreateSkill_Test()
        {
            var skillDto = new SkillDTO
            {
                Name = "Test"
            };

            _skillRepo.Setup(r => r.GetSkillByName(skillDto.Name)).ReturnsAsync((Skill)null);

            await _skillService.CreateSkillAsync(skillDto);
            _skillRepo.Verify(r => r.AddAsync(It.IsAny<Skill>()), Times.Once);
        }

        [Test]
        public async Task SkillExist_Test()
        {
            var skillDto = new SkillDTO
            {
                Name = "Test"
            };

            var existingSkill = new Skill { Name = "Test" };
            _skillRepo.Setup(r => r.GetSkillByName(skillDto.Name)).ReturnsAsync(existingSkill);

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _skillService.CreateSkillAsync(skillDto));
            Assert.That(exeption.Message, Is.EqualTo("Skill already exists!"));
        }

        [Test]
        public async Task GetAllSkills_Test()
        {
            var skills = new List<Skill>
            {
                new Skill { Name = "Skill1" },
                new Skill { Name = "Skill2"}
            };

            _skillRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(skills);
            await _skillService.GetAllAsync();
        }

        [Test]
        public async Task GetNoSkills_Test()
        {
            var skills = new List<Skill>{};

            _skillRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(skills);
            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _skillService.GetAllAsync());
            Assert.That(exeption.Message, Is.EqualTo("No skills!"));
        }
    }
}