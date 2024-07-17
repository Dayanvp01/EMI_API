using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Enums;
using EMI_API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.EndPoints
{
    public static class EmployeesEndPoints
    {
        private static readonly string entity = "Employee";
        public static RouteGroupBuilder MapEmployees(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll).GetDocumentation(entity); ;
            group.MapGet("/{id:int}", GetById).GetByIdDocumentation(entity); ;
            
            group.MapPost("/", Create).DisableAntiforgery().CreateDocumentation(entity); ;
           
            group.MapPut("/{id:int}", Update).DisableAntiforgery().UpdateDocumentation(entity); 
            group.MapDelete("/{id:int}", Delete).RequireAuthorization(Roles.ADMIN)
                .DeleteDocumentation(entity);

            group.MapPost("ByDepartmentIdInAnyProject/{deparmentId:int}", ByDepartmentIdInAnyProject)
                .WithOpenApiDocumentation(
                        summary: $"Obtener {entity} por department",
                        description: $"Obtiene los {entity} por departament que estan en al menos un proyecto",
                        paramDescription: $"El id del department a consultar {entity}",
                        requestBodyDescription:""
                    );

            group.MapPost("/{id:int}/setProject", SetProjects)
                     .WithOpenApiDocumentation(
                        summary: $"Asigna un proyecto a un {entity}",
                        description: $"Asigna una lista de proyectos a un {entity}",
                        paramDescription: $"El id del {entity} a asignar proyetos",
                        requestBodyDescription: "Lista de ids de los proyectos a asignar"
                    );

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
