using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Data.Context;

namespace proje.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            int currentUserId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));
            string currentUserRole = HttpContext?.Session?.GetString("CurrentRole");

            dynamic User = null;
            
            switch (currentUserRole)
            {
                case "Admin":

                    using(Db db = new Db())
                    {
                        Admin currentUser = db.Admin.Find(currentUserId);
                        User = currentUser;
                    }
                    break;
                case "Student":

                    using (Db db = new Db())
                    {
                        Students currentUser = db.Students.Find(currentUserId);
                        User = currentUser;
                    }
                    break;
                case "Teacher":

                    using (Db db = new Db())
                    {
                        Teacher currentUser = db.Teacher.Find(currentUserId);
                        User = currentUser;
                    }
                    break;
            }
            return PartialView(User);
        }
    }
}
