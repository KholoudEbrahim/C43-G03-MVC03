using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Profiles
{
    internal class EmployeeProfile : Profile
    {

        public EmployeeProfile()
        {
            // Source => Dist


            CreateMap<Employee, EmployeeDetailsResponse>();

            CreateMap<Employee, EmployeeResponse>();

            CreateMap<EmployeeRequest, Employee>();

            CreateMap<EmployeeUpdateRequest, Employee>();
        }
    }

    // Model First Name ,Last Name

    //Dto Full Name

}