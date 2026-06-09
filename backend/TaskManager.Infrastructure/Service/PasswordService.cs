using Microsoft.AspNetCore.Identity;
using TaskManager.Application.Interfaces;

namespace TaskManager.Infrastructure.Service;

public class PasswordService : IPasswordService
{
    public string HashPassword(string password)
    {
        var ph = new PasswordHasher<object>();

        return ph.HashPassword(null!, password);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var ph = new PasswordHasher<object>();
        return ph.VerifyHashedPassword(null!, passwordHash, password)
            == PasswordVerificationResult.Success;
    }
}
