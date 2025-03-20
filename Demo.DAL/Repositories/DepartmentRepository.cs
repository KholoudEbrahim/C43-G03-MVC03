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
    public class DepartmentRepository(ApplicationDbContext context)
        : GenericRepository<Department> (context)
        , IDepartmentRepository
    {
       
    }
   
}
