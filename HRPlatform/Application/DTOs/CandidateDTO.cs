namespace Application.DTOs
{
    public class CandidateDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateOnly BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public List<string> Skills { get; set; } = new List<string>();
    }
}
