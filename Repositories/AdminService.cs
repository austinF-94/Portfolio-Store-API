using Store_API.Models;
using Store_API.Migrations;

namespace Store_API.Repositories;

public class AdminService : IAdminService
{
    private static ProductDbContext _context;

    public AdminService(ProductDbContext context) 
    {
        _context = context;
    }

    public Admin CreateAdmin(Admin admin)
    {
        throw new System.NotImplementedException();
    }


    public string SignIn(string email, string password)
    {
        throw new System.NotImplementedException();
    }
}