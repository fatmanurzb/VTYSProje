using Microsoft.AspNetCore.Mvc;
using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Http;

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

        public IActionResult StudentCourseSelectConfirmPage()
        {
            using (Db db = new Db())
            {
                string sessionId = HttpContext?.Session?.GetInt32("CurrentUserId").ToString();
                int userId = Convert.ToInt32(sessionId);

                Teacher teacher = db.Teacher.Find(userId);
                List<Students> isInstructor = db.Students.Where(s=> s.InstructorId == teacher.TeacherId && s.isCourseSelectionConfirmed == false).ToList();
                List<Students> studentsSelectedCourse = new List<Students>();   

                foreach(Students student in isInstructor)
                {
                    List<StudentCourses> studentCourses = db.StudentCourses.Where(x=> x.StudentId == student.StudentId).ToList();
                    if(studentCourses.Count> 0)
                    {
                        studentsSelectedCourse.Add(student);
                    }

                }
                return PartialView(studentsSelectedCourse);
            }
        }

        [HttpPost]
        public IActionResult StudentCourseConfirm(int Id)
        {
            using (Db db = new Db())
            {
                Students confirmedStudent = db.Students.Find(Id);
                if (confirmedStudent != null)
                {
                    StudentCourses confirmedCourses = db.StudentCourses.Where(s => s.StudentId == confirmedStudent.StudentId).FirstOrDefault();

                    confirmedStudent.isCourseSelectionConfirmed = true;

                    db.SaveChanges();
                    return Json(new { success = true, message = "Öğrenci ders seçimi onaylandı!"});
                }
            }

            return Json(new { success = false, message = "Bir sorun oluştu!"});
        }

        [HttpPost]
        public IActionResult StudentCourseReject(int Id)
        {
            using (Db db = new Db())
            {
                Students confirmedStudent = db.Students.Find(Id);
                if (confirmedStudent != null)
                {
                    List<StudentCourses> confirmedCourses = db.StudentCourses.Where(s => s.StudentId == confirmedStudent.StudentId).ToList();
                    db.StudentCourses.RemoveRange(confirmedCourses);

                    db.SaveChanges();
                    return Json(new { success = true, message = "Öğrenci ders seçimi reddedildi!" });
                }
            }

            return Json(new { success = false, message = "Bir sorun oluştu!" });
        }
    }
}
