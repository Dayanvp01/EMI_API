using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.EndPoints
{
    public static class PositionsHistoriesEndPoints
    {
        public static RouteGroupBuilder MapPositionsHistories(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create).DisableAntiforgery();
            group.MapPut("/{id:int}", Update).DisableAntiforgery();
            group.MapDelete("/{id:int}", Delete).RequireAuthorization(Roles.ADMIN);
            return group;
        }


        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Ok<List<PositionHistoryDTO>>> GetAll(IPositionHistoryService service, int employeeId)
        {
            return await service.GetByEmployeeId(employeeId);
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Results<Ok<PositionHistoryDTO>, NotFound>> GetById(IPositionHistoryService service, int id)
        {
            return await service.GetById(id);
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<Created<PositionHistoryDTO>, NotFound, ValidationProblem>> Create(IPositionHistoryService service, int employeeId, CreatePositionDTO createPosition)
        {
            return await service.Create(employeeId, createPosition);
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<NoContent, NotFound, ValidationProblem>> Update(IPositionHistoryService service, int id, CreatePositionDTO updatePosition)
        {
            return await service.Update(id, updatePosition);
        }

        static async Task<Results<NoContent, NotFound>> Delete(IPositionHistoryService service, int id)
        {
            return await service.Delete(id);
        }
    }
}
