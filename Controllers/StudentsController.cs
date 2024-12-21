using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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

        public IActionResult Transcript()
        {
            using(Db db = new Db())
            {
                int userId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));
                Students currentStudent = db.Students.Find(userId);

                List<StudentCourses> studentCoursesIds = db.StudentCourses.Where(x => x.StudentId == currentStudent.StudentId).ToList();
                List<dynamic> studentCourses = new List<dynamic>();

                if(currentStudent.isCourseSelectionConfirmed)
                {
                    foreach (StudentCourses studentCourseId in studentCoursesIds) 
                    {
                        Courses studentCourse = db.Courses.Find(studentCourseId.CourseId);

                        if(studentCourse != null)
                        {
                            Teacher courseTeacher = db.Teacher.Find(studentCourse.CourseId);

                            if (courseTeacher != null)
                            {
                                studentCourses.Add(new
                                {
                                    CourseId = studentCourse.CourseId,
                                    CourseName = studentCourse.CourseName,
                                    CourseCredit = studentCourse.CourseCredit,
                                    CourseType = studentCourse.CourseType,
                                    TeacherName = courseTeacher.Name + " " + courseTeacher.Surname
                                });
                            }
                        }
                    }
                }
                return PartialView(studentCourses);
            }
        }

        public IActionResult CourseSelection()
        {
            using(Db db = new Db())
            {
                int userId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));
                Students currentStudent = db.Students.Find(userId);
                List<StudentCourses> studentCoursesIdList = db.StudentCourses.Where(x=> x.StudentId == userId).ToList();
                List<Courses> allCourses = db.Courses.ToList();
                List<dynamic> allCoursesWithTeacherName = new List<dynamic>();

                foreach(Courses course in allCourses)
                {
                    TeacherCourses teacherCourses = db.TeacherCourses.Where(x=> x.CourseId == course.CourseId).FirstOrDefault();

                    if(teacherCourses != null)
                    {
                        Teacher courseTeacher = db.Teacher.Find(teacherCourses.TeacherId);

                        if (courseTeacher != null)
                        {
                            if(!studentCoursesIdList.Any(x=> x.CourseId == course.CourseId))
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
                }

                List<dynamic> studentCourses = new List<dynamic>();

                foreach (StudentCourses courseId in studentCoursesIdList)
                {
                    Courses course = db.Courses.Find(courseId.CourseId);

                    if(course != null)
                    {
                        TeacherCourses teacherId = db.TeacherCourses.Where(x=> x.CourseId == courseId.CourseId).FirstOrDefault();

                        if(teacherId != null)
                        {
                            Teacher courseTeacher = db.Teacher.Find(teacherId.TeacherId);

                            if(course != null)
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
                    StudentCourses = studentCourses,
                    isConfirmed = currentStudent.isCourseSelectionConfirmed
                });
            }
        }

        public IActionResult SelectCourse(string SelectedCoursesIds)
        {
            List<int> selectedCoursesIds = JsonSerializer.Deserialize<List<int>>(SelectedCoursesIds);
            int currentStudentId = Convert.ToInt32(HttpContext?.Session?.GetInt32("CurrentUserId"));

            using (Db db = new Db())
            {
                List<Courses> requiredCourses = db.Courses.Where(x => x.CourseType == true).ToList();
                List<TeacherCourses> allCoursesIdsWithTeachers = db.TeacherCourses.ToList();

                for (int i = requiredCourses.Count - 1; i >= 0; i--)
                {
                    Courses requiredCourse = requiredCourses[i];

                    if (!allCoursesIdsWithTeachers.Any(x => x.CourseId == requiredCourse.CourseId))
                    {
                        requiredCourses.RemoveAt(i);
                    }
                }
                bool allReaquiredCoursesSelected = true;

                foreach (Courses requiredCourse in requiredCourses)
                {
                    if (!selectedCoursesIds.Contains(requiredCourse.CourseId))
                    {
                        allReaquiredCoursesSelected = false;
                        break;
                    }
                }

                if (!allReaquiredCoursesSelected)
                {
                    return Json(new { success = false, message = "Bütün zorunlu dersler seçilmelidir!" });
                }

                if (allReaquiredCoursesSelected)
                {
                    foreach(int courseId in selectedCoursesIds)
                    {
                        Courses selectedCourse = db.Courses.Find(courseId);

                        if(selectedCourse != null)
                        {
                            StudentCourses studentCourse = new StudentCourses
                            {
                                CourseId = selectedCourse.CourseId,
                                StudentId = currentStudentId
                            };

                            db.StudentCourses.Add(studentCourse);
                        }
                    }
                    db.SaveChanges();
                }
                return Json(new { success = true, message = "Başarıyla kayıt edildi!" });
            }
        }

    }
}
