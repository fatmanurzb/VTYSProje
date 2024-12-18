using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VTYSProje.Controllers
{
    public class CoursesController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                List<Courses> allCourses = db.Courses.ToList();
                return PartialView(allCourses);
            }
        }
    }
}
