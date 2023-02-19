using Store_API.Models;

namespace Store_API.Repositories;

public interface IAdminService 
{
    Admin CreateAdmin(Admin admin);
    string SignIn(string email, string password);
}