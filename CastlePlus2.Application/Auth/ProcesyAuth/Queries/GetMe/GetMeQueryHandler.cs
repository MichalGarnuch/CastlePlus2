using CastlePlus2.Application.Interfaces.Auth;
using CastlePlus2.Contracts.DTOs.Auth;
using MediatR;

namespace CastlePlus2.Application.Auth.ProcesyAuth.Queries.GetMe
{
    public sealed class GetMeQueryHandler : IRequestHandler<GetMeQuery, CurrentUserDto?>
    {
        private readonly IUzytkownikAuthRepository _uzytkownikRepository;

        public GetMeQueryHandler(IUzytkownikAuthRepository uzytkownikRepository)
        {
            _uzytkownikRepository = uzytkownikRepository;
        }

        public async Task<CurrentUserDto?> Handle(GetMeQuery request, CancellationToken ct)
        {
            var user = await _uzytkownikRepository.FindByIdAsync(request.UserId, ct);
            if (user == null || !user.CzyAktywny)
                return null;

            var roles = await _uzytkownikRepository.GetRoleCodesAsync(user.IdUzytkownika, ct);

            return new CurrentUserDto
            {
                IdUzytkownika = user.IdUzytkownika,
                Login = user.Login,
                Email = user.Email,
                Role = roles
            };
        }
    }
}
