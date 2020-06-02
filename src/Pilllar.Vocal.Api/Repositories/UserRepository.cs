using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Pilllar.Vocal.Models;
using Pilllar.Vocal.Services;
using Pilllar.Vocal.Api.Helpers;
using Pilllar.Vocal.Api.ResourceParameters;
using Microsoft.AspNetCore.Routing;

namespace Pilllar.Vocal.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly PilllarContext _context;
        private readonly IPropertyMappingService _propertyMappingService;
        public UserRepository(PilllarContext context, IPropertyMappingService propertyMappingService)
        {
            _context = context;
            _propertyMappingService = propertyMappingService;
        }

        public User Get(Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            User user = _context.Users.Find(userId);

            if (user == null)
                return null;

            //Example when if I need a collection
            //_context.Entry(user).Collection(u => u.Roles).Load();

            return user;

            //Just another way of doing
            //return _context.Users.FirstOrDefault(u => u.Id == userId);
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

        public User Get(string email)
        {
            var users = _context.Users;
            return users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefault();
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

        //public void Save(User user)
        //{
        //Verify if it´s necessary to indicate what property is to be modified in case of wanting just that to be modified
        //_context.Entry(user).Property(p => p.Email).IsModified = true;
        //_context.Users.Attach(user);

            //If the entry is being tracked, then invoking update API is not needed. 
            // context.Users.Update(user);
        //}
    }
}
