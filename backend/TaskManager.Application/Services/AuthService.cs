using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services;

public class AuthService(IUserRepository repo, IJwtTokenGenerator jwt, IPasswordService password) : IAuthService
{
    private readonly IUserRepository _repo = repo;

    private readonly IPasswordService _password = password;
    private readonly IJwtTokenGenerator _jwt = jwt;

    public async Task<string> Register(string email, string password)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = _password.HashPassword(password)
        };

        await _repo.AddAsync(user);

        return _jwt.Generate(user);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _repo.GetByEmailAsync(email);
        if (user == null || !_password.VerifyPassword(password, user.PasswordHash))
        {
            throw new Exception("Invalid credentials");
        }

        return _jwt.Generate(user);
    }


}