using Microsoft.AspNetCore.Mvc;
using Data.Context;
using Data.Entities;

namespace VTYSProje.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            using (Db db = new Db())
            {
                List<Teacher> allTeacher = db.Teacher.ToList();
                return PartialView(allTeacher);
            }
        }

        public IActionResult TeacherDetay(int Id) 
        {
            using (Db db = new Db())
            {
                Teacher teacher = db.Teacher.Find(Id);
                List<TeacherCourses> teacherCoursesIds = db.TeacherCourses.Where(x => x.TeacherId == Id).ToList();

                List<Courses> teacherCourses = new List<Courses>();

                foreach (TeacherCourses teacherCourseId in teacherCoursesIds)
                {
                    Courses courses = db.Courses.Find(teacherCourseId.CourseId);

                    if (courses != null) 
                    { 
                        teacherCourses.Add(courses);
                    }
                }
                return PartialView(new
                {
                    TeacherInfo = teacher,
                    Course = teacherCourses
                });
            }
        }
    }
}
