using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComerence.Api.Controllers;
using EComerence.Core.Domain;
using EComerence.Infrastructure.Commands;
using EComerence.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace PGSTask.Web.Api.Controllers
{
    [Route("[controller]")]
    public class UsersController : DefaultController
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            return Json(await _userService.GetAccountAsync(UserId));
        }
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody]RegisterUser command)
        {
            var Id = Guid.NewGuid();
            await _userService.RegisterAsync(Id, command.Email, command.Name, command.Password, command.City,command.Address,command.PostalCode);
            return Created($"/account/{Id}", null);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]Login command)
        {
            return Json(await _userService.LoginAsync(command.Email, command.Password));
        }


    }
}