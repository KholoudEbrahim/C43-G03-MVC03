global using Demo.BLL.DataTransferObjects.Departments;
global using Demo.DAL.Repositories;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services
{
    public class DepartmentService(IGenericRepository<Department> repository) : IDepartmentService
    //Injection
    {
        private readonly IGenericRepository<Department> _repository = repository;

        //GetAll

        public IEnumerable<DepartmentResponse> GetAll()
        {
            var departments = _repository.GetAll();

            return departments.Select(department => department.ToResponse());
        }


        //Get
        public DepartmentDetailsResponse? GetById(int id)
        {
            var department = _repository.GetById(id);


            //Manual Mapping
            //AutoMapper  <<<
            //Mapster 
            //Extension Methods 
            return department is null ? null : department.ToDetailsResponse();

        }

        //Add
        public int Add(DepartmentRequest request)
        {
            var department = request.ToEntity();
            return _repository.Add(department);
        }

        //Update
        public int Update(DepartmentUpdateRequest request)
        {
            var department = request.ToEntity();
            return _repository.Update(department);
        }

        //Delete
        public bool Delete(int id)
        {
            var department = _repository.GetById(id);
            if (department is null) return false;
            return _repository.Delete(department) > 0 ? true : false;


        }


    }
}

//for search
//Ctrl + t 
// Ctrl + ;