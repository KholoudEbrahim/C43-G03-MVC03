using Demo.BLL.Services;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentsController (IDepartmentService departmentService) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;

        public IActionResult Index()
        {
            return View();
        }
    }
}
