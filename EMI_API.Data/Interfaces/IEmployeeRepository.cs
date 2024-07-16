using EMI_API.Commons.Entities;

namespace EMI_API.Data.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task SetProjects(int id, List<int> projectsIds);

        Task<List<Employee>> ByDepartmentIdInAnyProject(int deparmentId);
    }
}
