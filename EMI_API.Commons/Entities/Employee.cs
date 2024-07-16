
namespace EMI_API.Commons.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Salary { get; set; }
        /// <summary>
        /// 1 For Regular, 2 For Manager
        /// </summary>
        public int CurrentPosition { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public List<PositionHistory> PositionsHistories { get; set; } = new List<PositionHistory>();
        public List<EmployeeProject> EmployeesProjects { get; set; } = new List<EmployeeProject>();

    }
}
