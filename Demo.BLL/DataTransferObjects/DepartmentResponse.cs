﻿using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects
{
    public class DepartmentResponse
    {
        public DepartmentResponse()
        {
        }

        public DepartmentResponse(Department department) 
        {
 
        }
        public int Id { get; set; } 
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedOn { get; set; }


    }
}
