using AutoMapper;
using Demo.DAL.Models;
using Demo.PL.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Demo.PL.Helper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap() ;
            CreateMap<DepartmentViewModel,Department>().ReverseMap() ;  
            CreateMap<ApplicationUser,UsersViewModel>().ReverseMap() ;  
            CreateMap<RoleViewModel,IdentityRole>().ReverseMap()
                .ForMember(d=>d.RoleName,o=>o.MapFrom(s => s.Name  )).ReverseMap();   
        }

    }
}
