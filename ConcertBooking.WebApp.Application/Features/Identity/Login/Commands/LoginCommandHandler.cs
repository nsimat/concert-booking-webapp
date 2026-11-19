using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Identity.Login.Commands;

public class LoginCommandHandler(
    IIdentityService identityService) 
    : IRequestHandler<LoginCommand, Result<TokenResponse>>
{
    public async Task<Result<TokenResponse>> Handle(LoginCommand request, CancellationToken cancellationToken) => await identityService.LoginAsync(request.Email, request.Password, cancellationToken);
}