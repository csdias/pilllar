using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pilllar.Vocal.Models;
using Pilllar.Vocal.Repositories;
using Pilllar.Vocal.Services;
using Pilllar.Vocal.Api.ResourceParameters;
using Pilllar.Vocal.Api.Helpers;
using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Pilllar.Vocal.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly PilllarContext _context;
        private readonly UserRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;
        private readonly IPropertyMappingService _propertyMappingService;
        private readonly IPropertyCheckerService _propertyCheckerService;

        public UsersController(
            PilllarContext context,
            IMapper mapper,
            ILogger<UsersController> logger,
            IPropertyMappingService propertyMappingService,
            IPropertyCheckerService propertyCheckerService
            )
        {
            _context = context;
            _repository = new UserRepository(context, propertyMappingService);
            _logger = logger;
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
            _propertyMappingService = propertyMappingService ??
              throw new ArgumentNullException(nameof(propertyMappingService));
            _propertyCheckerService = propertyCheckerService ??
              throw new ArgumentNullException(nameof(propertyCheckerService));
        }

        [HttpGet("{id}")]
        public User Get(int usuarioId)
        {
            return _repository.GetById(usuarioId);
        }

        [Authorize(Roles = "Manager, Employee")]
        [Authorize(Roles = "Manager")]
        [HttpGet(Name = nameof(GetUsers))]
        public ActionResult<IEnumerable<User>> GetUsers([FromQuery]UserResourceParameters userResourceParameters)
        {
            if (!_propertyMappingService.ValidMappingExistsFor<UserDto, User>(userResourceParameters.OrderBy))
            {
                return BadRequest();
            }

            if (!_propertyCheckerService.TypeHasProperties<UserDto>
              (userResourceParameters.Fields))
            {
                return BadRequest();
            }

            var usersFromRepo = _repository.Find(userResourceParameters);

            var previousPageLink = usersFromRepo.HasPrevious ?
                CreateUsersResourceUri(userResourceParameters,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = usersFromRepo.HasNext ?
                CreateUsersResourceUri(userResourceParameters,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = usersFromRepo.TotalCount,
                pageSize = usersFromRepo.PageSize,
                currentPage = usersFromRepo.CurrentPage,
                totalPages = usersFromRepo.TotalPages,
                previousPageLink,
                nextPageLink
            };

            Response.Headers.Add("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<UserDto>>(usersFromRepo));
        }

        [HttpPost]
        public void Post(User user)
        {
            _repository.Save(user);
            _context.SaveChanges();
        }

        [HttpPut]
        public void Put(User user)
        {
            _repository.Save(user);
            _context.SaveChanges();
        }

        private string CreateUsersResourceUri(
           UserResourceParameters userResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetUsers),
                      new
                      {
                          fields = userResourceParameters.Fields,
                          orderBy = userResourceParameters.OrderBy,
                          pageNumber = userResourceParameters.PageNumber - 1,
                          pageSize = userResourceParameters.PageSize,
                          searchQuery = userResourceParameters.SearchQuery
                      });
                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetUsers),
                      new
                      {
                          fields = userResourceParameters.Fields,
                          orderBy = userResourceParameters.OrderBy,
                          pageNumber = userResourceParameters.PageNumber + 1,
                          pageSize = userResourceParameters.PageSize,
                          searchQuery = userResourceParameters.SearchQuery
                      });

                default:
                    return Url.Link(nameof(GetUsers),
                    new
                    {
                        fields = userResourceParameters.Fields,
                        orderBy = userResourceParameters.OrderBy,
                        pageNumber = userResourceParameters.PageNumber,
                        pageSize = userResourceParameters.PageSize,
                        searchQuery = userResourceParameters.SearchQuery
                    });
            }

        }
    }
}