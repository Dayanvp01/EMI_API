using AutoMapper;
using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EMI_API.Business.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository  repository;
        private readonly IMapper mapper;
        public DepartmentService(IDepartmentRepository repository, IMapper mapper ) {
            this.repository = repository;
            this.mapper = mapper;
        }
    
        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        /// <returns></returns>
        public async Task<Ok<List<DepartmentDTO>>> GetAll()
        {
            var entities = await repository.GetAllAsync() ?? new List<Department>();
            List<DepartmentDTO> result = mapper.Map<List<DepartmentDTO>>(entities);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// obtiene entidad by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entida encontrada o nofound</returns>
        public async Task<Results<Ok<DepartmentDTO>, NotFound>> GetById(int id)
        {
            var result = await repository.GetByIdAsync(id);
            if (result is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(mapper.Map<DepartmentDTO>(result));
        }

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="name">nombre de la entidad</param>
        /// <returns></returns>
        public async Task<Results<Created<DepartmentDTO>, ValidationProblem>> Create(string name)
        {
            var entity = new Department { Name = name };
            var entityDTO = mapper.Map<DepartmentDTO>(await repository.AddAsync(entity));
            return TypedResults.Created($"/api/deparments/{entityDTO.Id}", entityDTO);
        }

        /// <summary>
        /// Actualiza la entidad by Id
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        /// <param name="name">nuevo nombre</param>
        /// <returns></returns>
        public async Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, string name)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity!=null)
            {
                return TypedResults.NotFound();
            }
            entity!.Name=name;
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
