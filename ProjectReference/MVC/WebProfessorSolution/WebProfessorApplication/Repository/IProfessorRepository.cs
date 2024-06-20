using WebProfessorApplication.Models;

namespace WebProfessorApplication.Repository
{
    public interface IProfessorRepository
    {

        // here we need to declare the method that belong to the 
        // professor and the inplementation of this class is
        // responsible to reach out to the  db and interact 


        void AddProfessor(Professor professor);

        Professor SearchByID(int? id);

        IEnumerable<Professor> GetAllDetails();

        void UpdateProfessorSalry(Professor professor);

        void DeleteProfessor(int? id);


    }
}
