using AutoMapper;
using ConcertBooking.WebApp.Application.DTOs;
using ConcertBooking.WebApp.Application.Features.Artists.Queries.GetArtistDetails;
using ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingDetails;
using ConcertBooking.WebApp.Application.Features.Bookings.Queries.GetBookingsList;
using ConcertBooking.WebApp.Domain.Entities;

namespace ConcertBooking.WebApp.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Booking, BookingDetailVm>().ReverseMap();
        CreateMap<Concert, ConcertDto>().ReverseMap();
        CreateMap<Customer, UserDto>().ReverseMap();
        CreateMap<Booking, BookingListVm>().ReverseMap();
        
        CreateMap<Artist, ArtistDetailsVm>().ReverseMap();
        CreateMap<Artist, ArtistDto>().ReverseMap();
        CreateMap<Venue, VenueDto>().ReverseMap();
        CreateMap<Ticket, TicketDto>().ReverseMap();
    }
}