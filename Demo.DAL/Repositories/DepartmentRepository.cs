using Demo.DAL.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    //CRUD Operations
    public class DepartmentRepository(ApplicationDbContext context) : IDepartmentRepository
    {
        private readonly ApplicationDbContext _context = context;

        public Department? GetById(int id)
        {
            return _context.Departments.Find(keyValues: id);
        }


        //Get all 
        public IEnumerable<Department> GetAll(bool withTracking = false)
            => withTracking ? _context.Departments.Where(d => !d.IsDeleted).ToList() :
            _context.Departments.AsNoTracking().Where(d => !d.IsDeleted).ToList();

        //Add
        public int Add(Department department)
        {
            _context.Departments.Add(department);
            return _context.SaveChanges();
        }

        // Update

        public int Update(Department department)
        {
            _context.Departments.Update(department);
            return _context.SaveChanges();
        }
        // Delete
        public int Delete(Department department)

        {

            _context.Departments.Remove(department);

            return _context.SaveChanges();
        }

    }
    //Repository
}
