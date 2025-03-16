﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects
{
    public class DepartmentDetailsResponse
    {
        public int Id { get; set; } 
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; } 
        public DateTime CreatedOn { get; set; }
        public int LastModifiedBy { get; set; } 
        public DateTime LastModifiedOn { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
    }
}
