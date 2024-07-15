namespace EMI_API.Commons.Entities
{
    public class PositionHistory
    {
        public int EmployeeId { get; set; }
        public string Position { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Employee Employee { get; set; } = null!;
    }
}
