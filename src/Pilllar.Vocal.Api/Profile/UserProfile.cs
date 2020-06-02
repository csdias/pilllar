using Pilllar.Vocal.Models;

namespace Pilllar.Vocal.Api.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();

            CreateMap<User, UserDtoForCreation>();

            CreateMap<UserDtoForCreation, User>();
        }
    }
}
