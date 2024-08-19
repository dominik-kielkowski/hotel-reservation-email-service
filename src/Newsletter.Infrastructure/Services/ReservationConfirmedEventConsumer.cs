using Application.Hotels.Rooms.MakeReservation;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Newsletter.Application.Commands_Queries.MakeReservation;

public class ReservationConfirmedEventConsumer : IConsumer<ReservationConfirmedEvent>
{
    private readonly IMediator _mediator;

    public ReservationConfirmedEventConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task Consume(ConsumeContext<ReservationConfirmedEvent> context)
    {
        var reservationConfirmedEvent = context.Message;

        // Create a command based on the event data
        var makeReservationCommand = new MakeReservationCommand(
            reservationConfirmedEvent.RoomId,
            reservationConfirmedEvent.UserId,
            reservationConfirmedEvent.UserEmail
        );

        // Send the command, which should trigger the corresponding handler
        await _mediator.Send(makeReservationCommand);
    }
}
