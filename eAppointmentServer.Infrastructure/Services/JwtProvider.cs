using eAppointmentServer.Application.Services;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace eAppointmentServer.Infrastructure.Services
{
    internal sealed class JwtProvider(IConfiguration _configuration,
        IUserRoleRepository _userRoleRepository,
        RoleManager<AppRole> _roleManager) : IJwtProvider
    {
        public async Task<string> CreateTokenAsync(AppUser appUser)
        {
            List<AppUserRole> appUserRoles = await _userRoleRepository.Where(p=>p.UserId==appUser.Id).ToListAsync();
            List<AppRole> roles = new List<AppRole>();

            foreach (var userRole in appUserRoles)
            {
                AppRole? role = await _roleManager.Roles.Where(p=>p.Id==userRole.RoleId).FirstOrDefaultAsync();
                if(role is not null)
                {
                    roles.Add(role);
                }
            }

            List<string?> stringRoles = roles.Select(p => p.Name).ToList();

            List<Claim> claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier,appUser.Id.ToString()),
            new Claim(ClaimTypes.Name,appUser.FullName),
            new Claim(ClaimTypes.Email,appUser.Email ?? string.Empty),
            new Claim("UserName",appUser.Email ?? string.Empty),
            new Claim(ClaimTypes.Role,JsonSerializer.Serialize(stringRoles))
            };

            DateTime expires = DateTime.Now.AddDays(1);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:SecretKey").Value ?? ""));

            SigningCredentials signingCredentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha512);

            JwtSecurityToken securityToken = new JwtSecurityToken(
                    issuer: _configuration.GetSection("Jwt:Issuer").Value,
                    audience: _configuration.GetSection("Jwt:Audience").Value,
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: expires,
                    signingCredentials: signingCredentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string token = handler.WriteToken(securityToken);

            return token;
        }
    }
}
