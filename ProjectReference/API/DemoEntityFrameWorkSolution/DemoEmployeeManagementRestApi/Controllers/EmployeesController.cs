using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoEmployeeManagementRestApi.Model;
using DemoEmployeeManagementRestApi.Repository;
using DemoEmployeeManagementRestApi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace DemoEmployeeManagementRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [AllowAnonymous]
   // [Authorize(AuthenticationSchemes = "Bearer")]
   // [EnableCors("AllowAllOrgin")]
    
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeesController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblEmployee>>> GetTblEmployees()
        {
          return await _repository.GetTblEmployee();
        }



        
        // GET: api/Employees/5
        [HttpGet]
        [Route("vm")]
        public async Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetTblEmployee()
        {
         return await _repository.GetViewModelEmployee();
        }


        // POST : api/Employees
        [HttpPost]
        public async Task<ActionResult<TblEmployee>> PostTblEmployeesReturnRecord(TblEmployee tblEmployee)
        {
          if(ModelState.IsValid)
            {
                // Insert a new Record and return as an object named Employee

                var employee = await _repository.PostTblEmployeesReturnRecord(tblEmployee);

                if(employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound();
                }
            }
            
          return BadRequest();
        }

        [HttpPost]
        [Route("id")]
        public async Task<int> PostTbleEmployees(TblEmployee tblEmployee)
        {
            if (ModelState.IsValid)
            {
                // Insert a new Record and return as an object named Employee

                var employeeId = await _repository.PostTbleEmployees(tblEmployee);

                if (employeeId !=null)
                {
                    return employeeId;
                }
                else
                {
                    return 0;
                }
            }

            return 0;
        }


        [HttpGet("{id}")]
        [EnableCors("AllowAllOrgin")]
        public async Task<ActionResult<TblEmployee>> GetTblEmployees(int id)
        {
            var tablEmployee = await _repository.GetEmployeeById(id);
            if(tablEmployee == null)
            {
                return NotFound();
            }

            return tablEmployee;
        }




        //-----------------------------------------------------------------------------------------------------------------------------

        [HttpGet("v1/{department_id}")]
        #region Get Department List using Stored Procedure 
        public async Task<IActionResult> GetEmployeeByDepartmentStoredProcedure(int department_id)
        {
            var employees = await _repository.GetEmployeeByDepartmentStoredProcedure(department_id);

            if (employees == null || !employees.Any())
            {
                return NotFound();
            }

            return Ok(employees);
        }

        #endregion

        [HttpPut]
        public async Task<IActionResult> UpdateEmployeeByStoredProcedure(TblEmployee employeee)
        {
            var employees = await _repository.UpdateEmployeeByStoredProcedure(employeee);

            if (employees == null )
            {
                return NotFound();
            }

            return Ok(employees);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnEmployee(int id)
        {
            var employees = await _repository.DeleteEmployee(id);

            if (employees == null)
            {
                return NotFound();
            }

            return Ok(employees);
        }

        //-----------------------------------------------------------------------------------------------------------------------------
        [HttpGet("Department/vw")]
        public async Task<ActionResult<IEnumerable<vw_Department>>> GetDetailsOfAllDepartmentForDropDown()
        {
            var department = await _repository.GetDetailsOfAllDepartmentForDropDown();

            if (department != null)
            {
                return Ok(department);
            }
            else
            {
                return NotFound();
            }
        }


     


        /*
        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblEmployee(int id, TblEmployee tblEmployee)
        {
            if (id != tblEmployee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(tblEmployee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblEmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblEmployee(int id)
        {
            if (_context.TblEmployees == null)
            {
                return NotFound();
            }
            var tblEmployee = await _context.TblEmployees.FindAsync(id);
            if (tblEmployee == null)
            {
                return NotFound();
            }

            _context.TblEmployees.Remove(tblEmployee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblEmployeeExists(int id)
        {
            return (_context.TblEmployees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }

        */
    }
}
