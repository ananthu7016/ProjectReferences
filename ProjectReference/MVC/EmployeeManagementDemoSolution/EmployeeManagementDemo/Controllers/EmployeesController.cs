using EmployeeManagementDemo.Models;
using EmployeeManagementDemo.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementDemo.Controllers
{

    // this class is inherited from the controller class 
    public class EmployeesController : Controller
    {

        // the flow is 
        // controller -> Service -> Repository 


        // we are using the constructor injection to get the all the methods of the EmployeeRepository
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        [HttpGet]      // this is a annnotation to show that the action verb is this  
        public IActionResult Index()   // The end point is Employees/Index
        {
            // Data 
            List<Employee> employees = _employeeRepository.GetAllEmployees().ToList();   
            return View(employees);

        }

        public IActionResult Create()   // End point is Employeees/Create
        {

            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)       // End point is Employees/Create 
        {

            try
            {
                if (ModelState.IsValid)  
                {
                    /*
                      Model States 
                     in ASP.NETCORE MVC , model state is a property of the controller base class that represent the state of the model binding process and
                     validation errors associated with the current request. It is an instance of the ModelStateDictionary class 


                    ModelBinding : When a request is recieveed by the controller action method , ASP.NET CORE MVC Attempts to bind the incomming request 
                    data to action menthod parameters or model properties. During this process , if any error occurs, such as missing or invalid data, 
                    these errors are captured and stored in the ModelState dictionary 

                    Validations : ASP.NET CORE MVC provides build in support for the model Validation you can apply validation attributes (such as Required,
                    MaxLength, Range) to model properties to define validation rules , when model binding occurs , these validation rules are evaluated 
                    and any validation errors are added to modelState Dictionary 
                     
                    */

                    _employeeRepository.AddEmployee(employee);

                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {
                return View();
            }
           return View();
        }


        #region 4_ Employee/Edit/4 -- UI with Data --Get 

        public IActionResult Edit(int? id)
        {
            //Search by id and get the EmployeeData 
           Employee employee = _employeeRepository.GetEmployeeById(id);

            // then we need to check if the employee is null or Id is null or not 
            if(employee == null|| id == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        #endregion

        #region 5_ Employee/Edit/4 -- UI with Data --Post 

        /* Is an Attribute coomonly used in ASP.NET Application to prevent Cross-site Request Forgery(CSRF) Attacks 
         CSRF attack occur when a malicious website tricks the user browser into making a unintentede request to diffrent site where the user is authendicated
        this could result in actions being taken on behalf of the user without their consent
         */

        [HttpPost]
        [ValidateAntiForgeryToken] // so this will prevent unautorized Access 
        public IActionResult Edit(int? id,Employee employee) 
        {

            try
            {
                if (ModelState.IsValid)
                {
                    _employeeRepository.UpdateEmployee(employee);

                    return RedirectToAction("Index");
                    // 
                }
            }
            catch (Exception ex)
            {
                return View();
            }
            return View();

        }


        #endregion



        #region Employee/Delete [httpGet]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            Employee employee = _employeeRepository.GetEmployeeById(id);
            
            if(employee == null || id == null)
            {
                return NotFound();
            }
            return View(employee);
        }
        #endregion


        #region Employee/Delete [HttpPost]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id,Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _employeeRepository.DeleteEmployee(id);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                return View();
            }

            return View();
        }

        #endregion


    }
}
