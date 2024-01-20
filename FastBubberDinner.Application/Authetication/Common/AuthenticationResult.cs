using FastBubberDinner.Domain.Entities;

namespace FastBubberDinner.Application.Authentication.Common;

public record AuthenticationResult
(
    User User,
    string Token
);