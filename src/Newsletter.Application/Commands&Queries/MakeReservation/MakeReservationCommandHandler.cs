using MediatR;
using Newsletter.Core.Interfaces;

namespace Newsletter.Application.Commands_Queries.MakeReservation
{
    public class MakeReservationCommandHandler : IRequestHandler<MakeReservationCommand, MakeReservationResult>
    {
        private readonly IEmailService _emailService;

        public MakeReservationCommandHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<MakeReservationResult> Handle(MakeReservationCommand request, CancellationToken cancellationToken)
        {
            var emailSent = await _emailService.SendEmailAsync(
                request.UserEmail,
                "Potwierdzenie rezerwacji pokoju",
                $"Drogi użytkowniku, potwierdzamy Twoją rezerwację pokoju o ID: {request.RoomId}. Dziękujemy za dokonanie rezerwacji.",
                $"<strong>Drogi użytkowniku</strong>,<br><br> Potwierdzamy Twoją rezerwację pokoju o ID: {request.RoomId}.<br><br> Dziękujemy za dokonanie rezerwacji."
            );

            return new MakeReservationResult(emailSent);
        }
    }
}
