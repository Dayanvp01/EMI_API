using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Enums;
using EMI_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;


namespace EMI_API.EndPoints
{
    public static class DepartmentsEndPoints
    {
        private static readonly string entity = "Department";
        public static RouteGroupBuilder MapDepartments(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).GetDocumentation(entity);
            group.MapGet("/{id:int}", GetById).GetByIdDocumentation(entity);
            group.MapPost("/", Create).DisableAntiforgery().CreateDocumentation(entity);
            group.MapPut("/{id:int}", Update).DisableAntiforgery().UpdateDocumentation(entity);
            group.MapDelete("/{id:int}", Delete).RequireAuthorization(Roles.ADMIN)
                .DeleteDocumentation(entity);
            return group;
        }


        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Ok<List<DepartmentDTO>>> GetAll(IDepartmentService service)
        {
            return await service.GetAll();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Results<Ok<DepartmentDTO>, NotFound>> GetById(IDepartmentService service, int id)
        {
            return await service.GetById(id);
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<Created<DepartmentDTO>, ValidationProblem>> Create(IDepartmentService service, string name)
        {
            return await service.Create(name);
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<NoContent, NotFound, ValidationProblem>> Update(IDepartmentService service, int id, string name)
        {
            return await service.Update(id, name);
        }

        static async Task<Results<NoContent, NotFound>> Delete(IDepartmentService service, int id)
        {
            return await service.Delete(id);
        }
    }
}
