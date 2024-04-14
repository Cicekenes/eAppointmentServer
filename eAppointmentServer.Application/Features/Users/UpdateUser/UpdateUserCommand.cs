using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string UserName,
        List<Guid> RoleIds) : IRequest<Result<string>>;

}
