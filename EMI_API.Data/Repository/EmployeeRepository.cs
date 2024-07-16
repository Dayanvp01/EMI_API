using AutoMapper;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMI_API.Data.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        public EmployeeRepository(ApplicationDbContext context, IMapper mapper) : base(context) { 
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<Employee>> ByDepartmentIdInAnyProject(int departmentId)
        {
            return await context.Employees.Where(x => x.DepartmentId == departmentId && x.EmployeesProjects.Any())
                                                .Include(x=> x.Department)
                                                .Include(x => x.PositionsHistories)
                                                .Include(x => x.EmployeesProjects).ThenInclude(ep => ep.Project)
                                                .ToListAsync();
            
        }

        public async Task SetProjects(int id, List<int> projectsIds)
        {
           var employee= await context.Employees
                .Include(x=> x.EmployeesProjects)
                .FirstOrDefaultAsync(x=> x.Id == id);

            if (employee is null)
            {
                throw new ArgumentException($"No existe un empleado con el id {id}");
            }
            var employeeProject = projectsIds.Select(pId => new EmployeeProject() { ProjectId = pId });
            employee.EmployeesProjects = mapper.Map(employeeProject, employee.EmployeesProjects);
            await context.SaveChangesAsync();
        }
    }
}
