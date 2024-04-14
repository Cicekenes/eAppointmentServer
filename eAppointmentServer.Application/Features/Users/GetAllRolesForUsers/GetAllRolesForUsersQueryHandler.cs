using eAppointmentServer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.GetAllRolesForUsers
{
    internal sealed class GetAllRolesForUsersQueryHandler(RoleManager<AppRole> _roleManager) : IRequestHandler<GetAllRolesForUsersQueries, Result<List<AppRole>>>
    {
        public async Task<Result<List<AppRole>>> Handle(GetAllRolesForUsersQueries request, CancellationToken cancellationToken)
        {
            List<AppRole> roles = await _roleManager.Roles.OrderBy(p=>p.Name).ToListAsync(cancellationToken);
            return roles;
        }
    }
}
