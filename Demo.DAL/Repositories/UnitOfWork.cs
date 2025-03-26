using Demo.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public UnitOfWork(ApplicationDbContext context,
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository) // DI

        {

            _context = context;

            _employeeRepository = employeeRepository;

            _departmentRepository = departmentRepository;

        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository;

        public IDepartmentRepository DepartmentRepository => _departmentRepository;

        public int SaveChanges() => _context.SaveChanges();


        //public IEmployeeRepository Employees => _employeeRepository;

        //public IDepartmentRepository Departments => _departmentRepository;

        //public int SaveChanges() => _context.SaveChanges();
    }
}
