using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data.Data;
using WorkoutPlanner.Data.Entities;
using WorkoutPlanner.MVC.Data;
using WorkoutPlanner.MVC.Models;

namespace WorkoutPlanner.MVC.Controllers
{
    public class WorkoutRatingController : Controller
    {
        private readonly WorkoutPlannerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public WorkoutRatingController(WorkoutPlannerContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: WorkoutRating
        public async Task<IActionResult> Index()
        {
            var workoutPlannerContext = _context.WorkoutRatings.Include(w => w.Workout).Include(x=>x.Profile);
            return View(await workoutPlannerContext.ToListAsync());
        }
        //Universal access to CurrentUser
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: WorkoutRating/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutRating = await _context.WorkoutRatings
                .Include(w => w.Workout).Include(x=>x.Profile)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workoutRating == null)
            {
                return NotFound();
            }

            return View(workoutRating);
        }

        // GET: WorkoutRating/Create
        public IActionResult Create()
        {
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "Name");
            return View();
        }

        // POST: WorkoutRating/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rate,Comment,WorkoutId")] WorkoutRating workoutRating)
        {
            if (ModelState.IsValid)
            {
                if (await GetCurrentUserAsync() != null)
                {
                    var currentAppUserId = (await GetCurrentUserAsync()).ProfileId;
                    workoutRating.Profile = _context.Profiles.FirstOrDefault(x=>x.Id == currentAppUserId);
                }
                _context.Add(workoutRating);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "Name", workoutRating.WorkoutId);
         
            return View(workoutRating);
        }

        // GET: WorkoutRating/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutRating = await _context.WorkoutRatings.Include(x=>x.Profile).Include(x=>x.Workout).SingleOrDefaultAsync(m => m.Id == id);
            if (workoutRating == null)
            {
                return NotFound();
            }
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "Name", workoutRating.WorkoutId);
            return View(workoutRating);
        }

        // POST: WorkoutRating/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rate,Comment,WorkoutId")] WorkoutRating workoutRating)
        {
            if (id != workoutRating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workoutRating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutRatingExists(workoutRating.Id))
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
            ViewData["WorkoutId"] = new SelectList(_context.Workouts, "Id", "Name", workoutRating.WorkoutId);
            return View(workoutRating);
        }

        // GET: WorkoutRating/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var workoutRating = await _context.WorkoutRatings
                .Include(w => w.Workout)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workoutRating == null)
            {
                return NotFound();
            }

            return View(workoutRating);
        }

        // POST: WorkoutRating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workoutRating = await _context.WorkoutRatings.SingleOrDefaultAsync(m => m.Id == id);
            _context.WorkoutRatings.Remove(workoutRating);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WorkoutRatingExists(int id)
        {
            return _context.WorkoutRatings.Any(e => e.Id == id);
        }
    }
}
