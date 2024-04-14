using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.GetAllAppointmentsByDoctorId
{
    public sealed record GetAllAppointmentsByDoctorIdQuery(Guid DoctorId):IRequest<Result<List<GetAllAppointmentByDoctorIdQueryResponse>>>;
}
