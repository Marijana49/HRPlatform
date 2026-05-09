namespace Domain.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CandidateSkill> CandidateSkills { get; set; }

        public Skill() { }

        public Skill(string name, List<CandidateSkill> candidateSkills)
        {
            Name = name;
            CandidateSkills = candidateSkills;
        }
    }
}
