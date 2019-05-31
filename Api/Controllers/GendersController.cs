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

        private IGetGenderCommand _getGender;
        private IGetGendersCommand _getGenders;
        private IAddGenderCommand _addGender;
        private IEditGenderCommand _editGender;
        private IDeleteGenderCommand _deleteGender;

        private string genericErrorMsg = "Something went wrong on the server";

        public GendersController(IGetGenderCommand getGender, IGetGendersCommand getGenders, IAddGenderCommand addGender, IEditGenderCommand editGender, IDeleteGenderCommand deleteGender)
        {
            _getGender = getGender;
            _getGenders = getGenders;
            _addGender = addGender;
            _editGender = editGender;
            _deleteGender = deleteGender;
        }

        // GET: api/Genders
        [HttpGet]
        public IActionResult Get([FromQuery] GenderSearch query)
        {
            return Ok(_getGenders.Execute(query));
        }

        // GET: api/Genders/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
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
