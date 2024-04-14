using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.DeletePatientById
{
    internal sealed class DeletePatientByIdCommandHandler(IPatientRepository _patientRepository,
        IUnitOfWork _unitOfWork
        ) : IRequestHandler<DeletePatientByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(DeletePatientByIdCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _patientRepository.GetByExpressionAsync(p=>p.Id==request.Id,cancellationToken);
            if(patient is null)
            {
                return Result<string>.Failure("Patient not found");
            }

            _patientRepository.Delete(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Patient delete is successful";
        }
    }
}
