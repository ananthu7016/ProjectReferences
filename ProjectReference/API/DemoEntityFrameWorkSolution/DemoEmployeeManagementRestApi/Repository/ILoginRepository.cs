using DemoEmployeeManagementRestApi.Model;

namespace DemoEmployeeManagementRestApi.Repository
{
    public interface ILoginRepository
    {

        TblUser ValidateUser(string username, string password);
    }
}
