using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WorkoutPlanner.MVC.Data;
using WorkoutPlanner.MVC.Models;
using Microsoft.AspNetCore.Identity;
using WorkoutPlanner.Data.Data;
using WorkoutPlanner.Data.Entities;
using WorkoutPlanner.MVC.Models.AuthProfileViewModels;

namespace WorkoutPlanner.MVC.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly WorkoutPlannerContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfilesController(WorkoutPlannerContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Profiles.ToListAsync());
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: Profiles/Details/5  //// Delete this after labb2
        public async Task<IActionResult> Details(int? id)
        {
            var user = await GetCurrentUserAsync() ?? new ApplicationUser();

            if (id == null)
            {
                return NotFound();
            }


            var profile = await _context.Profiles
                .SingleOrDefaultAsync(m => m.Id == id);

            if (profile == null)
            {
                return NotFound();
            }

            var profileViewModel = new ProfileAuthViewModel
            {
                Profile = profile,
                IsAuthor = user.ProfileId == id,
                Workouts = _context.Workouts.Where(w=>w.ProfileId == id)
            };


            return View(profileViewModel);
        }

        //REAL DETAILS - How its supposed to work
        public async Task<IActionResult> Detailss()
        {
            var user = await GetCurrentUserAsync();
            var id = user?.ProfileId;
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return RedirectToAction("Details", new {id = profile.Id});
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegistrationDate,Name,DateOfBirth,Weight,Lenght")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit()
        {
            var user = await GetCurrentUserAsync();
            var id = user?.ProfileId;
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegistrationDate,Name,DateOfBirth,Weight,Lenght")] Profile profile)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }                
            }
            return View(profile);
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .SingleOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.SingleOrDefaultAsync(m => m.Id == id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.Id == id);
        }
    }
}
