using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VTYSProje.Controllers
{
    public class StudentsController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                List<Students> allStudents = db.Students.ToList();
                return PartialView(allStudents);
            }
        }

        public IActionResult CourseSelection()
        {
            return PartialView();
        }
    }
}
