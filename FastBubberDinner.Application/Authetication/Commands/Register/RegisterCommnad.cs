using FastBubberDinner.Application.Authentication.Common;
using MediatR;

namespace FastBubberDinner.Application.Authentication.Commands.Register;

public record RegisterCommand
(
    string FirstName,
    string LastName,
    string Email,
    string Password
) : IRequest<AuthenticationResult>;