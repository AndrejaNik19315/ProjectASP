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
using Application.Commands.Items.WebApp;
using Application.Exceptions;
using Application.Commands.Items;
using Application.Dto;
using System.IO;
using Application.HelperClasses;

namespace WebApp.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IGetItemsWebCommand _getItems;
        private readonly IGetItemWebCommand _getItem;
        private readonly IAddItemCommand _addItem;
        private readonly IEditItemCommand _editItem;
        private readonly IDeleteItemCommand _deleteItem;

        public readonly string genericErrorMsg = "Something went wrong on the server.";

        public ItemsController(ProjectContext context, IGetItemsWebCommand getItems, IGetItemWebCommand getItem, IAddItemCommand addItem, IEditItemCommand editItem, IDeleteItemCommand deleteItem)
        {
            _context = context;
            _getItems = getItems;
            _getItem = getItem;
            _addItem = addItem;
            _editItem = editItem;
            _deleteItem = deleteItem;
        }

        // GET: Items
        public IActionResult Index([FromQuery] ItemSearchWeb query)
        {
            var items = _getItems.Execute(query);
            return View(items);
        }

        // GET: Items/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                var item = _getItem.Execute(id);
                return View(item);
            }
            catch (EntityNotFoundException)
            {
                Response.StatusCode = 404;
                return View("NotFound");
            }
            catch (Exception) {
                return View("Error");
            }
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["ItemQualityId"] = new SelectList(_context.ItemQualities, "Id", "Name");
            ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "Id", "Name");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] ItemDto dto)
        {
            if (!ModelState.IsValid) {
                return View(dto);
            }

            try
            {
                if (dto.Image != null) { 
                    var extension = Path.GetExtension(dto.Image.FileName);

                    if (!ImageUpload.AllowedExtensions.Contains(extension))
                    {
                        return UnprocessableEntity("Image is out of format. Allowed extensions: jpeg, jpg or png");
                    }

                    var fileName = (int)(DateTime.Now.Subtract(new DateTime(1970,1,1))).TotalSeconds + extension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    dto.Image.CopyTo(new FileStream(filePath, FileMode.Create));

                    dto.ImageName = fileName;
                }
                else {
                    dto.ImageName = "noimg.jpg";
                }

                _addItem.Execute(dto);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityUnprocessableException ex)
            {
                TempData["error"] = ex.Message;
            }
             catch (EntityAlreadyExistsException ex)
            {
                TempData["error"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;//genericErrorMsg;
            }

            return View();
        }

        // GET: Items/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                var dto = _context.Items.Find(id);

                var item = new ItemDto
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Cost = dto.Cost,
                    isCovert = dto.isCovert,
                    ItemQualityId = dto.ItemQualityId,
                    ItemTypeId = dto.ItemTypeId,
                    Quantity = dto.Quantity
                };

                ViewData["ItemQualityId"] = new SelectList(_context.ItemQualities, "Id", "Name");
                ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "Id", "Name");
                return View(item);
            }
            catch (EntityNotFoundException) {
                return View("NotFound");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ItemDto dto)
        {

            if (!ModelState.IsValid) {
                return View(dto);
            }

            try
            {
                _editItem.Execute(dto, id);
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

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromForm(Name="id")] int id)
        {
            try
            {
                _deleteItem.Execute(id);
                return RedirectToAction(nameof(Index));
            }
            catch (EntityNotFoundException)
            {
                return View("NotFound");
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
