using EmployeeManagementDemo.Models;

namespace EmployeeManagementDemo.Repository
{
    public interface IEmployeeRepository
    {

        // Display all Employees 
        IEnumerable<Employee> GetAllEmployees();  // -------SELECT----------(httpVerb) GET
        //so the return type of this method will be a list/ Enumerable of the Employee Object ie there will be multiple Employees

        // Add new Employees
        void AddEmployee(Employee employee);//--------------INSERT----------(httpVerb) POST


        // Update an Employee 
        void UpdateEmployee(Employee employee);//-----------UPDATE----------(httpVerb) POST

        // Delete an Employee
        void DeleteEmployee(int? empId);//------------------DELETE-----------(httpVerb) POST
        // here ? mark refers to Nullable

        // Serach By Id
        Employee GetEmployeeById(int? empId);//-------------SELECT BY ID------(httpVerb) GET


        /* Another name for Http Code is Http Methods so the Success code of the Http method is 200 and the Code for page not found is 404*/
    }
}
