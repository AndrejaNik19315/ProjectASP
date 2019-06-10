using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Helpers;
using Application.Commands.Authorization;
using Application.Dto;
using Application.Exceptions;
using Application.HelperClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IGetAuthUserCommand _getAuthUser;
        private readonly Encryption _encryption;

        public AuthController(IGetAuthUserCommand getAuthUser, Encryption encryption)
        {
            _getAuthUser = getAuthUser;
            _encryption = encryption;
        }

        // POST: api/Auth
        [HttpPost]
        public IActionResult Post([FromBody] LoginDto dto)
        {
            try
            {
                var user = _getAuthUser.Execute(dto);

                var stringObject = JsonConvert.SerializeObject(user);
                var encrypted = _encryption.EncryptString(stringObject);

                return Ok(new { token = encrypted });
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (EntityNotActiveException ex) {
                return UnprocessableEntity(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("decode")]
        public IActionResult Decode(string value) {
            var decodedString = _encryption.DecryptString(value);
            decodedString = decodedString.Replace("\f", "");
            var user = JsonConvert.DeserializeObject<LoggedUser>(decodedString);

            return null;
        }
    }
}
