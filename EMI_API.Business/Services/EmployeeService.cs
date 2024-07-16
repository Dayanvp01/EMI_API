using AutoMapper;
using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;
        public EmployeeService(IEmployeeRepository repository, IProjectRepository projectRepository, IMapper mapper) {
            this.repository = repository;
            this.projectRepository = projectRepository;
            this.mapper = mapper;
        }

        public async Task<Ok<List<EmployeeDTO>>> GetAll()
        {
            var employees = await repository.GetAllAsync()?? new List<Employee>();
            List<EmployeeDTO> employeesDTO = mapper.Map<List<EmployeeDTO>>(employees);
            return TypedResults.Ok(employeesDTO);
        }

        public async Task<Results<Ok<EmployeeDTO>, NotFound>> GetById(int id)
        {
            var result = await repository.GetByIdAsync(id);
            if (result is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(mapper.Map<EmployeeDTO>(result));
        }

        public async Task<Results<Created<EmployeeDTO>, ValidationProblem>>
            Create(CreateEmployeeDTO createEmployee)
        {
            var entity = mapper.Map<Employee>(createEmployee);
            var entityDTO = mapper.Map<EmployeeDTO>(await repository.AddAsync(entity));
            return TypedResults.Created($"/api/employees/{entityDTO.Id}", entityDTO);
        }
       
        public async Task<Results<NoContent, NotFound, ValidationProblem>>
            Update(int id, CreateEmployeeDTO updateEmployee)
        {
            var exist = await repository.ExistsAsync(id);
            if (!exist)
            {
                return TypedResults.NotFound();
            }
            var entity = mapper.Map<Employee>(updateEmployee);
            entity.Id= id;
            await repository.UpdateAsync(entity);
            return TypedResults.NoContent();
        }

        public async Task<Results<NoContent, NotFound>> Delete(int id)
        {
            var exist = await repository.ExistsAsync(id);
            if (!exist)
            {
                return TypedResults.NotFound();
            }
            await repository.DeleteAsync(id);
            return TypedResults.NoContent();
        }

        public async Task<Results<NoContent, NotFound, BadRequest<string>>> SetProject(int id, List<int> projectsIds)
        {
            var exist = await repository.ExistsAsync(id);
            if (!exist)
            {
                return TypedResults.NotFound();
            }
            var existingProjects = new List<int>();
            if (projectsIds.Count != 0)
            {
                existingProjects = await projectRepository.Exist(projectsIds);
            }
            if (existingProjects.Count != projectsIds.Count)
            {
                var notExistingProjects = projectsIds.Except(existingProjects);
                return TypedResults.BadRequest($"Los proyectos de id {string.Join(",", notExistingProjects)} no existen.");
            }
            await repository.SetProjects(id, projectsIds);
            return TypedResults.NoContent();
        }

        public async Task<Results<Ok<List<EmployeeDTO>>, NotFound>> ByDeparmentIdInAnyProject(int deparmentId)
        {
            var result = await repository.ByDepartmentIdInAnyProject(deparmentId);
            if (result is null)
            {
                return TypedResults.NotFound();
            }
            var employees= mapper.Map<List<EmployeeDTO>>(result);
            return TypedResults.Ok(employees);
        }
    }
}
