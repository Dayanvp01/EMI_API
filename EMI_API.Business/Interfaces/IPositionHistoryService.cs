using EMI_API.Commons.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.Business.Interfaces
{
    public interface IPositionHistoryService
    {
        Task<Ok<List<PositionHistoryDTO>>> GetByEmployeeId(int employeeId);
        Task<Results<Ok<PositionHistoryDTO>, NotFound>> GetById(int id);
        Task<Results<Created<PositionHistoryDTO>, NotFound, ValidationProblem>> Create(int employeeId, CreatePositionDTO createPosition);
        Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, CreatePositionDTO updatePosition);
        Task<Results<NoContent, NotFound>> Delete(int id);
    }
}
