using System.ComponentModel.DataAnnotations;

// Development steps 
// 1. create a class with Data Annotation 
/* Create repository layer 
    - IEmployeerepository 
         - searchAll , Insert , Update , Delete , Search by Id 
    - EmployeeRepository 
            - implementation of the IEmployeeRepository 
3. use of dependency injection in the EmployeeController to call IEmployeeRepository

4. Create Views - Index.cshtml for list all employees 
                 - Create.cshtml for adding a new Employee

5. Register midddleware services in the programme.cs
          - IEmployeeRepository and EmployeeRepository 


*/

namespace EmployeeManagementDemo.Models
{

    // so inorder to make the class a virtual table we need to specify the key annotation  as the primary key of the class and we use the annotation
    // [Required] to speccify the not null fields of the physical table in the virtual table 

    public class Employee
    {

       // this is annotation 
        [Key]
        public int Id { get; set; }

        // anotations are specified using Square brackets[] placed before the elements they are annotating. They can be applied to classes , methods , properties , 
        // or parameters to modify their behaviour or provide additional information 

        // the Key attribute is used to specify that a property is a primary key in the database table corresponding to the model classes

        // the required attribute is used to specify that a property must have a non- null value during model validation

        //some of the important Data Annotations are 
        // string Length : Specifies the Maximum and Minimum length of a string property 
        // Range : Specifies the numeric range constraint for the property.
        // RegularExpression : Specifies that a property value must match a specifiesd regular expression pattern 
        //EmailAddress : Specifies that a property value must be a valid EmailAddress
        //DisplayFormat : Specifies the display format for the property 
        //MaXLength : Specifies the Maximum length of a stringvv



        [Required]                 // so the annotation specifies that the field are required whenever we are creating an object 
        public string Name { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public int Salary { get; set; }
    }
}
