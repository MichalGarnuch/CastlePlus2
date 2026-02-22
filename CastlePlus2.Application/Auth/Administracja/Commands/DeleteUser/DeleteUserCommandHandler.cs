using CastlePlus2.Application.Common.Exceptions;
using CastlePlus2.Application.Interfaces.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.Administracja.Commands.DeleteUser
{
    public sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUzytkownikAuthRepository _repository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public DeleteUserCommandHandler(IUzytkownikAuthRepository repository, IRefreshTokenRepository refreshTokenRepository)
        {
            _repository = repository;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task Handle(DeleteUserCommand request, CancellationToken ct)
        {
            if (request.UserId == request.DeletedByUserId)
            {
                throw new BusinessConflictException("Administrator nie może usunąć własnego konta.");
            }

            var utcNow = DateTime.UtcNow;
            var deleted = await _repository.SoftDeleteUserAsync(request.UserId, request.DeletedByLogin, utcNow, ct);
            if (!deleted)
            {
                throw new KeyNotFoundException("Użytkownik nie istnieje.");
            }

            await _refreshTokenRepository.RevokeAllForUserAsync(request.UserId, utcNow, ct);
        }
    }
}