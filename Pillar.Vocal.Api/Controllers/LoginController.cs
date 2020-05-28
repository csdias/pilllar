using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pilllar.Vocal.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Pilllar.Vocal.Services;
using Pilllar.Vocal.Repositories;
using Microsoft.Extensions.Logging;

namespace Pilllar.Vocal.Api.Controllers
{
    [Route("v1/account")]
    public class LoginController : Controller
    {

        private readonly PilllarContext _context;
        private readonly UserRepository _repository;
        private readonly ILogger<LoginController> _logger;
        private readonly IPropertyMappingService _propertyMappingService;

        public LoginController(PilllarContext context, ILogger<LoginController> logger, IPropertyMappingService _propertyMappingService)
        {
            _context = context;
            _repository = new UserRepository(context, _propertyMappingService);
            _logger = logger;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public ActionResult<dynamic> Authenticate([FromBody]User model)
        {
            var encryptedPassword = TokenService.EncryptPassword(model.Password);

            var user = _repository.Get(model.Name, encryptedPassword);

            if (user == null)
                return NotFound(new { message = "Usuário ou senha inválidos" });

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user,
                token
            };
        }

        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "Manager, Employee")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "Manager")]
        public string Manager() => "Gerente";

    }
}