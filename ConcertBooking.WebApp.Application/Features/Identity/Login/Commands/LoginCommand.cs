using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Domain.Common;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Identity.Login.Commands;

public sealed record LoginCommand(string Email, string Password) : IRequest<Result<TokenResponse>>;