using EmployeeManagment.Models;
using EmployeeManagment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace EmployeeManagment.Controllers
{

    [Authorize]
    //[Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILogger _logger;
        public HomeController(IEmployeeRepository employeeRepository, IHostingEnvironment hostingEnvironment, ILogger<HomeController> logger)
        {
            this._employeeRepository = employeeRepository;
            this._hostingEnvironment = hostingEnvironment;
            this._logger = logger;
        }
        //public JsonResult Index()
        //{
        //    //return "Hello from MVC";
        //    return Json(new {id=1,name="Baljeet Singh Kochhar" });
        //}

        //public string Index()
        //{
        //    //return "Hello from MVC";
        //    return _employeeRepository.GetEmployee(1).Name;
        //}

        //[Route("~/Home")]
        //[Route("~/")]
        [AllowAnonymous]
        public ViewResult Index()
        {
            //return "Hello from MVC";

            var model = _employeeRepository.GetAllEmployee();

            return View(model);
        }

        //public JsonResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    //return "Hello from MVC";
        //    return Json(model);
        //}

        //public ObjectResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(1);
        //    //return "Hello from MVC";
        //    return new ObjectResult(model);
        //}

        //[Route("{id?}")]
        [AllowAnonymous]
        public ViewResult Details( int? id)
        {

            //throw new Exception("Error in details view");
            //Employee model = _employeeRepository.GetEmployee(1);
            //return "Hello from MVC";

            //    Example of View Data
            //ViewData["EmployeeData"]=model;
            //ViewData["PageTitle"] = "Employee Details";


            //    Example of View Bag
            //ViewBag.EmployeeData = model;
            //ViewBag.PageTitle = "Employee Details";



            //    Example of Strongly Type
            //ViewBag.PageTitle = "Employee Details";
            _logger.LogTrace("Trace Log");
            _logger.LogDebug("Debug Log");
            _logger.LogInformation("Information Log");
            _logger.LogWarning("Warning Log");
            _logger.LogError("Error Log");
            _logger.LogCritical("Critical Log");

            Employee employee = _employeeRepository.GetEmployee(id.Value);

            if(employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }

            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);
            //return View("Test");
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }


        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);


                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName
                };
                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();

        }

        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if (model.Photo != null)
                {
                    if(model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ExistingPhotoPath);   
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
                
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }

            return View();

        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolders = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolders, uniqueFileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }                
            }
            return uniqueFileName;
        }
    }
}
