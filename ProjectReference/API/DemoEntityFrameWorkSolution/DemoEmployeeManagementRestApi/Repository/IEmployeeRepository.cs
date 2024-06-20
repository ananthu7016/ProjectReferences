using DemoEmployeeManagementRestApi.Model;
using DemoEmployeeManagementRestApi.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DemoEmployeeManagementRestApi.Repository
{
    public interface IEmployeeRepository
    {


        // Search all Employee ie get all employees
        Task<ActionResult<IEnumerable<TblEmployee>>> GetTblEmployee();
                                                                                                         /*
                                                                                                          Task is a Type that represent an asynchronous 
                                                                                                          operation and can return a result.
                                                                                                                
                                                                                                          Asynchronous Operations : An Operation that can 
                                                                                                          Execute Concurrently with other Operations, allowing
                                                                                                          the calling thread to continue executing other tasks 
                                                                                                          waiting for the asynchronous operation to complete
                                                                                                          
                                                                                                          Why we use Task ? 
                                                                                                          Concurrency : allowing multiple operations to execute 
                                                                                                          concurrently, improving application performance and 
                                                                                                          responsiveness.
                                                                                                          Non Blocking : Enables Asynchronous programming , where 
                                                                                                          the calling thread is not blocked while waiting for the 
                                                                                                          operation to complete. This helps prevent UI freezes and 
                                                                                                          enhances scalability.
                                                                                                          */
        


        // get all Using ViewModel 
           Task<ActionResult<IEnumerable<EmpDeptViewModel>>> GetViewModelEmployee();

        // get all Employee Based on Id 
        Task<ActionResult<TblEmployee>> GetEmployeeById(int id);

        // insert an Employee - Return ID

        Task<int> PostTbleEmployees(TblEmployee tblEmployee);



        // Insert an Employee - return Employee Record
        Task<ActionResult<TblEmployee>> PostTblEmployeesReturnRecord(TblEmployee tblEmployee);


        // Update an employee With Id and Employee
        Task<ActionResult<TblEmployee>> PutTblEmployees(int id,TblEmployee tblEmployee);
        // Update an Employee With Employee Only 


        // Delete an Employee

        // Call Stored Procedure - GetEmployeesByDepartment
        Task<IEnumerable<TblEmployee>> GetEmployeeByDepartmentStoredProcedure(int department_id);

        // call Stored Procedure -- Insert an Employee -- Return Employee Record 

        Task<ActionResult<IEnumerable<TblEmployee>>> AddEmployeeByStoredProcedure(TblEmployee employee);

        // Call Stored Procedure --- Update and employee -- Return Employee Record 
        Task<ActionResult<IEnumerable<TblEmployee>>> UpdateEmployeeByStoredProcedure(TblEmployee employee);


        Task<ActionResult<IEnumerable<vw_Department>>> GetDetailsOfAllDepartmentForDropDown();


        Task<ActionResult<TblEmployee>>DeleteEmployee(int id);
    }
}
