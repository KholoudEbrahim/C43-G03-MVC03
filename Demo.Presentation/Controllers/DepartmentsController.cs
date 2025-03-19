using Azure.Core;
using Demo.BLL.DataTransferObjects.Departments;
using Demo.BLL.Services;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Presentation.Controllers
{
    public class DepartmentsController(IDepartmentService departmentService,
        IWebHostEnvironment webHostEnvironment, ILogger<DepartmentsController> logger) : Controller
    {
        private readonly IDepartmentService _departmentService = departmentService;
        private readonly IWebHostEnvironment _env = webHostEnvironment;
        private readonly ILogger<DepartmentsController> _logger = logger;

        public IActionResult Index() //home page => All Departments
        {
            var departments = _departmentService.GetAll();
            return View(departments); // Send Data from Action To View
        }

        #region Create

        [HttpGet]
        public ActionResult Create() => View();

        [HttpPost]
        public ActionResult Create(DepartmentRequest request)
        {

            if (!ModelState.IsValid) return View(request); // Server Side Validation
            try
            {

                var result = _departmentService.Add(request);

                if (result > 0) return RedirectToAction(nameof(Index)); //*

                ModelState.AddModelError(string.Empty, "Can't Create Department Now ");

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
            var department = _departmentService.GetById(id: id.Value);

            if (department is null) return NotFound(); // 404

            return View(department);
        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)

        {
            if (!id.HasValue) return BadRequest(); // 400
            var department = _departmentService.GetById(id: id.Value);

            if (department is null) return NotFound(); // 404

            return View(department.ToRequest());
        }
        [HttpPost]

        public IActionResult Edit([FromRoute] int id, DepartmentUpdateRequest request)
        {
            if (id != request.Id) return BadRequest();
            try
            {
                var result = _departmentService.Update(request);

                if (result > 0) return RedirectToAction(actionName: nameof(Index)); //*

                ModelState.AddModelError(string.Empty, "Can't Update Department Now ");

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
        //[HttpGet]
        //public IActionResult Delete(int? id)

        //{
        //    if (!id.HasValue) return BadRequest(); // 400
        //    var department = _departmentService.GetById(id: id.Value);

        //    if (department is null) return NotFound(); // 404

        //    return View(department);
        //}
        [HttpPost, ActionName(name: "Delete")]

        public ActionResult ConfirmDelete(int? id)
        {

            if (!id.HasValue) return BadRequest(); //400
            try
            {

                var result = _departmentService.Delete(id.Value);
                // If Department is Created RedirectToAction Index
                if (result) return RedirectToAction(nameof(Index));
                // else
                //ModelState.AddModelError(string.Empty, "Can't Create Department Now");
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
                    ModelState.AddModelError(string.Empty, "Can't Create Department Now");

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
