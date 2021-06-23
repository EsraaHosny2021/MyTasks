using EmployeeManagement.Models;
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Controllers
{
   
    public class HomeController : Controller
    {
        //readonly prevents assigning any random values to this repository
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        //controller here injects interface to make code more flexible and easilty testable
        //constructor injection
        public HomeController(IEmployeeRepository employeeRepository,
             IHostingEnvironment hostingEnvironment)
         {
             _employeeRepository = employeeRepository;
            this.hostingEnvironment = hostingEnvironment;


         }
        /*public HomeController()
        {
            _employeeRepository = new MockEmployeeRepository();

        }*/

        /* public string Index()
{
return "Hello from MVC";
}*/
        /* public JsonResult Index()
       {
           return Json(new { id=1,name="Esraa" });
       }*/

       
        public ViewResult Index()
        {
            //return _employeeRepository.GetEmployee(3).Name;
            var model = _employeeRepository.GetAllEmployees();
            return View(model);
                
        }
       
        public ViewResult Details(int? id)
        {
            //check if employee details exist or not
            Employee employee = _employeeRepository.GetEmployee(id.Value);
            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                //Employee = _employeeRepository.GetEmployee(id ?? 1),
                Employee = employee,
                PageTitle = "Employee Details"
            };
            return View(homeDetailsViewModel);
            //public string Details(int? id, string name)
            //return "id = " + id.Value.ToString() + "name = " + name;
            //Employee model = _employeeRepository.GetEmployee(1);
            //return Json(model);
            //Employee model = _employeeRepository.GetEmployee(1);
            //return new ObjectResult(model);
            //Employee model = _employeeRepository.GetEmployee(1);
            //return View(model);
            //Employee model = _employeeRepository.GetEmployee(1);
            //return View("Test",model);
            //absolute path
            //return View("MyViews/Test.cshtml");
            //return View("~/MyViews/Test.cshtml");
            //wrong line not working=> return View("Test/UpdateTest.cshtml");
            //relative path
            //return View("../Test/UpdateTest"); 
            //return View("../../MyViews/Test");
            /*ViewData["Employee1"] = model;
            ViewData["PageTitle1"] = "Employee Details";
            ViewBag.Employee2 = model;
            ViewBag.PageTitle2 = "Employee Details";
            */

            //return View(model);

        }

        
      
        public ViewResult TestView()
        {
            var model = _employeeRepository.GetAllEmployees();
            return View("~/Views/Home/Index.cshtml",model);
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
            //population of view model with employee data from database
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
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath,"images",model.ExistingPhotoPath);
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
                //image path
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                //image name
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                //issue in creating employee image then edit it with a used resource
                //model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                // }

            }

            return uniqueFileName;
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = ProcessUploadedFile(model);
                //create a new employee object
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
        /*
         * [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                string uniqueFileName = null;
                // if (model.Photos != null && model.Photos.Count > 0)
                if (model.Photos != null)
                {
                    //foreach (IFormFile photo in model.Photos) {
                    //    //image path
                    //    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    //    //image name
                    //    uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                    //    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    //    photo.CopyTo(new FileStream(filePath, FileMode.Create));
                       //image path
                       string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                   //image name
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photos.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                   model.Photos.CopyTo(new FileStream(filePath, FileMode.Create));
               // }

            }
                //create a new employee object
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
         * 
         */



        /*
         *  public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                Employee newEmployee = _employeeRepository.Add(employee);
                return View(newEmployee);
                //new { id = newEmployee.Id } is called anonymous object
            }//RedirectToAction("details", new { id = newEmployee.Id });
            return View();
        }
         */

    }
}
