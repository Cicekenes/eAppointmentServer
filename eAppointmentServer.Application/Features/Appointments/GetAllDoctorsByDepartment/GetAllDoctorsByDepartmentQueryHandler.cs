using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllDoctorsByDepartment
{
    internal sealed class GetAllDoctorsByDepartmentQueryHandler(IDoctorRepository _doctorRepository) : IRequestHandler<GetAllDoctorsByDepartmentQuery, Result<List<Doctor>>>
    {
        public async Task<Result<List<Doctor>>> Handle(GetAllDoctorsByDepartmentQuery request, CancellationToken cancellationToken)
        {
            List<Doctor> doctors = await _doctorRepository
                .Where(p=>p.Department==request.DepartmentValue)
                .OrderBy(p=>p.FirstName)
                .ToListAsync(cancellationToken);
            return doctors;
        }
    }

}
