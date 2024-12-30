using AutoMapper;
using ETicket.Data.Acess.layer.Models;
using ETicket.Presentation.layer.Areas.Identity.Models.ViewModels;

namespace ETicket.Presentation.layer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {

            CreateMap<ApplicationUser, ApplicationUserVM>()
                .ForMember(dest=> dest.Phone ,config =>config.MapFrom(src =>src.PhoneNumber) )
                .ReverseMap()
                .ForMember(dest => dest.PhoneNumber, config => config.MapFrom(src => src.Phone))
;

        }
    }
}
