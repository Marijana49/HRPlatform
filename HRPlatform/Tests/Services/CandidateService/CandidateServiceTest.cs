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

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.CreateCandidateAsync(candidateDTO));
            Assert.That(exeption.Message, Is.EqualTo("Email already exists!"));
        }

        [Test]
        public async Task CreateCandidate_NoSkillsTest()
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

            _skillRepo.Setup(r => r.GetSkillsByName(It.IsAny<List<string>>())).ReturnsAsync(new List<Skill>());
            _candidateRepo.Setup(r => r.GetCandidateByEmailAsync(candidateDTO.Email)).ReturnsAsync((Candidate)null);

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.CreateCandidateAsync(candidateDTO));
            Assert.That(exeption.Message, Is.EqualTo("Skills not found!"));
        }

        [Test]
        public async Task RemoveCandidate_Test()
        {
            var removeCandidate = new CandidateForRemove
            {
                Id = 1,
            };

            var exsistingCandidate = new Candidate { Id = 1 };
            _candidateRepo.Setup(r => r.GetByIdAsync(removeCandidate.Id)).ReturnsAsync(exsistingCandidate);

            await _candidateService.RemoveCandidateAsync(removeCandidate);
            _candidateRepo.Verify(r => r.DeleteAsync(exsistingCandidate), Times.Once);
        }

        [Test]
        public async Task RemoveCandidateExeption_Test()
        {
            var removeCandidate = new CandidateForRemove
            {
                Id = 1,
            };

            _candidateRepo.Setup(r => r.GetByIdAsync(removeCandidate.Id)).ReturnsAsync((Candidate)null);
            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.RemoveCandidateAsync(removeCandidate));
            Assert.That(exeption.Message, Is.EqualTo("Candidate doesn't exist!"));
        }

        [Test]
        public async Task RemoveCandidateSkill_Test()
        {
            var skillForRemove = "Test";
            var existingSkill = new Skill { Name = "Test" };
            var candidate = new Candidate { Id = 1 };

            _skillRepo.Setup(r => r.GetSkillByName(skillForRemove)).ReturnsAsync(existingSkill);
            _candidateRepo.Setup(r => r.GetByIdAsync(candidate.Id)).ReturnsAsync(candidate);

            await _candidateService.RemoveCandidateSkillAsync(candidate.Id, skillForRemove);
        }

        [Test]
        public async Task RemoveCandidateSkillExeption_Test()
        {
            var skillForRemove = "Test";
            var candidate = new Candidate { Id = 1 };

            _skillRepo.Setup(r => r.GetSkillByName(skillForRemove)).ReturnsAsync((Skill)null);
            _candidateRepo.Setup(r => r.GetByIdAsync(candidate.Id)).ReturnsAsync(candidate);

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.RemoveCandidateSkillAsync(candidate.Id, skillForRemove));
            Assert.That(exeption.Message, Is.EqualTo($"Skill {skillForRemove} doesn't exist!"));
        }

        [Test]
        public async Task CandidateNotFound_RemoveSkillTest()
        {
            var skill = "Test";
            var existingSkill = new Skill { Name = "Test" };
            var candidate = new Candidate { Id = 1 };

            _skillRepo.Setup(r => r.GetSkillByName(skill)).ReturnsAsync(existingSkill);
            _candidateRepo.Setup(r => r.GetByIdAsync(candidate.Id)).ReturnsAsync((Candidate)null);

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.RemoveCandidateSkillAsync(candidate.Id, skill));
            Assert.That(exeption.Message, Is.EqualTo("Candidate not found!"));
        }

        [Test]
        public async Task UpdateCandidateSkill_Test()
        {
            var skillForUpdate = "Test";
            var existingSkill = new Skill { Name = "Test" };
            var candidate = new Candidate { Id = 1 };

            _skillRepo.Setup(r => r.GetSkillByName(skillForUpdate)).ReturnsAsync(existingSkill);
            _candidateRepo.Setup(r => r.GetByIdAsync(candidate.Id)).ReturnsAsync(candidate);

            await _candidateService.UpdateCandidateSkillAsync(candidate.Id, skillForUpdate);
        }

        [Test]
        public async Task UpdateCandidateSkillExeption_Test()
        {
            var skillForExeption = "Test";
            var candidate = new Candidate { Id = 1 };

            _skillRepo.Setup(r => r.GetSkillByName(skillForExeption)).ReturnsAsync((Skill)null);
            _candidateRepo.Setup(r => r.GetByIdAsync(candidate.Id)).ReturnsAsync(candidate);

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.UpdateCandidateSkillAsync(candidate.Id, skillForExeption));
            Assert.That(exeption.Message, Is.EqualTo($"Skill {skillForExeption} doesn't exist!"));
        }

        [Test]
        public async Task CandidateNotFound_UpdateSkillTest()
        {
            var skill = "Test";
            var existingSkill = new Skill { Name = "Test" };
            var candidate = new Candidate { Id = 1 };

            _skillRepo.Setup(r => r.GetSkillByName(skill)).ReturnsAsync(existingSkill);
            _candidateRepo.Setup(r => r.GetByIdAsync(candidate.Id)).ReturnsAsync((Candidate)null);

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.UpdateCandidateSkillAsync(candidate.Id, skill));
            Assert.That(exeption.Message, Is.EqualTo("Candidate not found!"));
        }

        [Test]
        public async Task SearchCandidate_Test()
        {
            var name = "Name";
            var skills = new List<string> { "skill1", "skill2" };
            var candidateList = new List<Candidate> { new Candidate { Id = 1, FullName = "Test" } };

            _candidateRepo.Setup(r => r.SearchCandidateAsync(It.IsAny<string>(), It.IsAny<List<string>>())).ReturnsAsync(candidateList);

            var res = await _candidateService.SearchCandidateAsync(name, skills);

            Assert.NotNull(res);
        }

        [Test]
        public async Task SearchCandidate_NotFoundTest()
        {
            var name = "Name";
            var skills = new List<string> { "skill1", "skill2" };

            _candidateRepo.Setup(r => r.SearchCandidateAsync(It.IsAny<string>(), It.IsAny<List<string>>())).ReturnsAsync(new List<Candidate>());

            var exeption = Assert.ThrowsAsync<KeyNotFoundException>(async () => await _candidateService.SearchCandidateAsync(name, skills));
            Assert.That(exeption.Message, Is.EqualTo("Candidate not found!"));
        }
    }
}