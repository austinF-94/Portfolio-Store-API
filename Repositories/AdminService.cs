using Store_API.Models;
using Store_API.Migrations;
using bcrypt = BCrypt.Net.BCrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Store_API.Repositories;

public class AdminService : IAdminService
{
    private static ProductDbContext _context;
    private static IConfiguration _config;

    public AdminService(ProductDbContext context, IConfiguration config) 
    {
        _context = context;
        _config = config;
    }

    public AdminService(ProductDbContext context) 
    {
        _context = context;
    }

    public Admin CreateAdmin(Admin admin)
    {
        var passwordHash = bcrypt.HashPassword(admin.Password);
        admin.Password = passwordHash;        
        _context.Add(admin);
        _context.SaveChanges();
        return admin;
    }

    public string SignIn(string email, string password)
    {
        var admin = _context.Admin.SingleOrDefault(x => x.Email == email);
        var verified = false;

        if (admin != null) {
            verified = bcrypt.Verify(password, admin.Password);
        }

        if (admin == null || !verified)
        {
            return String.Empty;
        }
        
        return BuildToken(admin);    }

    private string BuildToken(Admin admin) 
    {
    var secret = _config.GetValue<String>("TokenSecret");
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    
    // Create Signature using secret signing key
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

    // Create claims to add to JWT payload
    var claims = new Claim[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, admin.AdminId.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, admin.Email ?? ""),
    };

    // Create token
    var jwt = new JwtSecurityToken(
        claims: claims,
        expires: DateTime.Now.AddMinutes(5),
        signingCredentials: signingCredentials);
    
    // Encode token
    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

    return encodedJwt;
    }
}