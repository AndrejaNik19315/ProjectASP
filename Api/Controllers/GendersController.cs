using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Commands.Genders;
using Application.Searches;
using Application.Exceptions;
using Application.Dto;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GendersController : ControllerBase
    {

        private readonly IGetGenderCommand _getGender;
        private readonly IGetGendersCommand _getGenders;
        private readonly IAddGenderCommand _addGender;
        private readonly IEditGenderCommand _editGender;
        private readonly IDeleteGenderCommand _deleteGender;

        private string genericErrorMsg = "Something went wrong on the server";

        public GendersController(IGetGenderCommand getGender, IGetGendersCommand getGenders, IAddGenderCommand addGender, IEditGenderCommand editGender, IDeleteGenderCommand deleteGender)
        {
            _getGender = getGender;
            _getGenders = getGenders;
            _addGender = addGender;
            _editGender = editGender;
            _deleteGender = deleteGender;
        }

        /// <summary>
        /// Returns all genders
        /// </summary>
        // GET: api/Genders
        [HttpGet]
        public ActionResult<IEnumerable<GenderDto>> Get([FromQuery] GenderSearch query)
        {
            return Ok(_getGenders.Execute(query));
        }

        /// <response code="404">Gender not found</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Returns single gender by id
        /// </summary>
        // GET: api/Genders/5
        [HttpGet("{id}")]
        public ActionResult<GenderDto> Get(int id)
        {
            try
            {
                var gender = _getGender.Execute(id);
                return Ok(gender);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="400">Bad Format</response>
        /// <response code="404">Gender not found</response>
        /// <response code="409">Conflict, gender with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Update gender
        /// </summary>
        /// <remarks>
        /// PUT / Example
        /// {
        ///     "Sex" : "Attack Helicopter"
        /// }
        /// </remarks>
        // PUT: api/Genders/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] GenderDto gender)
        {
            try
            {
                _editGender.Execute(gender, id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (EntityAlreadyExistsException ex)
            {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="201">Created</response>
        /// <response code="400">Bad Format</response>
        /// <response code="409">Conflict, gender with that name exists.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Create gender
        /// </summary>
        /// <remarks>
        /// POST / Example
        /// {
        ///     "Sex" : "Male"
        /// }
        /// </remarks>
        // POST: api/Genders
        [HttpPost]
        public IActionResult Post([FromBody] GenderDto dto)
        {
            try
            {
                _addGender.Execute(dto);
                return Created("api/genders" + dto.Id, new GenderDto
                {
                    Id = dto.Id,
                    Sex = dto.Sex
                });
            }
            catch (EntityAlreadyExistsException ex) {
                return Conflict(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }

        /// <response code="404">Gender doesn't exist.</response>
        /// <response code="500">Server error.</response>
        /// <summary>
        /// Remove gender by id
        /// </summary>
        /// <remarks>
        /// NOTE: Genders that are in use cannot be removed and will return code 500
        /// </remarks>
        // DELETE: api/Genders/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _deleteGender.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex) {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, genericErrorMsg);
            }
        }
    }
}
