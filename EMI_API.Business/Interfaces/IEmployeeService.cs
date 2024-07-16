using EMI_API.Commons.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.Business.Interfaces
{
    public interface IEmployeeService
    {
        Task<Ok<List<EmployeeDTO>>> GetAll();
  
        Task<Results<Ok<EmployeeDTO>, NotFound>> GetById(int id);
        Task<Results<Created<EmployeeDTO>, ValidationProblem>> Create(CreateEmployeeDTO createEmployee);
        Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, CreateEmployeeDTO updateEmployee);
        Task<Results<NoContent, NotFound>> Delete(int id);
        Task<Results<NoContent, NotFound, BadRequest<string>>> SetProject(int id, List<int> projectsIds);
        Task<Results<Ok<List<EmployeeDTO>>, NotFound>> ByDeparmentIdInAnyProject(int deparmentId);
    }
}
