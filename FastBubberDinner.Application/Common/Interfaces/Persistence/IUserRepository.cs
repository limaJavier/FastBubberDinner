using FastBubberDinner.Domain.Entities;

namespace FastBubberDinner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}