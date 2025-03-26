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
        private readonly ApplicationDbContext _context; //UnManaged Res
        private readonly Lazy <IEmployeeRepository> _employeeRepository;
        private readonly Lazy <IDepartmentRepository> _departmentRepository;

        public UnitOfWork(ApplicationDbContext context) // DI

        {

            _context = context;

            _employeeRepository = new Lazy<IEmployeeRepository>(()=> new EmployeeRepository(context));

            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(context));

        }

        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges() => _context.SaveChanges();


        //public IEmployeeRepository Employees => _employeeRepository;

        //public IDepartmentRepository Departments => _departmentRepository;

        //public int SaveChanges() => _context.SaveChanges();
    }
}
