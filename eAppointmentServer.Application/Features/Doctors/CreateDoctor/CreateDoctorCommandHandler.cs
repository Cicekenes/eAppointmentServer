using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.CreateDoctor
{
    internal sealed class CreateDoctorCommandHandler(IDoctorRepository _doctorRepository,IUnitOfWork _unitOfWork,IMapper _mapper) : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor doctor = _mapper.Map<Doctor>(request);
            await _doctorRepository.AddAsync(doctor,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor create is successful";
        }
    }

}
