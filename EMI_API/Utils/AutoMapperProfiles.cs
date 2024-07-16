using AutoMapper;
using EMI_API.Commons.DTOs;
using EMI_API.Commons.DTOs.Identity;
using EMI_API.Commons.Entities;
using Microsoft.AspNetCore.Identity;

namespace EMI_API.Utils
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<CreateEmployeeDTO, Employee>().ReverseMap();
            CreateMap<CreateProjectDTO, Project>();
            CreateMap<CreatePositionDTO, PositionHistory>();



            CreateMap<Project, ProjectDTO>().ReverseMap();
           
            CreateMap<Department, DepartmentDTO>().ReverseMap();
            CreateMap<PositionHistory, PositionHistoryDTO>().ReverseMap();


            CreateMap<Employee, EmployeeDTO>()
                .ForMember(p => p.Projects,
                entity => entity.MapFrom(p =>
                p.EmployeesProjects.Select(ep => new ProjectDTO { Id = ep.ProjectId, Name = ep.Project.Name, Description=ep.Project.Description })));

           
            CreateMap<Employee, EmployeeDTO>()
              .ForMember(dest => dest.Projects, opt => opt.MapFrom(src => src.EmployeesProjects.Select(ep => ep.Project)));


            CreateMap<UserCredentialsDTO, IdentityUser>()
                .ForMember(x=> x.Email,y=> y.MapFrom(s=> s.Email))
                .ForMember(x => x.UserName, y => y.MapFrom(s => s.Email));

        }
    }
}
