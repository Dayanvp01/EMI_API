using EMI_API.Commons.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.Business.Interfaces
{
    public interface IDepartmentService
    {
        Task<Ok<List<DepartmentDTO>>> GetAll();
        Task<Results<Ok<DepartmentDTO>, NotFound>> GetById(int id);
        Task<Results<Created<DepartmentDTO>, ValidationProblem>> Create(string name);
        Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, string name);
        Task<Results<NoContent, NotFound>> Delete(int id);
    }
}
