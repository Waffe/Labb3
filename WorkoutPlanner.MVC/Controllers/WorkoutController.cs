using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WorkoutPlanner.Data.Data;
using WorkoutPlanner.Data.Entities;
using WorkoutPlanner.Data.Entities.EfManyToMany;
using WorkoutPlanner.MVC.Data;
using WorkoutPlanner.MVC.Models;
using WorkoutPlanner.MVC.Models.WorkoutViewModels;

namespace WorkoutPlanner.MVC.Controllers
{
    public class WorkoutController : Controller
    {
        private readonly WorkoutPlannerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _appContext;


        public WorkoutController(WorkoutPlannerContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext appContext)
        {
            _context = context;
            _userManager = userManager;
            _appContext = appContext;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Workout
        public async Task<IActionResult> Index()
        {
            var listOfWorkouts = await _context.Workouts.Include(x=>x.Profile).ToListAsync();

                
            return View(listOfWorkouts);
        }

        // GET: Workout/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var workout = await _context.Workouts.Include(x=>x.Profile).Include(x=>x.Exercises).ThenInclude(x=>x.Exercise)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // GET: Workout/Create
        public async Task<IActionResult> Create()
        {
            if (await GetCurrentUserAsync() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Workout/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProfileId")] Workout workout)
        {
            var testwork = workout;

            var currentAppuserId = (await GetCurrentUserAsync()).ProfileId;
            testwork.Profile = _context.Profiles.FirstOrDefault(x => x.Id == currentAppuserId);
            if (ModelState.IsValid)
            {
                _context.Workouts.Add(testwork);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workout);
        }

        // GET: Workout/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (await GetCurrentUserAsync() == null)
            {
                return RedirectToAction("Login", "Account");
            }
            Workout workout;
            ViewBag.Exercises = _context.Exercises.ToList();
            if (id == null)
            {
                return NotFound();
            }
            using (var context = _context)
            {
                workout = await context.Workouts.Include(x=>x.Exercises).SingleOrDefaultAsync(m => m.Id == id);
            }
                
            if (workout == null)
            {
                return NotFound();
            }
            return View(workout);
        }

        // POST: Workout/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProfileId")] Workout workout)
        {
            //ViewBag.Workouts = _context.Exercises.ToList();
            if (id != workout.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (var context = _context)
                    {
                        context.Update(workout);
                        await context.SaveChangesAsync();
                    }                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkoutExists(workout.Id))
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
            return View(workout);
        }

        // GET: Workout/Delete/5
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

            var workout = await _context.Workouts
                .SingleOrDefaultAsync(m => m.Id == id);
            if (workout == null)
            {
                return NotFound();
            }

            return View(workout);
        }

        // POST: Workout/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workout = await _context.Workouts.SingleOrDefaultAsync(m => m.Id == id);
            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool WorkoutExists(int id)
        {
            return _context.Workouts.Any(e => e.Id == id);
        }

        [HttpPost]
        public IActionResult AddExercise(int workoutId, int exerciseId)
        {

            using (var apa = _context)
            {
                var workoutToBeUpdated = apa.Workouts.Include(x=>x.Exercises).ThenInclude(x=>x.Exercise).FirstOrDefault(x => x.Id == workoutId);
                var workout = apa.Workouts.FirstOrDefault(x => x.Id == workoutId);
                var exercise = apa.Exercises.FirstOrDefault(x => x.Id == exerciseId);
                var workoutExercise = new WorkoutExercises() { Workout=workout, Exercise = exercise,WorkoutId = workout.Id,ExerciseId = exercise.Id};
                if (!workoutToBeUpdated.Exercises.Any(x=>x.WorkoutId == workoutExercise.WorkoutId && x.ExerciseId == workoutExercise.ExerciseId))
                {
                    workoutToBeUpdated.Exercises.Add(workoutExercise);
                    apa.Update(workoutToBeUpdated);
                    apa.SaveChanges();
                }
            }

            return RedirectToAction("Edit",new {id = workoutId});
        }
    }
}
