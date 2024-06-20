using DemoEmployeeManagementRestApi.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace DemoEmployeeManagementRestApi.Repository
{
    public class LoginRepository : ILoginRepository
    {

        private readonly PropelDbContext _context;


        //Dependency Injection 
        public LoginRepository(PropelDbContext context)
        {
            _context = context;
        }
        public TblUser ValidateUser(string username, string password)
        {
            if(_context != null)
            {
                TblUser dbuser = _context.TblUsers.FirstOrDefault(em => em.UserName == username && em.UserPassword == password);

                 if(dbuser != null)
                {
                    return dbuser;
                }
            }

            return null;
        }
        
    }
}
