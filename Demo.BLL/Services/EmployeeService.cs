
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services
{
    public class EmployeeService(IUnitOfWork unitOfWork
        , IMapper mapper, IAttachmentService attachmentService)
        : IEmployeeService
    {
        //private readonly IGenericRepository<Employee> _unitOfWork.employeeRepository = repository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        private readonly IAttachmentService _attachmentService = attachmentService;

        //GetAll
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
        public IEnumerable<EmployeeResponse> GetAll(string? SearchValue)
        {
            return _unitOfWork.EmployeeRepository
      .GetAllQueryable()
      .Where(e => !e.IsDeleted && (string.IsNullOrEmpty(SearchValue) || e.Name.Contains(SearchValue)))
      .Select(e => new EmployeeResponse
      {
          Id = e.Id,
          Age = e.Age,
          Email = e.Email,
          EmployeeType = e.EmployeeType.ToString(),
          Gender = e.Gender.ToString(),
          IsActive = e.IsActive,
          Name = e.Name,
          Salary = e.Salary,
          Department = e.Department.Name,
          Image = e.ImageName

      }).ToList();


            //&& e.Name.ToLower().Contains(SearchValue.ToLower())


        }
        //        return employees;
        //    }


        //Get
        public EmployeeDetailsResponse? GetById(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);


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

            if (request.Image is not null)
                Employee.ImageName =  _attachmentService.Upload(request.Image,"Imgs");
             _unitOfWork.EmployeeRepository.Add(Employee);
           


            return _unitOfWork.SaveChanges();


        }

        //Update
        public int Update(EmployeeUpdateRequest request)
        {
            var Employee = _mapper.Map<EmployeeUpdateRequest, Employee>(request);
            _unitOfWork.EmployeeRepository.Update(Employee);
            return _unitOfWork.SaveChanges();
        }

        //Delete
        public bool Delete(int id)
        {
            var Employee = _unitOfWork.EmployeeRepository.GetById(id);
            if (Employee is null) 
                return false;
            Employee.IsDeleted = true;
            _unitOfWork.EmployeeRepository.Delete(Employee);
              return _unitOfWork.SaveChanges() > 0 ? true : false;
          


        }

    }
}
