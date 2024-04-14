using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Patients.CreatePatient
{
    internal sealed class CreatePatientCommandHandler(IPatientRepository _patientRepository, IUnitOfWork _unitOfWork, IMapper _mapper) : IRequestHandler<CreatePatientCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            if(await _patientRepository.AnyAsync(p => p.IdentityNumber == request.IdentityNumber))
            {
                return Result<string>.Failure("Patient already recorded");
            }
            Patient patient = _mapper.Map<Patient>(request);
            await _patientRepository.AddAsync(patient);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return "Patient create is successful";
        }
    }


}
