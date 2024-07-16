using EMI_API.Commons.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.Business.Interfaces
{
    public interface IProjectService
    {
        Task<Ok<List<ProjectDTO>>> GetAll();
        Task<Results<Ok<ProjectDTO>, NotFound>> GetById(int id);
        Task<Results<Created<ProjectDTO>, ValidationProblem>> Create(CreateProjectDTO createProject);
        Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, CreateProjectDTO updateProject);
        Task<Results<NoContent, NotFound>> Delete(int id);
    }
}
