using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public interface IEmployeeRepository
    {
        int Add(Employee Employee);
        int Delete(Employee Employee);
        IEnumerable<Employee> GetAll(bool withTracking = false);
        Employee? GetById(int id);
        int Update(Employee Employee);
    }
}
