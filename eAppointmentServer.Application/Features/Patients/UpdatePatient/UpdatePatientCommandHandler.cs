using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.UpdatePatient
{
    internal sealed class UpdatePatientCommandHandler(IPatientRepository _patientRepository,
        IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<UpdatePatientCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {
            Patient? patient = await _patientRepository.GetByExpressionWithTrackingAsync(p=>p.Id==request.Id);
            if(patient is null)
            {
                return Result<string>.Failure("Patient not found");
            }

            if (patient.IdentityNumber != request.IdentityNumber)
            {
                if(await _patientRepository.AnyAsync(p => p.IdentityNumber == request.IdentityNumber))
                {
                    return Result<string>.Failure("This identity number already use");
                }
            }

            _mapper.Map(request,patient);
            _patientRepository.Update(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return "Patient update is successful";
        }
    }

}
