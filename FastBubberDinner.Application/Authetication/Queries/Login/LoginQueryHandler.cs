using System.Net;
using FastBubberDinner.Application.Authentication.Common;
using FastBubberDinner.Application.Common.Interfaces.Authentication;
using FastBubberDinner.Application.Common.Interfaces.Persistence;
using FastBubberDinner.Domain.Common.Errors;
using MediatR;

namespace FastBubberDinner.Application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Validate user exists asyncronously
        var user = await Task.Run(() => _userRepository.GetUserByEmail(query.Email) ?? throw new ServiceException("User was not found", status: (int)HttpStatusCode.NotFound));

        // Validate the password is correct
        if (user.Password != query.Password)
            throw new ServiceException("Password is not correct", status: (int)HttpStatusCode.BadRequest);

        // Create a JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // This return statement might change in the future to a normal one if some awaiting is done above
        return new AuthenticationResult(user, token);
    }
}