using System.Net;
using FastBubberDinner.Application.Common.Interfaces.Authentication;
using FastBubberDinner.Application.Common.Interfaces.Persistence;
using FastBubberDinner.Domain.Common.Errors;
using FastBubberDinner.Domain.Entities;

namespace FastBubberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        // Check if user already exists
        if (_userRepository.GetUserByEmail(email) is not null)
            throw new ServiceException("Email is already registered", status: (int)HttpStatusCode.BadRequest);

        // Create user (generate unique Id)
        var user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };

        _userRepository.Add(user);

        // Create Jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }

    public AuthenticationResult Login(string email, string password)
    {
        // Validate user exists
        var user = _userRepository.GetUserByEmail(email) ?? throw new ServiceException("User was not found", status: (int)HttpStatusCode.NotFound);

        // Validate the password is correct
        if (user.Password != password)
            throw new ServiceException("Password is not correct", status: (int)HttpStatusCode.BadRequest);

        // Create a JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}
