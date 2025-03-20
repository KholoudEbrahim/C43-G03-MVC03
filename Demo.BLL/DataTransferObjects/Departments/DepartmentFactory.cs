using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects.Departments

{                              //Extension Methods
    public static class DepartmentFactory
    {
        public static DepartmentResponse ToResponse(this Department department) => new()
        {
            Id = department.Id,
            Name = department.Name,
            Code = department.Code,
            CreatedOn = department.CreatedOn,
        };

        public static DepartmentDetailsResponse ToDetailsResponse(this Department department) => new()

        {

            Id = department.Id,

            Name = department.Name,
            Description = department.Description,
            CreatedBy = department.CreatedBy,
            CreatedOn = department.CreatedOn,
            IsDeleted = department.IsDeleted,
            Code = department.Code,
            LastModifiedBy = department.LastModifiedBy,
            LastModifiedOn = department.LastModifiedOn

        };

        public static Department ToEntity(this DepartmentRequest departmentRequest) => new()
        {
            Name = departmentRequest.Name,
            Description = departmentRequest.Description,
            Code = departmentRequest.Code,
            CreatedOn = departmentRequest.CreatedOn,

        };

        public static Department ToEntity(this DepartmentUpdateRequest departmentRequest) => new()

        {
            Id = departmentRequest.Id,
            Name = departmentRequest.Name,
            Description = departmentRequest.Description,
            Code = departmentRequest.Code,
        };
        public static DepartmentUpdateRequest ToRequest(this DepartmentDetailsResponse department) => new() { 
                  Id = department.Id,
                  Name = department.Name,
              Description = department.Description,
            CreatedOn = (department.CreatedOn),
              Code = department.Code,
        };

    }
}
