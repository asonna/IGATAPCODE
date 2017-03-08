using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IGATAPCODE.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace IGATAPCODE.Controllers
{
    [Authorize]
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public TeacherController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        // GET: /IGATAPCODE/Index
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View(_db.Teachers
                .Where(teacher => teacher.User.Id == userId)
                .ToList());
        }

        // GET: /IGATAPCODE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /IGATAPCODE/Create
        [HttpPost]
        public async Task<IActionResult> Create(string FirstName, string LastName, string Bio, IFormFile picture)
        {
            byte[] photoArray = new byte[0];
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var currentUser = await _userManager.FindByIdAsync(userId);

            if (picture.Length > 0)
            {
                using (var fileStream = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    photoArray = ms.ToArray();
                }
            }
            Teacher newTeacher = new Teacher(FirstName, LastName, Bio, photoArray, currentUser);
            _db.Teachers.Add(newTeacher);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /IGATAPCODE/Edit
        public IActionResult Edit(int id)
        {
            Teacher thisTeacher = _db.Teachers.FirstOrDefault(im => im.Id == id);
            return View(thisTeacher);
        }

        // POST: /IGATAPCODE/Edit
        [HttpPost]
        public IActionResult Edit(Teacher teacher, IFormFile picture)
        {
            byte[] photoArray = new byte[0];

            if (picture.Length > 0)
            {
                using (var fileStream = picture.OpenReadStream())
                using (var ms = new MemoryStream())
                {
                    fileStream.CopyTo(ms);
                    photoArray = ms.ToArray();
                }
            }
            teacher.Picture = photoArray;
            _db.Entry(teacher).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
