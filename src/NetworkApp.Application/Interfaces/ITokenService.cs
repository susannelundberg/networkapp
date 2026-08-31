using NetworkApp.Domain;
using NetworkApp.Domain.Entities;

namespace NetworkApp.Application;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}
