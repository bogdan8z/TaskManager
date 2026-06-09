using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;
public interface IJwtTokenGenerator
{
    string Generate(User user);
}
