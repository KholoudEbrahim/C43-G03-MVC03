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
        //private readonly Lazy <IEmployeeRepository> _employeeRepository;
        //private readonly Lazy <IDepartmentRepository> _departmentRepository;


        private readonly Func<IEmployeeRepository> _employeeRepositoryFactory;

        private readonly Func<IDepartmentRepository> _departmentRepositoryFactory;
   
        public UnitOfWork(ApplicationDbContext context, 
            Func<IEmployeeRepository> employeeRepositoryFactory
            , Func<IDepartmentRepository> departmentRepositoryFactory) // DI

        {

            _context = context;
            _employeeRepositoryFactory = employeeRepositoryFactory;
            _departmentRepositoryFactory = departmentRepositoryFactory;
            //_employeeRepository = new Lazy<IEmployeeRepository>(()=> new EmployeeRepository(context));

            //_departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(context));

        }
        private IEmployeeRepository _employeeRepository;
        private IDepartmentRepository _departmentRepository;
        public IEmployeeRepository EmployeeRepository => _employeeRepository ?? _employeeRepositoryFactory.Invoke();

        public IDepartmentRepository DepartmentRepository => _departmentRepository ?? _departmentRepositoryFactory.Invoke();

        public int SaveChanges() => _context.SaveChanges();


        //public IEmployeeRepository Employees => _employeeRepository;

        //public IDepartmentRepository Departments => _departmentRepository;

        //public int SaveChanges() => _context.SaveChanges();
    }
}
