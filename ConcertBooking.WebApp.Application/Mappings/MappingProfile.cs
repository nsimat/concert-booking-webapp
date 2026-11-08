using AutoMapper;
using ConcertBooking.WebApp.Application.DTOs;
using ConcertBooking.WebApp.Application.Features.Bookings.Commands.UpdateBooking;
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

        CreateMap<Booking, UpdateBookingCommand>().ReverseMap();
    }
}