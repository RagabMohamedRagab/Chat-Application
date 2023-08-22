using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SignalRDay1.ViewModels;

namespace SignalRDay1.Models {
    public class MappingProfile:Profile {
        public MappingProfile()
        {
            CreateMap<UserRegistrationModel, IdentityUser>()
          .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
