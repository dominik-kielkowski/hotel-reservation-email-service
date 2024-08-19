using MediatR;

namespace Newsletter.Application.Commands_Queries.MakeReservation
{
    public sealed record MakeReservationCommand(int RoomId, Guid UserId, string UserEmail) : IRequest<MakeReservationResult>;

    public sealed record MakeReservationResult(bool IsSuccess);
}

