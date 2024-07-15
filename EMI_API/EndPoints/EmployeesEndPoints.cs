using EMI_API.Commons.DTOs;
using EMI_API.Commons.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System;

namespace EMI_API.EndPoints
{
    public static class EmployeesEndPoints
    {

        public static RouteGroupBuilder MapEmployees(this RouteGroupBuilder group)
        {
            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);

            group.MapPost("/", Create).DisableAntiforgery();

            group.MapPut("/{id:int}", Update).DisableAntiforgery();

            group.MapDelete("/{id:int}", Delete);
            return group;
        }

        static async Task<Ok<List<EmployeeDTO>>> GetAll()
        {
            var result = new List<EmployeeDTO>(); // agregar servicio para obtenet todos
            return TypedResults.Ok(result);
        }

        static async Task<Results<Ok<EmployeeDTO>, NotFound>> GetById( int id)
        {
            var result = new EmployeeDTO();// agregar servicio para obtener por Id
            if (result is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(result);
        }

        static async Task<Results<Created<EmployeeDTO>, ValidationProblem>> Create(CreateEmployeeDTO newEntity)
        {
            var entityDTO =  new EmployeeDTO(); // Agregar servicio para crear nuevo registro
            return TypedResults.Created($"/employees/{entityDTO.Id}", entityDTO);
        }

        static async Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, CreateEmployeeDTO employee)
        {
            var result = true;// Agregar servicio para actualizar
            if (!result)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.NoContent();
        }

        static async Task<Results<NoContent, NotFound>> Delete(int id)
        {
            var result = true;//Agregar servicio para borrar un registro
            if (!result)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.NoContent();
        }
    }
}
