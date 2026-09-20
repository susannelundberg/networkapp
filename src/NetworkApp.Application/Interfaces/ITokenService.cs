using NetworkApp.Domain;
using NetworkApp.Domain.Entities;

namespace NetworkApp.Application.Interfaces;

public interface ITokenService
{
    Task<string> CreateToken(User user);
}
