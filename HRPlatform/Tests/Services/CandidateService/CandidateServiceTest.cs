using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Moq;

namespace Tests.Services.CandidateService
{
    public class CandidateServiceTest
    {
        private Mock<ICandidateRepository> _candidateRepo;
        private Mock<ISkillRepository> _skillRepo;
        private Application.Services.CandidateService _candidateService;

        [SetUp]
        public void SetUp()
        {
            _candidateRepo = new Mock<ICandidateRepository>();
            _skillRepo = new Mock<ISkillRepository>();
            _candidateService = new Application.Services.CandidateService(_candidateRepo.Object, _skillRepo.Object);
        }

        [Test]
        public async Task CreateCandidate_Test()
        {
            var birthDate = new DateOnly(1999, 12, 12);

            var candidateDTO = new CandidateDTO
            {
                FullName = "Test",
                BirthDate = birthDate,
                ContactNumber = "123540",
                Email = "testMail@gmail.com",
                Skills = new List<string> { "C#", "English" }
            };

            var mockSkills = new List<Skill> { new Skill { Id = 1, Name = "C#" }, new Skill { Id = 2, Name = "English" } };
            _skillRepo.Setup(r => r.GetSkillsByName(It.IsAny<List<string>>())).ReturnsAsync(mockSkills);

            _candidateRepo.Setup(r => r.GetCandidateByEmailAsync(candidateDTO.Email)).ReturnsAsync((Candidate)null);

            await _candidateService.CreateCandidateAsync(candidateDTO);

            _candidateRepo.Verify(r => r.AddAsync(It.IsAny<Candidate>()), Times.Once);
        }

        [Test]
        public async Task EmailExist_Test()
        {
            var birthDate = new DateOnly(1999, 12, 12);

            var candidateDTO = new CandidateDTO
            {
                FullName = "Test",
                BirthDate = birthDate,
                ContactNumber = "123540",
                Email = "testMail@gmail.com",
                Skills = new List<string> { "C#", "English" }
            };

            var exsistingCandidate = new Candidate { Email = candidateDTO.Email };
            var mockSkills = new List<Skill> { new Skill { Id = 1, Name = "C#" }, new Skill { Id = 2, Name = "English" } };

            _skillRepo.Setup(r => r.GetSkillsByName(It.IsAny<List<string>>())).ReturnsAsync(mockSkills);
            _candidateRepo.Setup(r => r.GetCandidateByEmailAsync(candidateDTO.Email)).ReturnsAsync((exsistingCandidate));

            var exeption = Assert.ThrowsAsync<Exception>(async () => await _candidateService.CreateCandidateAsync(candidateDTO));
            Assert.That(exeption.Message, Is.EqualTo("Email already exists!"));
        }
    }
}
