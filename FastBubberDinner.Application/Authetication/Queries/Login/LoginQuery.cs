using FastBubberDinner.Application.Authentication.Common;
using MediatR;

namespace FastBubberDinner.Application.Authentication.Queries.Login;

public record LoginQuery
(
    string Email,
    string Password
) : IRequest<AuthenticationResult>;