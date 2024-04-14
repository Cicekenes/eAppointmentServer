using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointmentsByDoctorId
{
    internal sealed class GetAllAppointmentByDoctorIdQueryHandler(IAppointmentRepository _appointmentRepository) : IRequestHandler<GetAllAppointmentsByDoctorIdQuery, Result<List<GetAllAppointmentByDoctorIdQueryResponse>>>
    {
        public async Task<Result<List<GetAllAppointmentByDoctorIdQueryResponse>>> Handle(GetAllAppointmentsByDoctorIdQuery request, CancellationToken cancellationToken)
        {
            List<Appointment> appointments = await _appointmentRepository
                .Where(p=>p.DoctorId==request.DoctorId)
                .Include(p=>p.Patient)
                .ToListAsync(cancellationToken);

            List<GetAllAppointmentByDoctorIdQueryResponse> response = 
                appointments.Select(s => 
                new GetAllAppointmentByDoctorIdQueryResponse(
                    s.Id,
                    s.StartDate,
                    s.EndDate,
                    s.Patient!.FullName,
                    s.Patient))
                .ToList();

            return response;
        }
    }
}
