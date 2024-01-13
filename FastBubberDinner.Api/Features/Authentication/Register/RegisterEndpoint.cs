using FastBubberDinner.Application.Services.Authentication;
using FastBubberDinner.Contracts.Authentication;
using FastEndpoints;
namespace FastBubberDinner.Api.Features.Authentication.Register;

public class RegisterEndpoint : Endpoint<RegisterRequest, AuthenticationResponse>
{
    private readonly IAuthenticationService _authenticationService;
    public RegisterEndpoint(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    public override void Configure()
    {
        Post("/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var authenticationResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);

        var response = new AuthenticationResponse(
            authenticationResult.id,
            authenticationResult.FirstName,
            authenticationResult.LastName,
            authenticationResult.Email,
            authenticationResult.Token
        );

        await SendAsync(response);
    }
}
