using FastBubberDinner.Application.Authentication.Queries.Login;
using FastBubberDinner.Contracts.Authentication;
using FastEndpoints;
using MediatR;

namespace FastBubberDinner.Api.Features.Authentication.Login;

public class LoginEndpoint : Endpoint<LoginRequest, AuthenticationResponse>
{
    private readonly ISender _mediator;
    public LoginEndpoint(ISender mediator)
    {
        _mediator = mediator;
    }
    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var loginQuery = new LoginQuery(
            request.Email,
            request.Password);

        var authenticationResult = await _mediator.Send(loginQuery);

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