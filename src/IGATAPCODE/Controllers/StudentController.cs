using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IGATAPCODE.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace IGATAPCODE.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public StudentController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        // GET: /Student/Index
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return View(_db.Students
                .Where(student => student.User.Id == userId)
                .ToList());
        }

        // GET: /Student/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Student/Create
        [HttpPost]
        public async Task<IActionResult> Create(string FirstName, string LastName, IFormFile picture)
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
            Student newStudent = new Student(FirstName, LastName, photoArray, currentUser);
            _db.Students.Add(newStudent);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /Student/Edit
        public IActionResult Edit(int id)
        {
            Student thisStudent = _db.Students.FirstOrDefault(im => im.Id == id);
            return View(thisStudent);
        }

        // POST: /Student/Edit
        [HttpPost]
        public IActionResult Edit(Student student, IFormFile picture)
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
            student.Picture = photoArray;
            _db.Entry(student).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
