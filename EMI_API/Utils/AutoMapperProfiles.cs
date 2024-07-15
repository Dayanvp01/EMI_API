using AutoMapper;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.Entities;

namespace EMI_API.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<Employee, EmployeeDTO>();

        }
    }
}
