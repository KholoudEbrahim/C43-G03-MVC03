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
    public class DepartmentService(IUnitOfWork unitOfWork) : IDepartmentService
    //Injection
    {
      //  private readonly IGeneric__unitOfWork.Departments<Department> ___unitOfWork.Departments = __unitOfWork.Departments;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        //GetAll

        public IEnumerable<DepartmentResponse> GetAll()
        {
            var departments = _unitOfWork.DepartmentRepository.GetAll();

            return departments.Select(department => department.ToResponse());
        }


        //Get
        public DepartmentDetailsResponse? GetById(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);


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
            return _unitOfWork.SaveChanges();
        }

        //Update
        public int Update(DepartmentUpdateRequest request)
        {
            var department = request.ToEntity();
            return _unitOfWork.SaveChanges();
        }

        //Delete
        public bool Delete(int id)
        {
            var department = _unitOfWork.DepartmentRepository.GetById(id);
            if (department is null) return false;
            _unitOfWork.DepartmentRepository.Delete(department);
            return _unitOfWork.SaveChanges() > 0 ? true : false;


        }


    }
}

//for search
//Ctrl + t 
// Ctrl + ;