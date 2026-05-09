namespace Domain.Models
{
    public class CandidateSkill
    {
        public int CandidateId { get; set; }
        public Candidate Candidate { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public CandidateSkill() { }

        public CandidateSkill(int candidateId, int skillId)
        {
            CandidateId = candidateId;
            SkillId = skillId;
        }
    }
}
