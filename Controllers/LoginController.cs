using Microsoft.AspNetCore.Mvc;
using Data.Context;
using Data.Entities;


namespace VTYSProje.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Entering(string email, string password) {
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password)) {
                using (Db db = new Db())
                {
                    List<Admin> isAdmin = db.Admin.Where(x => x.Email == email && x.Password == password).ToList();
                    List<Students> isStudent = db.Students.Where(x => x.Email == email && x.Password == password).ToList();
                    if (isAdmin.Count > 0)
                    {
                        Admin currentUser = isAdmin[0];
                        HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                        HttpContext?.Session?.SetString("Job", currentUser.Role);
                        HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.AdminId);
                        HttpContext?.Session?.SetString("CurrentRole", "Admin");

                        return RedirectToAction("Index", "Home");
                    }
                    else if (isStudent.Count > 0)
                        {
                            Students currentUser = isStudent[0];
                            HttpContext?.Session?.SetString("Name", currentUser.Name + " " + currentUser.Surname);
                            HttpContext?.Session?.SetString("Job", currentUser.Role);
                            HttpContext?.Session?.SetInt32("CurrentUserId", currentUser.StudentId);
                            HttpContext?.Session?.SetString("CurrentRole", "Student");

                            return RedirectToAction("Index", "Home");
                        }
                }
            }

            return RedirectToAction("Index","Login");
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Json(new { redirectUrl = Url.Action("Index", "Login")});
        }
    }
}
