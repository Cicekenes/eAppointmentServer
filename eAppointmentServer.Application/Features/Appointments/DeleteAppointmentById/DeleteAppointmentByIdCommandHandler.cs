using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.DeleteAppointmentById
{
    internal sealed class DeleteAppointmentByIdCommandHandler(
        IAppointmentRepository _appointmentRepository,
        IUnitOfWork _unitOfWork) : IRequestHandler<DeleteAppointmentByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteAppointmentByIdCommand request, CancellationToken cancellationToken)
        {
            Appointment? appointment = await _appointmentRepository.GetByExpressionAsync(p=>p.Id==request.Id,cancellationToken);
            if(appointment is null)
            {
                return Result<string>.Failure("Appointment not found");
            }
            if (appointment.IsCompleted)
            {
                return Result<string>.Failure("You cannot delete a completed appointment");
            }
            _appointmentRepository.Delete(appointment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return "Appointment delete is successful";
        }
    }
}
