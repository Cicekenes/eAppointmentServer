using eAppointmentServer.Domain.Entities;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointmentsByDoctorId
{
    public sealed record GetAllAppointmentByDoctorIdQueryResponse(
        Guid Id,
        DateTime StartDate,
        DateTime EndDate,
        string Title,
        Patient Patient);
}
