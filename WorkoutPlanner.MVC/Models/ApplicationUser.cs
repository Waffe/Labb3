using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WorkoutPlanner.Data.Entities;

namespace WorkoutPlanner.MVC.Models
{
    // Add profile data for application users by adding properties to the Profile class
    public class ApplicationUser : IdentityUser
    {
        public int ProfileId { get; set; }
        public Profile Profile { get; set; }

    }
}
