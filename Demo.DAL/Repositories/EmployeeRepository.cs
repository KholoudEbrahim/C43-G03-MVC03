using Demo.DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public class EmployeeRepository(ApplicationDbContext context)
        : GenericRepository<Employee>(context)
        , IEmployeeRepository
    {
        public IEnumerable<Employee> GetAll(string name)
        {
            throw new NotImplementedException();
        }
    }
}

