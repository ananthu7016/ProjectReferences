using Microsoft.AspNetCore.Mvc;
using WebProfessorApplication.Models;
using WebProfessorApplication.Repository;

namespace WebProfessorApplication.Controllers
{
    public class ProfessorController : Controller
    {

        // here we need to crete an instance of the repository layer throught constructor injection 
        private readonly IProfessorRepository _professorRepository;

        public ProfessorController(IProfessorRepository professorRepository)
        {
            _professorRepository = professorRepository;
        }



        // this is the action method to get the list of All professors and this action method returns a list of all the details of the 
        // professor along with the Edit/Delete options
        public IActionResult Index()
        {
             List<Professor> professor = _professorRepository.GetAllDetails().ToList();
            return View(professor);
        }


        // this action method is for Creating a new professor and it is a Http get for the create of the Employees
        [HttpGet]
        public IActionResult Create()
        {
          
            return View();
        }


        // then we need to create an Htpp post method for Adding of a professor 
        [HttpPost]
        public IActionResult Create(Professor professor)
        {
            try
            {

                // this action method will be invoked once the user has entered all the details and press the Create button and this method will be 
                // invoked only if the validation is successfull 
                if (ModelState.IsValid)
                {
                    // ie if all the validations are true and there was no problem 
                    _professorRepository.AddProfessor(professor);
                    // this will add the professor to the Db 
                    // then we need to return back to the Index 
                    return RedirectToAction("Index");
                    // this will redired back to the Index view 
                }
                else
                {
                    return View();
                    // this 
                }

            }
            catch (Exception ex)
            {
                // this block will catch any exception that maybe raised and since this was as exception we need to show a view 
                return View(ex);
            }

            
        }


        // this is a action method to get the details of the person to edit ie this action method returns a view which contain all the details of the 
        // person to edit and the user can edit the details he wanted 
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            try
            {
                // so we need to create an instance of the Professor to store the detail of the professor with the choosen Id 
                Professor professor = _professorRepository.SearchByID(id);
                // this method will return the object of the selected Employee to Edit 

                // then we need to pass this object to the View to display the details 
                return View(professor);
            }
            catch(Exception ex)
            {
                // this block will catch any execption that maybe raised 
                // and if any exception is reaised we need to show that in a view 
                return View(ex);
            }
           
        }



        // this is the httpPost Action method of the Update once the user has changed the details of the Professor and then click the edit button this 
        // method will be invoked and this will save the details to the Db
        [HttpPost]
        public IActionResult Edit(int? id ,Professor professor)
        {
            try
            {
                // first we need to makesure all the ModelStates are Valid or not 
                if (ModelState.IsValid)
                {
                    // so if the model states are valid the control will eneter in this block 
                    // then we need to use the instance of the repository to get access to the methods in the repository 
                    _professorRepository.UpdateProfessorSalry(professor);
                    return RedirectToAction("Index");
                    // this will redired to the Index page if the salary of the person is succesfully edited 
                }
                else
                {
                    return View();

                }

            }
            catch (Exception ex)
            {
                // this block will catch any exception that maybe raised and return a view 
                return View(ex);
            }
        }


        // this is the action method that return a view that contain the details of the professor to be deleted and this is the get Method of the 
        // delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {

            // this method recieves an id of the person to be deleted and based on the recieved id we neeed to show the user the details of the 
            // professor he wanted to delete 
            try
            {

                // first we need to create an instance of the professor to store the details of the professor from the method 
                Professor professor = _professorRepository.SearchByID(id);
                // this method will return an object of the selected employee we neeed to 
                // pass this object to the view to be displayed 
                return View(professor);
                       // this will return a view of the professor with all the details of the selected professor 

            }
            catch(Exception ex)
            {
                //this block will catch any exception that may be raised and this will return a view
                return View();
            }
        }


        // this method is the HttpPost of the Delete and this action method will be invoked once the user has pressed the delete Button 
        [HttpPost]
        public IActionResult Delete(int id, Professor professor)
        {
            try
            {
                // first we need to check if the ModelState is Valid or not 
                if (!ModelState.IsValid)
                {
                    // so the control comes to here if the model state is Valid
                    // then to use the instace of the repository layer to get access to the methods
                    _professorRepository.DeleteProfessor(id);
                    return RedirectToAction("Index");
                    // so if the deletion was done we will be redirected to the List 
                }

            }
            catch(Exception ex)
            {
                // the control will come to this block when there is some exception that maybe Raised
                return View();
            }

            return View();
        }


       

    }
}
