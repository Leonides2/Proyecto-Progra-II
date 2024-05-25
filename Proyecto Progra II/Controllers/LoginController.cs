﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Proyecto_Progra_II.Models;
using Proyecto_Progra_II.Models.Custom;
using Proyecto_Progra_II.Services.Login;

namespace Proyecto_Progra_II.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginService _loginService;
        private readonly ApiContext _context;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UsuarioRequest request)
        {
            var user_request = await _loginService.ReturnToken(request);

            if (user_request == null)
            {
                return Unauthorized();
            }

            return Ok(user_request);
        }

    }
}