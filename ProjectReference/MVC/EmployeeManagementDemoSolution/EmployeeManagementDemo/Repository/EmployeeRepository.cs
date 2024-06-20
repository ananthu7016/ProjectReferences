using System.Data.SqlClient;
using EmployeeManagementDemo.Models;

namespace EmployeeManagementDemo.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        // connection string 
        private readonly string connectionString;
        
        public EmployeeRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ConnectionStringMVC");
        }

        // so here we are getting the connecction string using the constructor injection 
       
        public void AddEmployee(Employee employee)
        {


            // so we are creating an Instance for the SqlConnection 
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_AddEmployees",connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                // then we need to pass the Parameters to the stored procedure 

                command.Parameters.AddWithValue("@Name", employee.Name);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@Designation", employee.Designation);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
  
            }
        }

        public void DeleteEmployee(int? empId)
        {
            try
            {
                // so we are creating an Instance for the SqlConnection 
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_DeleteEmployees", connection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // then we need to pass the Parameters to the stored procedure 
                    command.Parameters.AddWithValue("@Id", empId);
                 

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }

            }
            catch (Exception ex)
            {
                // this block will catch any exception that many be raised 
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            try
            {
                // first we need to create a list 
                List<Employee> employeeList = new List<Employee>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand("sp_GetAllEmployees", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    // then we need to open the connection 
                    connection.Open();


                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee();

                        employee.Id = Convert.ToInt32(reader["Id"].ToString());
                        employee.Name = (reader["Name"].ToString());
                        employee.Gender = (reader["Gender"].ToString());
                        employee.Designation = (reader["Designation"].ToString());
                        employee.Salary = Convert.ToInt32(reader["Salary"].ToString());
                        employeeList.Add(employee);
                    }
                    // so here we are creating an object for the Employee with the Data from the DataBase and then adding the object to the 
                    // the list and then Returning the list to the caller 

                }
                return employeeList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Employee GetEmployeeById(int? empId)
        {
            Employee employee = new Employee();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("sp_GetEmployeesById", connection);

                command.CommandType = System.Data.CommandType.StoredProcedure;

                // then we need to pass the Parameters to the stored procedure 
                command.Parameters.AddWithValue("@Id", empId);


                connection.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                 

                    employee.Id = Convert.ToInt32(reader["Id"].ToString());
                    employee.Name = (reader["Name"].ToString());
                    employee.Gender = (reader["Gender"].ToString());
                    employee.Designation = (reader["Designation"].ToString());
                    employee.Salary = Convert.ToInt32(reader["Salary"].ToString());
  
                }
             
                connection.Close();

            }

            return employee;


        }

        public void UpdateEmployee(Employee employee)
        {
            try
            {
                
             // so we are creating an Instance for the SqlConnection 
            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("sp_UpdateEmployees", connection);

                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // then we need to pass the Parameters to the stored procedure 
                    command.Parameters.AddWithValue("@Id", employee.Id);
                    command.Parameters.AddWithValue("@Name", employee.Name);
                    command.Parameters.AddWithValue("@Gender", employee.Gender);
                    command.Parameters.AddWithValue("@Salary", employee.Salary);
                    command.Parameters.AddWithValue("@Designation", employee.Designation);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();

                }

            }
            catch (Exception ex)
            {
               // exception will be catched by this block 
            }
        }


        

    }
}
