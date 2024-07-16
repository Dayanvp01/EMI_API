using AutoMapper;
using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace EMI_API.Business.Services
{
    public  class ProjectService: IProjectService
    {
        private readonly IProjectRepository repository;
        private readonly IMapper mapper;
        public ProjectService(IProjectRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        /// <returns></returns>
        public async Task<Ok<List<ProjectDTO>>> GetAll()
        {
            var entities = await repository.GetAllAsync() ?? new List<Project>();
            List<ProjectDTO> result = mapper.Map<List<ProjectDTO>>(entities);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// obtiene entidad by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entida encontrada o nofound</returns>
        public async Task<Results<Ok<ProjectDTO>, NotFound>> GetById(int id)
        {
            var result = await repository.GetByIdAsync(id);
            if (result is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(mapper.Map<ProjectDTO>(result));
        }

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="name">nombre de la entidad</param>
        /// <returns></returns>
        public async Task<Results<Created<ProjectDTO>, ValidationProblem>> Create(CreateProjectDTO createProject)
        {
            var entity = mapper.Map<Project>(createProject);
            var entityDTO = mapper.Map<ProjectDTO>(await repository.AddAsync(entity));
            return TypedResults.Created($"/api/projects/{entityDTO.Id}", entityDTO);
        }

        /// <summary>
        /// Actualiza la entidad by Id
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        /// <param name="name">nuevo nombre</param>
        /// <returns></returns>
        public async Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, CreateProjectDTO updateProject)
        {
            var exist = await repository.ExistsAsync(id);
            if (!exist)
            {
                return TypedResults.NotFound();
            }
            var entity= mapper.Map<Project>(updateProject);
            entity.Id = id;
            await repository.UpdateAsync(entity);
            return TypedResults.NoContent();
        }

        /// <summary>
        /// Elimina entidad by Id
        /// </summary>
        /// <param name="id">id de la entidad</param>
        /// <returns></returns>
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
    }
}
