using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.CreateAppointment
{
    public sealed record CreateAppointmentCommand(
        string StartDate,
        string EndDate,
        Guid DoctorId,
        Guid? PatientId,
        string FirstName,
        string LastName,
        string IdentityNumber,
        string City,
        string Town,
        string FullAddress): IRequest<Result<string>>;


}
