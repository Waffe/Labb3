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
using WorkoutPlanner.Data.Entities.EfManyToMany;
using WorkoutPlanner.MVC.Models;


namespace WorkoutPlanner.MVC.Controllers
{
    public class ProgramsController : Controller
    {
        private readonly WorkoutPlannerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProgramsController(WorkoutPlannerContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Programs
        public async Task<IActionResult> Index()
        {
            var workoutPlannerContext = _context.Programs.Include(p => p.Profile);
            return View(await workoutPlannerContext.ToListAsync());
        }

        // GET: Programs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs
                .Include(p => p.Profile).Include(x=>x.WorkoutPlans).ThenInclude(x=>x.Workout)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (program == null)
            {
                return NotFound();
            }
            program.WorkoutPlans = program.WorkoutPlans.OrderBy(x => x.Week).ThenBy(x => x.DayOfWeek).ToList();
            return View(program);
        }

        // GET: Programs/Create
        public async Task<IActionResult> Create()
        {
            if (await GetCurrentUserAsync() == null)
            {
                return RedirectToAction("Login", "Account");
            }

            return View();
        }

        // POST: Programs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,Name,Difficulty,ProfileId")] WorkoutPlanner.Data.Entities.Program program)
        {
            var currentAppuserId = (await GetCurrentUserAsync()).ProfileId;
            program.Profile = _context.Profiles.FirstOrDefault(x => x.Id == currentAppuserId);
            if (ModelState.IsValid)
            {
                _context.Add(program);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(program);
        }

        // GET: Programs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.Workouts = _context.Workouts.ToList();

            var program = await _context.Programs.Include(x=>x.WorkoutPlans).SingleOrDefaultAsync(m => m.Id == id);
            

            if (program == null)
            {
                return NotFound();
            }
            program.WorkoutPlans = program.WorkoutPlans.OrderBy(x => x.Week).ThenBy(x=>x.DayOfWeek).ToList();
            return View(program);
        }

        // POST: Programs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Name,Difficulty,ProfileId")] WorkoutPlanner.Data.Entities.Program program)
        {
            if (id != program.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(program);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramExists(program.Id))
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
            return View(program);
        }

        // GET: Programs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var program = await _context.Programs
                .Include(p => p.Profile)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (program == null)
            {
                return NotFound();
            }

            return View(program);
        }

        // POST: Programs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var program = await _context.Programs.SingleOrDefaultAsync(m => m.Id == id);
            _context.Programs.Remove(program);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProgramExists(int id)
        {
            return _context.Programs.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult AddWorkout(int workoutId, int programId, int week, DayOfWeek dayOfWeek)
        {

            using (var apa = _context)
            {
                var programToBeUpdated = apa.Programs.Include(x => x.WorkoutPlans).ThenInclude(x => x.Workout).FirstOrDefault(x => x.Id == programId);
                var workout = apa.Workouts.FirstOrDefault(x => x.Id == workoutId);
                var program = apa.Programs.FirstOrDefault(x => x.Id == programId);

                var workoutPlan = new WorkoutPlan() { Workout = workout, Program = program, WorkoutId = workout.Id, ProgramId = program.Id, Week = week, DayOfWeek = dayOfWeek };



                programToBeUpdated.WorkoutPlans.Add(workoutPlan);
                apa.Update(programToBeUpdated);
                apa.SaveChanges();

            }

            return RedirectToAction("Edit", new { id = programId });
        }

        public async Task<IActionResult> DeleteWorkoutplan(int id, int programid)
        {
            var workout = await _context.WorkoutPlans.SingleOrDefaultAsync(m => m.Id == id);
            _context.WorkoutPlans.Remove(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = programid });
        }
    }
}
