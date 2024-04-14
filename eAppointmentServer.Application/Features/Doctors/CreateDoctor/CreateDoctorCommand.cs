using eAppointmentServer.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.CreateDoctor
{
    public sealed record CreateDoctorCommand(string FirstName,string LastName,int DepartmentValue):IRequest<Result<string>>;
}
