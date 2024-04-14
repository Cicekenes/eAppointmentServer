using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Users.UpdateUser
{
    internal sealed class UpdateUserCommandHandler(
        UserManager<AppUser> _userManager,
        IUserRoleRepository _userRoleRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IRequestHandler<UpdateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            AppUser? user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user is null)
            {
                return Result<string>.Failure("User not found");
            }
            if (user.Email != request.Email)
            {
                if (await _userManager.Users.AnyAsync(p => p.Email == request.Email))
                {
                    return Result<string>.Failure("Email already exists");
                }
            }
            if (user.UserName != request.UserName)
            {
                if (await _userManager.Users.AnyAsync(p => p.UserName == request.UserName))
                {
                    return Result<string>.Failure("UserName already exists");
                }
            }
            

            _mapper.Map(request,user);

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }
            if (request.RoleIds.Any())
            {
                List<AppUserRole> userRoles = await _userRoleRepository.Where(p=>p.UserId==user.Id).ToListAsync(cancellationToken);
                _userRoleRepository.DeleteRange(userRoles);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                userRoles = new List<AppUserRole>();
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
            return "User update is successful";
        }
    }

}
