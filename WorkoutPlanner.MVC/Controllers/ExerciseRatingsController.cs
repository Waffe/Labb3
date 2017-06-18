using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data.Data;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Controllers
{
    public class ExerciseRatingsController : Controller
    {
        private readonly WorkoutPlannerContext _context;

        public ExerciseRatingsController(WorkoutPlannerContext context)
        {
            _context = context;    
        }

        // GET: ExerciseRatings
        public async Task<IActionResult> Index()
        {
            var workoutPlannerContext = _context.ExerciseRatings.Include(e => e.Exercise).Include(e => e.Profile);
            return View(await workoutPlannerContext.ToListAsync());
        }

        // GET: ExerciseRatings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseRating = await _context.ExerciseRatings
                .Include(e => e.Exercise)
                .Include(e => e.Profile)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exerciseRating == null)
            {
                return NotFound();
            }

            return View(exerciseRating);
        }

        // GET: ExerciseRatings/Create
        public IActionResult Create()
        {
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Name");
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id");
            return View();
        }

        // POST: ExerciseRatings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rate,Comment,ProfileId,ExerciseId")] ExerciseRating exerciseRating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exerciseRating);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Name", exerciseRating.ExerciseId);
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", exerciseRating.ProfileId);
            return View(exerciseRating);
        }

        // GET: ExerciseRatings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseRating = await _context.ExerciseRatings.SingleOrDefaultAsync(m => m.Id == id);
            if (exerciseRating == null)
            {
                return NotFound();
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Name", exerciseRating.ExerciseId);
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", exerciseRating.ProfileId);
            return View(exerciseRating);
        }

        // POST: ExerciseRatings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rate,Comment,ProfileId,ExerciseId")] ExerciseRating exerciseRating)
        {
            if (id != exerciseRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseRatingExists(exerciseRating.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ExerciseId"] = new SelectList(_context.Exercises, "Id", "Name", exerciseRating.ExerciseId);
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "Id", "Id", exerciseRating.ProfileId);
            return View(exerciseRating);
        }

        // GET: ExerciseRatings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseRating = await _context.ExerciseRatings
                .Include(e => e.Exercise)
                .Include(e => e.Profile)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exerciseRating == null)
            {
                return NotFound();
            }

            return View(exerciseRating);
        }

        // POST: ExerciseRatings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exerciseRating = await _context.ExerciseRatings.SingleOrDefaultAsync(m => m.Id == id);
            _context.ExerciseRatings.Remove(exerciseRating);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ExerciseRatingExists(int id)
        {
            return _context.ExerciseRatings.Any(e => e.Id == id);
        }
    }
}
