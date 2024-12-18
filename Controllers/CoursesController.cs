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

        public IActionResult CoursesDetay(int Id)
        {
            using (Db db = new Db())
            {
                Courses courses = db.Courses.Find(Id);
                List<TeacherCourses> teacherCoursesIds = db.TeacherCourses.Where(x => x.CourseId == Id).ToList();

                List<Teacher> teacherCourses = new List<Teacher>();

                foreach (TeacherCourses teacherCourseId in teacherCoursesIds)
                {
                    Teacher teacher = db.Teacher.Find(teacherCourseId.TeacherId);

                    if (courses! == null)
                    {
                        teacherCourses.Add(teacher);
                    }
                }
                return PartialView(new
                {
                    CoursesInfo = courses,
                    Teachers = teacherCourses
                });
            }
        }
    }
}
