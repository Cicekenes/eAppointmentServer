using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.CreateUser
{
    internal sealed class CreateUserCommandHandler(
        UserManager<AppUser> _userManager,
        IUserRoleRepository _userRoleRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(await _userManager.Users.AnyAsync(p => p.Email == request.Email))
            {
                return Result<string>.Failure("Email already exists");
            }
            if(await _userManager.Users.AnyAsync(p => p.UserName == request.UserName))
            {
                return Result<string>.Failure("UserName already exists");
            }

            AppUser user =_mapper.Map<AppUser>(request);
            IdentityResult result = await _userManager.CreateAsync(user,request.Password);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s=>s.Description).ToList());
            }
            if (request.RoleIds.Any())
            {
                List<AppUserRole> userRoles = new List<AppUserRole>();
                foreach (var roleId in request.RoleIds)
                {
                    AppUserRole userRole = new AppUserRole()
                    {
                        RoleId = roleId,
                        UserId = user.Id
                    };
                    userRoles.Add(userRole);
                }
                await _userRoleRepository.AddRangeAsync(userRoles, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return "User create is successful";
        }
    }
}
