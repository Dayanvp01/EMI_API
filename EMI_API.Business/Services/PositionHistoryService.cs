using AutoMapper;
using EMI_API.Business.Interfaces;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;

namespace EMI_API.Business.Services
{
    public class PositionHistoryService: IPositionHistoryService
    {

        private readonly IPositionHistoryRepository repository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;
        public PositionHistoryService(IPositionHistoryRepository repository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.repository = repository;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        /// <returns></returns>
        public async Task<Ok<List<PositionHistoryDTO>>> GetByEmployeeId(int employeeId)
        {
            var entities = await repository.GetAllAsync() ?? new List<PositionHistory>();
            List<PositionHistoryDTO> result = mapper.Map<List<PositionHistoryDTO>>(entities);
            return TypedResults.Ok(result);
        }

        /// <summary>
        /// obtiene entidad by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>entida encontrada o nofound</returns>
        public async Task<Results<Ok<PositionHistoryDTO>, NotFound>> GetById(int id)
        {
            var result = await repository.GetByIdAsync(id);
            if (result is null)
            {
                return TypedResults.NotFound();
            }
            return TypedResults.Ok(mapper.Map<PositionHistoryDTO>(result));
        }

        /// <summary>
        /// Crea una nueva entidad
        /// </summary>
        /// <param name="name">nombre de la entidad</param>
        /// <returns></returns>
        public async Task<Results<Created<PositionHistoryDTO>, NotFound,ValidationProblem>> Create(int employeeId, CreatePositionDTO createPosition)
        {
            var existEmployee= await employeeRepository.ExistsAsync(employeeId);
            if (!existEmployee)
            {
                return TypedResults.NotFound();
            }
            var entity = mapper.Map<PositionHistory>(createPosition);
            entity.EmployeeId = employeeId;
            var entityDTO = mapper.Map<PositionHistoryDTO>(await repository.AddAsync(entity));
            return TypedResults.Created($"/api/positionsHistories/{entityDTO.Id}", entityDTO);
        }

        /// <summary>
        /// Actualiza la entidad by Id
        /// </summary>
        /// <param name="id">Id de la entidad</param>
        /// <param name="name">nuevo nombre</param>
        /// <returns></returns>
        public async Task<Results<NoContent, NotFound, ValidationProblem>> Update(int id, CreatePositionDTO updatePosition)
        {
            var entity = await repository.GetByIdAsync(id);
            if (entity != null)
            {
                return TypedResults.NotFound();
            }
            var updateEntity = mapper.Map<PositionHistory>(updatePosition);
            updateEntity.EmployeeId = entity!.EmployeeId;
            await repository.UpdateAsync(updateEntity);
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
