using Pilllar.Vocal.Api.Helpers;
using Pilllar.Vocal.Api.ResourceParameters;
using Pilllar.Vocal.Models;

namespace Pilllar.Vocal.Repositories
{
    public interface IUserRepository
    {
        PagedList<User> Find(UserResourceParameters userResourceParameters);
        User Get(string name, string password);
        User GetById(long usuarioId);
        void Save(User user);
    }
}