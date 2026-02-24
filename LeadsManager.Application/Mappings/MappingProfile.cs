using AutoMapper;
using LeadsManager.Application.DTOs;
using LeadsManager.Domain.Entities;

namespace LeadsManager.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Lead, LeadDto>()
            .ForMember(d => d.ContactFirstName, opt => opt.MapFrom(s => s.Contact.FirstName))
            .ForMember(d => d.ContactLastName, opt => opt.MapFrom(s => s.Contact.LastName))
            .ForMember(d => d.ContactFullName, opt => opt.MapFrom(s => s.Contact.FullName))
            .ForMember(d => d.ContactEmail, opt => opt.MapFrom(s => s.Contact.Email))
            .ForMember(d => d.ContactPhoneNumber, opt => opt.MapFrom(s => s.Contact.PhoneNumber))
            .ForMember(d => d.Suburb, opt => opt.MapFrom(s => s.Location.Suburb))
            .ForMember(d => d.Price, opt => opt.MapFrom(s => s.Price.Amount))
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));
    }
}
