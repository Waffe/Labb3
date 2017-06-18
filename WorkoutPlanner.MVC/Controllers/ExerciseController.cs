using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.Data.Data;
using WorkoutPlanner.Data.Entities;
using WorkoutPlanner.MVC.Data;
using WorkoutPlanner.MVC.Models;
using WorkoutPlanner.MVC.Models.AuthProfileViewModels;

namespace WorkoutPlanner.MVC.Controllers
{
    public class ExerciseController : Controller
    {
        private readonly WorkoutPlannerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _appContext;

        public ExerciseController(WorkoutPlannerContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext appContext)
        {
            _context = context;
            _userManager = userManager;
            _appContext = appContext;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Exercise
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync() ?? new ApplicationUser();
            var listOfExercises = await _context.Exercises.Include(e => e.Workouts).Include(x => x.Profile).ToListAsync();
            var listOfExerciseViewmodels = new List<ExerciseAuthViewModel>();
            foreach (var exercise in listOfExercises)
            {
                listOfExerciseViewmodels.Add(new ExerciseAuthViewModel() { Exercise = exercise, IsAuthor = user.ProfileId == exercise.ProfileId, });
            }

            return View(listOfExerciseViewmodels);
        }

        // GET: Exercise/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.Include(e=>e.Profile)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercise/Create
        public async Task<IActionResult> Create()
        {
            if (await GetCurrentUserAsync() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Exercise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Instructions,Video")] Exercise exercise)
        {

            if (ModelState.IsValid)
            {
                if (await GetCurrentUserAsync() != null)
                {
                    var currentAppuserId = (await GetCurrentUserAsync()).ProfileId;
                    exercise.Profile = _context.Profiles.FirstOrDefault(x => x.Id == currentAppuserId);
                }
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(exercise);
        }

        // GET: Exercise/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await GetCurrentUserAsync() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            return View(exercise);
        }

        // POST: Exercise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Instructions,Video,ProfileId")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            return View(exercise);
        }

        // GET: Exercise/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (await GetCurrentUserAsync() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.Id == id);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
