using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.EndPoints
{
    public static class EmployeesEndPoints
    {

        public static RouteGroupBuilder MapEmployees(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);
            group.MapPost("ByDepartmentIdInAnyProject/{deparmentId:int}", ByDepartmentIdInAnyProject);
            group.MapPost("/", Create).DisableAntiforgery();
            group.MapPost("/{id:int}/setProject", SetProjects);
            group.MapPut("/{id:int}", Update).DisableAntiforgery();
            group.MapDelete("/{id:int}", Delete).RequireAuthorization(Roles.ADMIN);
            return group;
        }


        [Authorize(Roles = Roles.ADMIN)]
        private static async Task<Results<NoContent, NotFound, BadRequest<string>>> SetProjects(IEmployeeService service,int id, List<int> projectsIds)
        {
            return await service.SetProject(id, projectsIds);
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Ok<List<EmployeeDTO>>> GetAll(IEmployeeService service)
        {
            return await service.GetAll();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Results<Ok<EmployeeDTO>, NotFound>> GetById(IEmployeeService service,int id)
        {
            return await service.GetById(id);
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Results<Ok<List<EmployeeDTO>>, NotFound>> ByDepartmentIdInAnyProject(IEmployeeService service, int deparmentId)
        {
            return await service.ByDeparmentIdInAnyProject(deparmentId); 
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<Created<EmployeeDTO>, ValidationProblem>> Create(IEmployeeService service, CreateEmployeeDTO newEntity)
        {
            return await service.Create(newEntity);
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<NoContent, NotFound, ValidationProblem>> Update(IEmployeeService service, int id, CreateEmployeeDTO employee)
        {
           return await service.Update(id, employee);
        }

        static async Task<Results<NoContent, NotFound>> Delete(IEmployeeService service, int id)
        {
            return await service.Delete(id);
        }
    }
}
