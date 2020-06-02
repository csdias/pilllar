using Pilllar.Vocal.Api.Helpers;
using Pilllar.Vocal.Api.ResourceParameters;
using Pilllar.Vocal.Models;
using System;

namespace Pilllar.Vocal.Repositories
{
    public interface IUserRepository
    {
        PagedList<User> Find(UserResourceParameters userResourceParameters);
        User Get(Guid userId);
        User Get(string name, string password);
        void Save(User user);
    }
}