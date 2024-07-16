namespace EMI_API.Commons.DTOs
{
    public class PositionHistoryDTO
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string Position { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}

