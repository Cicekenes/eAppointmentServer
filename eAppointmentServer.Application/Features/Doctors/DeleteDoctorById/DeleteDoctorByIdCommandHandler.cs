using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.DeleteDoctorById
{
    internal sealed class DeleteDoctorByIdCommandHandler(IDoctorRepository _doctorRepository,IUnitOfWork _unitOfWork) : IRequestHandler<DeleteDoctorByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeleteDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _doctorRepository.GetByExpressionAsync(p=>p.Id==request.Id,cancellationToken);
            if(doctor is null)
            {
                return Result<string>.Failure("Doctor not found");
            }
            _doctorRepository.Delete(doctor);
            await _unitOfWork.SaveChangesAsync();

            return "Doctor delete is successful";
        }
    }
}
