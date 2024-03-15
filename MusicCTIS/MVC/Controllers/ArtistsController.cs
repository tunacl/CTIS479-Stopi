#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Contexts;
using DataAccess.Entities;
using Business.Models;
using Business.Services;
using DataAccess.Results.Bases;
using System.Data;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class ArtistsController : Controller
    {
        // TODO: Add service injections here
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        // GET: Artists
        public IActionResult Index()
        {
            List<ArtistModel> artistList = _artistService.Query().ToList(); // TODO: Add get collection service logic here
            return View(artistList);
        }

        // GET: Artists/Details/5
        public IActionResult Details(int id)
        {
            ArtistModel artist = _artistService.Query().SingleOrDefault(a => a.Id == id); // TODO: Add get item service logic here
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // GET: Artists/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View();
        }

        // POST: Artists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArtistModel artist)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                Result result = _artistService.Add(artist);
                if (result.isSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);

            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(artist);
        }

        // GET: Artists/Edit/5
        public IActionResult Edit(int id)
        {
            ArtistModel artist = _artistService.Query().SingleOrDefault(a => a.Id == id); // TODO: Add get item service logic here
            if (artist == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(artist);
        }

        // POST: Artists/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArtistModel artist)
        {
            if (ModelState.IsValid)
            {
            Result result = _artistService.Update(artist);
                if (result.isSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                // TODO: Add update service logic here
                ModelState.AddModelError("", result.Message);

            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            return View(artist);
        }

        // GET: Artists/Delete/5
        public IActionResult Delete(int id)
        {
            ArtistModel artist = _artistService.Query().SingleOrDefault(a => a.Id == id); // TODO: Add get item service logic here
            if (artist == null)
            {
                return NotFound();
            }
            return View(artist);
        }

        // POST: Artists/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _artistService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));

        }
	}
}
