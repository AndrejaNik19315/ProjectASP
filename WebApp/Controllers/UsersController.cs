using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Domain;
using EFDataAccess;
using Application.Searches;
using Application.Commands.Users.WebApp;
using Application.Commands.Users;
using Application.Exceptions;
using Application.Dto;

namespace WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IGetUsersWebCommand _getUsers;
        private readonly IGetUserCommand _getUser;
        private readonly IAddUserCommand _addUser;
        private readonly IEditUserCommand _editUser;
        private readonly IDeleteUserCommand _deleteUser;

        public readonly string genericErrorMsg = "Something went wrong on the server.";

        public UsersController(ProjectContext context, IGetUsersWebCommand getUsers, IGetUserCommand getUser, IAddUserCommand addUser, IEditUserCommand editUser, IDeleteUserCommand deleteUser)
        {
            _context = context;
            _getUsers = getUsers;
            _getUser = getUser;
            _addUser = addUser;
            _editUser = editUser;
            _deleteUser = deleteUser;
        }

        // GET: Users
        public IActionResult Index([FromQuery] UserSearchWeb query)
        {
            var users = _getUsers.Execute(query);
            return View(users);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var user = _getUser.Execute(id);
                return View(user);
            }
            catch (EntityNotFoundException)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _addUser.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityAlreadyExistsException e)
            {
                TempData["error"] = e.Message;
            }
            catch (EntityUnprocessableException e)
            {
                TempData["error"] = e.Message;
            }
            catch (Exception e) {
                TempData["error"] = genericErrorMsg;
            }

            return View();
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            try {
                var dto = _context.Users.Find(id);

                var user = new UserDto
                {
                    Id = dto.Id,
                    Firstname = dto.Firstname,
                    Lastname = dto.Lastname,
                    Username = dto.Username,
                    Email = dto.Email,
                    IsActive = dto.IsActive,
                    Password = dto.Password,
                    RoleId = dto.RoleId
                };

                ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Name");
                return View(user);
            }
            catch (EntityNotFoundException) {
                return View("NotFound");
            }
            catch (Exception ex) {
                return RedirectToAction("Index");
            }
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, UserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                _editUser.Execute(dto, id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return View("NotFound");
            }
            catch (EntityUnprocessableException ex)
            {
                TempData["error"] = ex.Message;
                return View(dto);
            }
            catch (EntityAlreadyExistsException ex)
            {
                TempData["error"] = ex.Message;
                return View(dto);
            }
            catch (Exception)
            {
                TempData["error"] = genericErrorMsg;
                return View(dto);
            }
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromForm(Name = "id")] int id)
        {
            try {
                _deleteUser.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException) {
                return View("NotFound");
            }
            catch (Exception ex) {
                return View();
            }
        }
    }
}
