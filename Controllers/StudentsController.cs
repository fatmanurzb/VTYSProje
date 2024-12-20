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
            using (Db db = new Db())
            {
                int userId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));
                List<Courses> allCourses = db.Courses.ToList();
                List<dynamic> allCoursesWithTeacherName = new List<dynamic>();

                foreach (Courses course in allCourses)
                {
                    TeacherCourses teacherCourses = db.TeacherCourses.Where(x=> x.CourseId == course.CourseId).FirstOrDefault();

                    if (teacherCourses != null)
                    {
                        Teacher courseTeacher = db.Teacher.Find(teacherCourses.TeacherId);

                        if (courseTeacher != null)
                        {
                            allCoursesWithTeacherName.Add(new
                            {
                                CourseId = course.CourseId,
                                CourseName = course.CourseName,
                                CourseCredit = course.CourseCredit,
                                CourseType = course.CourseType,
                                TeacherName = courseTeacher.Name + " " + courseTeacher.Surname
                            });
                        }
                    }
                }

                List<StudentCourses> stundentCoursesIdList = db.StudentCourses.Where(x=> x.StudentId == userId).ToList();
                List<dynamic> studentCourses = new List<dynamic>();

                foreach(StudentCourses courseId in stundentCoursesIdList)
                {
                    Courses course = db.Courses.Find(courseId.CourseId);
                    
                    if(course != null)
                    {
                        TeacherCourses teacherId = db.TeacherCourses.Where(x=> x.CourseId == courseId.CourseId).FirstOrDefault();

                        if(teacherId != null)
                        {
                            Teacher courseTeacher = db.Teacher.Find(teacherId.TeacherId);

                            if(course == null)
                            {
                                studentCourses.Add(new
                                {
                                    CourseId = course.CourseId,
                                    CourseName = course.CourseName,
                                    CourseCredit = course.CourseCredit,
                                    CourseType = course.CourseType,
                                    TeacherName = courseTeacher.Name + " " + courseTeacher.Surname
                                });
                            }
                        }
                    }
                }
                return PartialView(new
                {
                    AllCourses = allCoursesWithTeacherName,
                    StudentCourses = studentCourses
                });
            }
        }
    }
}
