using FastBubberDinner.Domain.Entities;

namespace FastBubberDinner.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}