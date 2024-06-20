using System.Data;
using System.Data.SqlClient;
using Humanizer.Localisation.TimeToClockNotation;
using Microsoft.AspNetCore.Identity;
using WebProfessorApplication.Models;

namespace WebProfessorApplication.Repository
{
    public class ProfessorRepository : IProfessorRepository
    {


        // first we need to get the connnection string 
        // declaring a private readonly field to store the connection string 

        private readonly string connectionString;

        public ProfessorRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("ConnectionStringMVC");
        }



        public void AddProfessor(Professor professor)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(connectionString))
                {

                    // first we need to create an instance of SqlCommand 

                    SqlCommand command = new SqlCommand("sp_AddProfessor", connection);
                    // then we need to specify the type 
                    command.CommandType = CommandType.StoredProcedure;

                    // then we need to pass the parameters exepected by the command 
                    command.Parameters.AddWithValue("@manager_id", professor.ManagerId);
                    command.Parameters.AddWithValue("@first_name", professor.FirstName);
                    command.Parameters.AddWithValue("@last_name", professor.LastName);
                    command.Parameters.AddWithValue("@dept_no", professor.DeptNo);
                    command.Parameters.AddWithValue("@salary", professor.Salary);
                    command.Parameters.AddWithValue("@joiningDate", professor.JoiningDate);
                    command.Parameters.AddWithValue("@dob", professor.Dob);
                    command.Parameters.AddWithValue("@gender", professor.Gender);


                    // then we need to open the connection and execute the Query 
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();


                }
            }
            catch (Exception)
            {
                // this block will catch any exception that maybe raised 
            }
        }



        // this is a method which is repsonsible to reach out to the Db and delete the record of the Professor corresponding to the Id 
        public void DeleteProfessor(int? id)
        {
            try
            {

                // first we need to create and instance of the SqlConnection 
                using (SqlConnection connnection = new SqlConnection(connectionString))
                {

                    // first we need to create an instance of SqlCommand 

                    SqlCommand command = new SqlCommand("sp_DeleteProfessor", connnection);
                    // then we need to specify the type 
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id",id);
                    
                    // then we need to pass the parameters exepected by the command 

                    // then we need to execute the procedure since there is no values to return we need to use execute nonQuery 
                    connnection.Open();
                    command.ExecuteNonQuery();

                    // we dont neeed to return any values because its just an update method
                }

            }
            catch (Exception) 
            { 
                // the control comes to this block if there was some exception raised 

            }   
        }

        public IEnumerable<Professor> GetAllDetails()
        {
            // then we need to Create a list 
            List<Professor> professorsList = new List<Professor>();
            try
            {
                using (SqlConnection connnection = new SqlConnection(connectionString))
                {

                    // first we need to create an instance of SqlCommand 

                    SqlCommand command = new SqlCommand("sp_GetAllProfessor", connnection);
                    // then we need to specify the type 
                    command.CommandType = CommandType.StoredProcedure;
                    connnection.Open();

                    // then we need to execute the command with the reader
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                       Professor professor = new Professor();
                        professor.ProfessorId = Convert.ToInt32(reader[0]);
                        professor.ManagerId = Convert.ToInt32(reader[1]);
                        professor.FirstName = reader[2].ToString();
                        professor.LastName = reader[3].ToString();
                        professor.DeptNo = Convert.ToInt32(reader[4]);
                        professor.Salary = Convert.ToDouble(reader[5]);
                        professor.JoiningDate = "" + reader[6];
                        professor.Dob = "" + reader[7];
                        professor.Gender = reader[8].ToString();

                        // we need to add it to the list 
                        professorsList.Add(professor);
            
                    }
                }

                return professorsList;
            }
            catch (Exception)
            {
                // this block will catch any exception that maybe raised 
                return professorsList = new List<Professor>();
            }
        }

        public Professor SearchByID(int? id)
        {
            Professor professor = new Professor();
            try
            {
                using (SqlConnection connnection = new SqlConnection(connectionString))
                {

                    // first we need to create an instance of SqlCommand 

                    SqlCommand command = new SqlCommand("sp_GetProfessorById", connnection);
                    // then we need to specify the type 
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", id);

                    connnection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    // then we need to pass the parameters exepected by the command 
                  
                    while (reader.Read())
                    {
                       
                        professor.ProfessorId = Convert.ToInt32(reader[0]);
                        professor.ManagerId = Convert.ToInt32(reader[1]);
                        professor.FirstName = reader[2].ToString();
                        professor.LastName = reader[3].ToString();
                        professor.DeptNo = Convert.ToInt32(reader[4]);
                        professor.Salary = Convert.ToDouble(reader[5]);
                        professor.JoiningDate = "" + reader[6];
                        professor.Dob = "" + reader[7];
                        professor.Gender = reader[8].ToString();


                    }
                    return professor;
                }
            }
            catch (Exception)
            {
                // this block will catch any exception that maybe raised 
                return professor;
            }
        }

        public void UpdateProfessorSalry(Professor professor)
        {
            try
            {
                using (SqlConnection connnection = new SqlConnection(connectionString))
                {

                    // first we need to create an instance of SqlCommand 

                    SqlCommand command = new SqlCommand("sp_UpdateProfessorSalary", connnection);
                    // then we need to specify the type 
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@id", professor.ProfessorId);
                    command.Parameters.AddWithValue("@salary", professor.Salary);
                    // then we need to pass the parameters exepected by the command 

                    // then we need to execute the procedure since there is no values to return we need to use execute nonQuery 
                    connnection.Open();
                    command.ExecuteNonQuery();

                    // we dont neeed to return any values because its just an update method
                }
            }
            catch (Exception)
            {
                // this block will catch any exception that maybe raised 
            }
        }
    }
}
