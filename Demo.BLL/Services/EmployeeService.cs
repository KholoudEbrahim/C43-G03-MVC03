global using AutoMapper;
global using Demo.BLL.DataTransferObjects.Employees;
using Demo.DAL.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services
{
    public class EmployeeService(IUnitOfWork unitOfWork
        , IMapper mapper)
        : IEmployeeService
    {
        //private readonly IGenericRepository<Employee> _unitOfWork.employeeRepository = repository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        //GetAll

        public IEnumerable<EmployeeResponse> GetAll(string? SearchValue)
        {
            ///var Employees = _unitOfWork.employeeRepository.GetAll();    
            ///return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResponse>>(Employees)
            ///Filtration => Remote
            /// Projection => Remote
            ///var employees = _unitOfWork.employeeRepository.GetAllQueryable().Select(e => new EmployeeResponse
            ///{
            ///    Id = e.Id,
            ///    Age = e.Age,
            ///    Email = e.Email,
            ///    EmployeeType = e.EmployeeType.ToString(),
            ///    Gender = e.Gender.ToString(),
            ///    IsActive = e.IsActive,
            ///    Name = e.Name,
            ///    Salary = e.Salary
            ///});


            if (string.IsNullOrWhiteSpace(SearchValue))
            return _unitOfWork.employeeRepository.GetAll(e => new EmployeeResponse
            {
                Id = e.Id,
                Age = e.Age,
                Email = e.Email,
                EmployeeType = e.EmployeeType.ToString(),
                Gender = e.Gender.ToString(),
                IsActive = e.IsActive,
                Name = e.Name,
                Salary = e.Salary,
                Department = e.Department.Name

            }, e => !e.IsDeleted ,
            e => e.Department);

            return _unitOfWork.employeeRepository.GetAll(e => new EmployeeResponse
            {
                Id = e.Id,
                Age = e.Age,
                Email = e.Email,
                EmployeeType = e.EmployeeType.ToString(),
                Gender = e.Gender.ToString(),
                IsActive = e.IsActive,
                Name = e.Name,
                Salary = e.Salary,
                Department = e.Department.Name

            }, e => !e.IsDeleted && e.Name.ToLower().Contains(SearchValue.ToLower()),
            e => e.Department);

            //&& e.Name.ToLower().Contains(SearchValue.ToLower())


        }
        //        return employees;
        //    }


        //Get
        public EmployeeDetailsResponse? GetById(int id)
        {
            var Employee = _unitOfWork.employeeRepository.GetById(id);


            //Manual Mapping
            //AutoMapper  <<<
            //Mapster 
            //Extension Methods 
            return Employee is null ? null : _mapper.Map<Employee, EmployeeDetailsResponse>(Employee);

        }

        //Add
        public int Add(EmployeeRequest request)
        {
            var Employee = _mapper.Map<EmployeeRequest, Employee>(request);
            return _unitOfWork.employeeRepository.Add(Employee);
        }

        //Update
        public int Update(EmployeeUpdateRequest request)
        {
            var Employee = _mapper.Map<EmployeeUpdateRequest, Employee>(request);
            return _unitOfWork.employeeRepository.Update(Employee);
        }

        //Delete
        public bool Delete(int id)
        {
            var Employee = _unitOfWork.employeeRepository.GetById(id);
            if (Employee is null) 
                return false;
            Employee.IsDeleted = true;
            return _unitOfWork.employeeRepository.Delete(Employee) > 0 ? true : false;


        }

    }
}
