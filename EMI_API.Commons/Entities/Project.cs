namespace EMI_API.Commons.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public List<EmployeeProject> EmployeesProjects { get; set; } = new List<EmployeeProject>();
    }
}
