using Demo.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public class EmployeeRepository(ApplicationDbContext context) : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Employee? GetById(int id)
        {
            return _context.Employees.Find(keyValues: id);
        }


        //Get all 
        public IEnumerable<Employee> GetAll(bool withTracking = false)
            => withTracking ? _context.Employees.Where(d => !d.IsDeleted).ToList() :
            _context.Employees.AsNoTracking().Where(d => !d.IsDeleted).ToList();

        //Add
        public int Add(Employee Employee)
        {
            _context.Employees.Add(Employee);
            return _context.SaveChanges();
        }

        // Update

        public int Update(Employee Employee)
        {
            _context.Employees.Update(Employee);
            return _context.SaveChanges();
        }
        // Delete
        public int Delete(Employee Employee)

        {

            _context.Employees.Remove(Employee);

            return _context.SaveChanges();
        }

    }
}

