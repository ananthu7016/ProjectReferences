using System.ComponentModel.DataAnnotations;

namespace WebProfessorApplication.Models
{
    public class Professor
    {

        // this is the model class for the professor here we need to 
        // define the properties of the professor 

        [Key]
        public int ProfessorId { get; set; }

        [Required]
        public int ManagerId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int DeptNo { get; set; }

        [Required]
        public double Salary {  get; set; }

        [Required]
        public string JoiningDate { get; set; }

        [Required]
        public string Dob { get; set; }

        [Required]
        public string Gender { get; set; }

    }
}
