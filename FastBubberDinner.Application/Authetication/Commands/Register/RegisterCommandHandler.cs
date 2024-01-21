using System.Net;
using FastBubberDinner.Application.Authentication.Common;
using FastBubberDinner.Application.Common.Interfaces.Authentication;
using FastBubberDinner.Application.Common.Interfaces.Persistence;
using FastBubberDinner.Domain.Common.Errors;
using FastBubberDinner.Domain.Entities;
using MediatR;

namespace FastBubberDinner.Application.Authentication.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    
    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        // Check if user already exists asyncronously
        if (await Task.Run(() => _userRepository.GetUserByEmail(command.Email)) is not null)
            throw new ServiceException("Email is already registered", status: (int)HttpStatusCode.BadRequest);

        // Create user (generate unique Id)
        var user = new User()
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        await Task.Run(() => _userRepository.Add(user));

        // Create Jwt token
        var token = _jwtTokenGenerator.GenerateToken(user);

        // This return statement might change in the future to a normal one if some awaiting is done above
        return new AuthenticationResult(user, token);
    }
}
