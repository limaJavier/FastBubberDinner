using FastBubberDinner.Application.Authentication.Commands.Register;
using FastBubberDinner.Contracts.Authentication;
using FastEndpoints;
using MediatR;
namespace FastBubberDinner.Api.Features.Authentication.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest, AuthenticationResponse>
{
    private readonly ISender _mediator;
    public RegisterEndpoint(ISender mediator)
    {
        _mediator = mediator;
    }
    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var registerCommand = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password
        );
        var authenticationResult = await _mediator.Send(registerCommand);

        var response = new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token
        );

        await SendAsync(response);
    }
}
