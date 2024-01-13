using FastBubberDinner.Application.Common.Interfaces.Persistence;
using FastBubberDinner.Domain.Entities;

namespace FastBubberDinner.Infrastructure.Persistence;

public class InMemoryUserRepository : IUserRepository
{
    private static List<User> _users = new List<User>();
    public void Add(User user)
    {
        _users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        var user = _users.FirstOrDefault(u => u.Email == email);
        return user;
    }
}