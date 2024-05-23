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
using MVC.Controllers.Bases;
using Microsoft.AspNetCore.Authorization;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class SongsController : MvcControllerBase
    {
        // TODO: Add service injections here
        private readonly ISongService _songService;
        private readonly IArtistService _artistService;

        public SongsController(ISongService songService, IArtistService artistService)
        {
            _songService = songService;
            _artistService = artistService;
        }

        // GET: Songs
        public IActionResult Index()
        {
            List<SongModel> songList = _songService.Query().ToList(); // TODO: Add get collection service logic here
            return View(songList);
        }

        // GET: Songs/Details/5
        public IActionResult Details(int id)
        {
            SongModel song = _songService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        [Authorize(Roles = "admin")]

        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["ArtistId"] = new SelectList(_artistService.Query().ToList(), "Id", "Name");
            return View();
        }

        // POST: Songs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create(SongModel song)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add insert service logic here
                Result result = _songService.Add(song);
                if (result.isSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = song.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["ArtistId"] = new SelectList(_artistService.Query().ToList(), "Id", "Name");
            return View(song);
        }

        // GET: Songs/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id)
        {
            SongModel song = _songService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (song == null)
            {
                return View("Error", "Song not found!");
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["ArtistId"] = new SelectList(_artistService.Query().ToList(), "Id", "Name");
            return View(song);
        }

        // POST: Songs/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public IActionResult Edit(SongModel song)
        {
            if (ModelState.IsValid)
            {
                // TODO: Add update service logic here
                Result result = _songService.Update(song);
                if (result.isSuccess)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = song.Id });
                }
                // TODO: Add update service logic here
                ModelState.AddModelError("", result.Message);
                
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["ArtistId"] = new SelectList(_artistService.Query().ToList(), "Id", "Name");

            return View(song);
        }

        // GET: Songs/Delete/5
        [Authorize(Roles = "admin")]

        public IActionResult Delete(int id)
        {
            SongModel song = _songService.Query().SingleOrDefault(s => s.Id == id); // TODO: Add get item service logic here
            if (song == null)
            {
                return NotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: Add delete service logic here
            Result result = _songService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
