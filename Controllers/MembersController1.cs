using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsClubManagement_SPA_Muster_details.Data;
using SportsClubManagement_SPA_Muster_details.Models.ViewModel;
using SportsClubManagement_SPA_Muster_details.Models;

namespace SportsClubManagement_SPA_Muster_details.Controllers
{
    public class MembersController1 : Controller


    {

       private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _Em;
        public MembersController1(ApplicationDbContext db, IWebHostEnvironment em)
        {
            _db = db;
            _Em = em;
        }

        public IActionResult Index()
        {

            var MemberInfo=_db.Members.Include(x=>x.Enrollments).ThenInclude(a=>a.Activity).ToList();
            return View(MemberInfo);
        }

        public IActionResult AddNewActivity()
        {
            ViewBag.activity = new SelectList(_db.Activities, "ActivityID", "ActivityName");
            return PartialView("_AddNewActivity");
        }

        public IActionResult AddMember()
        {
            ViewBag.activity = new SelectList(_db.Activities, "ActivityID", "ActivityName");
            return PartialView("_AddMember");
        }
        [HttpPost]
        public IActionResult _AddMember(MemberVm model)
        {
            if (ModelState.IsValid)
            {
                string? picturePath = null;

                // Save the uploaded picture if present
                if (model.PicturePath != null)
                {
                    string uploadsFolder = Path.Combine(_Em.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.PicturePath.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.PicturePath.CopyTo(stream);
                    }

                    picturePath = $"/uploads/{uniqueFileName}";
                }

                // Create the Member object
                var member = new Member
                {
                    Name = model.Name,
                    Age = model.Age,
                    Email = model.Email,
                    DOB = model.DOB,
                    MorningShift = model.MorningShift,
                    Picture = picturePath
                };

                // Save the member to the database
                _db.Members.Add(member);
                _db.SaveChanges();

                // Save enrollments
                foreach (var activity in model.ActivityList)
                {
                    _db.Enrollments.Add(new Enrollment
                    {
                        MemberID = member.MemberID,
                        ActivityID = activity
                    });
                }

                _db.SaveChanges();
                TempData["Message"] = "Data inserted successfully!";
                return RedirectToAction("Index");
            }

            // Reload the activities for the dropdown or multi-select view
            ViewBag.activity = new SelectList(_db.Activities, "ActivityID", "ActivityName");
            return PartialView("_AddMember", model);
        }

    }
}
