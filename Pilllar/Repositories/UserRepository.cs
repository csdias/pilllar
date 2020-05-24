using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Pilllar.Admin.Models;
using Pilllar.Admin.Services;
using Pilllar.Admin.WebApi.Helpers;
using Pilllar.Admin.WebApi.ResourceParameters;
using Microsoft.AspNetCore.Routing;

namespace Pilllar.Admin.Repositories
{
    public class UserRepository
    {
        private readonly PilllarContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        public UserRepository(PilllarContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
        }

        public User GetById(long usuarioId)
        {
            User user = _context.Users.Find(usuarioId);

            if (user == null)
                return null;

            return user;
        }

        //public IEnumerable<User> Get(User user)
        public User Get(string name, string password)
        {
            var users = _context.Users;
            return users.Where(x => x.Name.ToLower() == name.ToLower() && x.Password == x.Password).FirstOrDefault();
            //var users = new List<User>();
            //users.Add(new User { Id = Guid.NewGuid(), Name = "Zeus", Email = "zeus@gmail.com", Password = "zeus", Role = "manager" });
            //users.Add(new User { Id = Guid.NewGuid(), Name = "Atena", Email = "atena@gmail.com", Password = "atena", Role = "employee" });
        }
        public PagedList<User> Find(UserResourceParameters userResourceParameters)
        {
            if (userResourceParameters == null)
            {
                throw new ArgumentNullException(nameof(userResourceParameters));
            }

            var collection = _context.Users as IQueryable<User>;

            if (!string.IsNullOrWhiteSpace(userResourceParameters.SearchQuery))
            {

                var searchQuery = userResourceParameters.SearchQuery.Trim();
                collection = collection.Where(a => a.Name.Contains(searchQuery)
                    || a.Email.Contains(searchQuery)
                    || (a.Id != null && a.Id.ToString().Contains(searchQuery)));
            }

            if (!string.IsNullOrWhiteSpace(userResourceParameters.OrderBy))
            {
                // get property mapping dictionary
                var authorPropertyMappingDictionary =
                    _propertyMappingService.GetPropertyMapping<UserDto, User>();

                collection = collection.ApplySort(userResourceParameters.OrderBy,
                    authorPropertyMappingDictionary);
            }

            return PagedList<User>.Create(collection,
                userResourceParameters.PageNumber,
                userResourceParameters.PageSize);
        }
        public void Save(User user)
        {
            _context.Users.Attach(user);
        }

    }
}
