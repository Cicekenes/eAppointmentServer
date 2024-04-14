using AutoMapper;
using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Doctors.UpdateDoctor
{
    internal sealed class UpdateDoctorCommandHandler(IDoctorRepository _doctorRepository,
        IUnitOfWork _unitOfWork,
        IMapper _mapper) : IRequestHandler<UpdateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            Doctor? doctor = await _doctorRepository.GetByExpressionWithTrackingAsync(p=>p.Id==request.Id);
            if(doctor is null)
            {
                return Result<string>.Failure("Doctor not found");
            }

            _mapper.Map(request,doctor);

            _doctorRepository.Update(doctor);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return "Doctor update is successful";
        }
    }
}
