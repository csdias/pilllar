using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pilllar.Admin.Models;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Pilllar.Admin.Services;
using Pilllar.Admin.Repositories;
using Microsoft.Extensions.Logging;

namespace Pilllar.Admin.WebApi.Controllers
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
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "manager")]
        public string Manager() => "Gerente";

    }
}