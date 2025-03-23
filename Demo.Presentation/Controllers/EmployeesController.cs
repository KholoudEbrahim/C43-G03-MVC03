
using Demo.BLL.Services;
using Demo.DAL.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class EmployeesController (IEmployeeService EmployeeService,
        IWebHostEnvironment webHostEnvironment, ILogger<EmployeesController> logger) : Controller
    {
        private readonly IEmployeeService _EmployeeService = EmployeeService;
        private readonly IWebHostEnvironment _env = webHostEnvironment;
        private readonly ILogger<EmployeesController> _logger = logger;

        public IActionResult Index() //home page => All Employees
        {
            var Employees = _EmployeeService.GetAll();
            ViewData["message"] = "Hello From view Data";
            ViewBag.Message = new EmployeeDetailsResponse { Name = "Employee02" };
            return View(Employees); // Send Data from Action To View
        }

        #region Create

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(EmployeeRequest request)
        {

            if (!ModelState.IsValid) return View(request); // Server Side Validation
            string message;
            try
            {

                var result = _EmployeeService.Add(request);

                if (result > 0) message = $"Department {request.Name} Created";
                else message = $"cant Create Department {request.Name}";
                TempData["Message"] = message;
                return RedirectToAction(nameof(Index));

                ModelState.AddModelError(string.Empty, "Can't Create Employee Now ");

                return View(request);
            }
            catch (Exception ex)

            {

                if (_env.IsDevelopment())

                    ModelState.AddModelError(string.Empty, ex.Message);

                // Log Production
                _logger.LogError(ex.Message);
            }
            return View(model: request);
        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)

        {
            if (!id.HasValue) return BadRequest(); // 400
            var Employee = _EmployeeService.GetById(id: id.Value);

            if (Employee is null) return NotFound(); // 404

            return View(Employee);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)

        {
            if (!id.HasValue) return BadRequest(); // 400
            var Employee = _EmployeeService.GetById(id: id.Value);

            if (Employee is null) return NotFound(); // 404

            var employeeRequest = new EmployeeUpdateRequest
            {
               Id= Employee.Id,
               Address= Employee.Address,
               Age= Employee.Age,   
               Email= Employee.Email,
               HiringDate= Employee.HiringDate, 
               IsActive= Employee.IsActive,
               Name= Employee.Name, 
               PhoneNumber= Employee.PhoneNumber,   
               Salary= Employee.Salary,
               EmployeeType = Enum.Parse<EmployeeType>(Employee.EmployeeType), // string => Enum (Employee Type)
               Gender = Enum.Parse<Gender>(Employee.Gender)

            };
            return View();
        }
        [HttpPost]

        public IActionResult Edit([FromRoute] int id, EmployeeUpdateRequest request)
        {
            if (id != request.Id) return BadRequest();
            try
            {
                var result = _EmployeeService.Update(request);

                if (result > 0) return RedirectToAction(actionName: nameof(Index)); //*

                ModelState.AddModelError(string.Empty, "Can't Update Employee Now ");

                return View(request);
            }
            catch (Exception ex)
            {
                if (_env.IsDevelopment())
                    ModelState.AddModelError(string.Empty, ex.Message);
                // Log Production
                _logger.LogError(ex.Message);

            }
            return View(request);
        }

        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)

        {
            if (!id.HasValue) return BadRequest(); // 400
            var Employee = _EmployeeService.GetById(id: id.Value);

            if (Employee is null) return NotFound(); // 404

            return View(Employee);
        }
        [HttpPost, ActionName(name: "Delete")]

        public ActionResult ConfirmDelete(int? id)
        {

            if (!id.HasValue) return BadRequest(); //400
            try
            {

                var result = _EmployeeService.Delete(id.Value);
                // If Employee is Created RedirectToAction Index
                if (result) return RedirectToAction(nameof(Index));
                // else
                //ModelState.AddModelError(string.Empty, "Can't Create Employee Now");
                //return View(request);
                /// Send Data TO Index Action TO Return it to Index View

                return RedirectToAction(nameof(Index));

            }

            catch (Exception ex)
            {

                // Prod

                // Log Error

                // Return FM
                if (_env.IsProduction())
                {
                    _logger.LogError(message: ex.Message);
                    ModelState.AddModelError(string.Empty, "Can't Create Employee Now");

                }

                // Dev

                // Return Ex Details

                //ModelState.AddModelError(string.Empty, ex.Message);

                //return View(request);

                /// ALL

                /// Send Data To Index Action TO Return it to Index View

                return RedirectToAction(nameof(Index));

                #endregion



            }
        }
    }
}
