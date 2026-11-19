using ConcertBooking.WebApp.Application.Contracts.Infrastructure.Identity;
using ConcertBooking.WebApp.Application.Contracts.Persistence;
using ConcertBooking.WebApp.Domain.Common;
using ConcertBooking.WebApp.Domain.Entities;
using MediatR;

namespace ConcertBooking.WebApp.Application.Features.Identity.Register.Commands;

public class RegisterCommandHandler(
    IIdentityService identityService,
    ICustomerRepository customerRepository) 
    : IRequestHandler<RegisterCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    { 
        // 1. Register the credentials of the new customer
        var created = await identityService.RegisterAsync(request.Email, request.Password, cancellationToken);
        if(created.IsFailure)
            return created.Error;
        
        // 2. Create the new customer
        var customer = new Customer(created.Data, request.FirstName, request.LastName, request.Password, request.PhoneNumber);
        
        // 3. Add the new customer to the repository
        await customerRepository.AddAsync(customer, cancellationToken);

        // 4. Return the ID of the new customer
        return customer.Id;
    }
}