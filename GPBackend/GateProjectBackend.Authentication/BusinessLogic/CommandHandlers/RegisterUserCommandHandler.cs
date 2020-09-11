using GateProjectBackend.Authentication.BusinessLogic.CommandHandlers.Commands;
using GateProjectBackend.Authentication.BusinessLogic.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GateProjectBackend.Authentication.BusinessLogic.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
    {
        public RegisterUserCommandHandler()
        {

        }

        public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            //var user = await _usersRepository.CreateUser(request.FirstName, request.LastName, request.Email, request.Password);
            //var userResponse = Convert(user);
            return null;
        }

        //private string CreateHashedPassword(string password)
        //{

        //}
    }
}
