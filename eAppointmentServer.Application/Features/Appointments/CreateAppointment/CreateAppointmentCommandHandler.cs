﻿using eAppointmentServer.Domain.Entities;
using eAppointmentServer.Domain.Repositories;
using GenericRepository;
using MediatR;
using TS.Result;

namespace eAppointmentServer.Application.Features.Appointments.CreateAppointment
{
    internal sealed class CreateAppointmentCommandHandler(
        IAppointmentRepository _appointmentRepository,
        IUnitOfWork _unitOfWork,
        IPatientRepository _patientRepository) : IRequestHandler<CreateAppointmentCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {

            DateTime startDate = Convert.ToDateTime(request.StartDate);
            DateTime endDate = Convert.ToDateTime(request.EndDate);

            Patient patient = new();

            if (request.PatientId is null)
            {
                patient = new()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IdentityNumber = request.IdentityNumber,
                    City = request.City,
                    Town = request.Town,
                    FullAddress = request.FullAddress
                };

                await _patientRepository.AddAsync(patient, cancellationToken);
            }

            bool isAppointmentDateNotAvailable =
                await _appointmentRepository
                .AnyAsync(p => p.DoctorId == request.DoctorId &&
                 ((p.StartDate < endDate && p.StartDate >= startDate) || // Mevcut randevunun bitişi, diğer randevunun başlangıcıyla çakışıyor
                 (p.EndDate > startDate && p.EndDate <= endDate) || // Mevcut randevunun başlangıcı, diğer randevunun bitişiyle çakışıyor
                 (p.StartDate >= startDate && p.EndDate <= endDate) || // Mevcut randevu, diğer randevu içinde tamamen
                 (p.StartDate <= startDate && p.EndDate >= endDate)), // Mevcut randevu, diğer randevuyu tamamen kapsıyor
                 cancellationToken);

            if (isAppointmentDateNotAvailable)
            {
                return Result<string>.Failure("Appointment date is not available");
            }

            Appointment appointment = new()
            {
                DoctorId = request.DoctorId,
                PatientId = request.PatientId ?? patient.Id,
                StartDate = Convert.ToDateTime(request.StartDate),
                EndDate = Convert.ToDateTime(request.EndDate),
                IsCompleted = false
            };

            await _appointmentRepository.AddAsync(appointment, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return "Appointment create is successful";
        }
    }


}
