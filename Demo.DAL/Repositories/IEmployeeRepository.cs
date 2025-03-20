﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
       public IEnumerable<Employee> GetAll(string name);
    }
}
