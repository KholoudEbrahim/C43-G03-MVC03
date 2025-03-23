using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.DataTransferObjects.Departments
{
    public class DepartmentRequest
    {
        [Required(ErrorMessage = "Name Is Required !!??")]

        [StringLength(maximumLength: 50, MinimumLength = 5)]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Code { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
