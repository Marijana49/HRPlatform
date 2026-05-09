namespace Domain.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<CandidateSkill> Skills { get; set; } = new List<CandidateSkill>();

        public Candidate() { }

        public Candidate(string fullName, string birthDate, string contactNumber, string email, List<CandidateSkill> skills)
        {
            FullName = fullName;
            BirthDate = birthDate;
            ContactNumber = contactNumber;
            Email = email;
            Skills = skills;
        }
    }
}
