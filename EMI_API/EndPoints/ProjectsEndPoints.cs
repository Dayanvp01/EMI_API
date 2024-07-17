using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Enums;
using EMI_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.EndPoints
{
    public static  class ProjectsEndPoints
    {
        private static readonly string entity = "Project";
        public static RouteGroupBuilder MapProjects(this RouteGroupBuilder group)
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
        static async Task<Ok<List<ProjectDTO>>> GetAll(IProjectService service)
        {
            return await service.GetAll();
        }

        [Authorize(Roles = $"{Roles.ADMIN},{Roles.USER}")]
        static async Task<Results<Ok<ProjectDTO>, NotFound>> GetById(IProjectService service, int id)
        {
            return await service.GetById(id);
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<Created<ProjectDTO>, ValidationProblem>> Create(IProjectService service, CreateProjectDTO createProject)
        {
            return await service.Create(createProject);
        }

        [Authorize(Roles = Roles.ADMIN)]
        static async Task<Results<NoContent, NotFound, ValidationProblem>> Update(IProjectService service, int id, CreateProjectDTO updateProject )
        {
            return await service.Update(id, updateProject);
        }

        static async Task<Results<NoContent, NotFound>> Delete(IProjectService service, int id)
        {
            return await service.Delete(id);
        }
    }
}
