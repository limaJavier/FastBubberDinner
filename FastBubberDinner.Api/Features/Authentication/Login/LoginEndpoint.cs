using FastBubberDinner.Application.Services.Authentication;
using FastBubberDinner.Contracts.Authentication;
using FastEndpoints;

namespace FastBubberDinner.Api.Features.Authentication.Login;

public class LoginEndpoint : Endpoint<LoginRequest, AuthenticationResponse>
{
    private readonly IAuthenticationService _authenticationService;
    public LoginEndpoint(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    public override void Configure()
    {
        Post("/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authenticationResult = _authenticationService.Login(request.Email, request.Password);

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