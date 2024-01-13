using FastBubberDinner.Domain.Entities;

namespace FastBubberDinner.Application.Services.Authentication;

public record AuthenticationResult
(
    User User,
    string Token
);