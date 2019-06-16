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

namespace WebApp.Controllers
{
    public class ItemsController : Controller
    {

        private readonly IGetItemsWebCommand _getItems;
        private readonly IGetItemWebCommand _getItem;

        public ItemsController(IGetItemsWebCommand getItems, IGetItemWebCommand getItem)
        {
            _getItems = getItems;
            _getItem = getItem;
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
            //ViewData["ItemQualityId"] = new SelectList(_context.ItemQualities, "Id", "Name");
            //ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "Id", "Name");
            return View();
        }

        //// POST: Items/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Name,Cost,isCovert,inStock,ItemTypeId,ItemQualityId,Quantity,Id,CreatedAt,UpdatedAt")] Item item)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(item);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ItemQualityId"] = new SelectList(_context.ItemQualities, "Id", "Name", item.ItemQualityId);
        //    ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "Id", "Name", item.ItemTypeId);
        //    return View(item);
        //}

        //// GET: Items/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = await _context.Items.FindAsync(id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ItemQualityId"] = new SelectList(_context.ItemQualities, "Id", "Name", item.ItemQualityId);
        //    ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "Id", "Name", item.ItemTypeId);
        //    return View(item);
        //}

        //// POST: Items/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Name,Cost,isCovert,inStock,ItemTypeId,ItemQualityId,Quantity,Id,CreatedAt,UpdatedAt")] Item item)
        //{
        //    if (id != item.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(item);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ItemExists(item.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ItemQualityId"] = new SelectList(_context.ItemQualities, "Id", "Name", item.ItemQualityId);
        //    ViewData["ItemTypeId"] = new SelectList(_context.ItemTypes, "Id", "Name", item.ItemTypeId);
        //    return View(item);
        //}

        //// GET: Items/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var item = await _context.Items
        //        .Include(i => i.ItemQuality)
        //        .Include(i => i.ItemType)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (item == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(item);
        //}

        //// POST: Items/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var item = await _context.Items.FindAsync(id);
        //    _context.Items.Remove(item);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ItemExists(int id)
        //{
        //    return _context.Items.Any(e => e.Id == id);
        //}
    }
}
