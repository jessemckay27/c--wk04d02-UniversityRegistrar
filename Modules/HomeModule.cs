using Nancy;
using System.Collections.Generic;
using System;
using Registrar.Objects;

namespace Registrar
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/courses"] = _ => {
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };

      Get["/students"] = _ => {
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };

      Get["/students/new"] = _ => {
        return View["student_form.cshtml"];
      };

      Post["/students/new"] = _ => {
        string studentName = Request.Form["student-name"];
        string studentDate = Request.Form["student-date"];
        Student newStudent = new Student(studentName, studentDate);
        newStudent.Save();
        return View["student.cshtml", newStudent];
      };

      Get["/courses/new"] = _ => {
        return View["course_form.cshtml"];
      };

      Post["/courses/new"] = _ => {
        string courseName = Request.Form["course-name"];
        string courseNumber = Request.Form["course-number"];
        Course newCourse = new Course(courseName, courseNumber);
        newCourse.Save();
        return View["course.cshtml", newCourse];
      };

      Get["/student/{student_id}"] = parameters => {
        Student selectedStudent = Student.Find(parameters.student_id);
        return View["student.cshtml", selectedStudent];
      };

      Get["/course/{course_id}"] = parameters => {
        Course selectedCourse = Course.Find(parameters.course_id);
        return View["course.cshtml", selectedCourse];
      };

      Get["/course/{id}/students/add"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Course selectedCourse = Course.Find(parameters.id);
        List<Student> currentStudents = selectedCourse.GetStudents();
        List<Student> allStudents = Student.GetAll();

        List<Student> availableStudents = new List<Student> {};
        foreach(Student student in allStudents)
        {
          // If a student is already in the course it will return an index of 0 or above.
          if(currentStudents.IndexOf(student) < 0)
          {
            availableStudents.Add(student);
          }
        }
        model.Add("course", selectedCourse);
        model.Add("available-students", availableStudents);
        return View["AddStudentToCourse.cshtml", model];
      };

      Post["/course/{id}/students/added"] = parameters => {
        int studentId = Request.Form["student-id"];
        Student selectedStudent = Student.Find(studentId);

        Course selectedCourse = Course.Find(parameters.id);
        selectedCourse.AddStudent(selectedStudent);

        return View["student_added.cshtml", selectedCourse];
      };

      Get["/student/{id}/courses/add"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object> ();
        Student selectedStudent = Student.Find(parameters.id);
        List<Course> currentCourses = selectedStudent.GetCourses();
        List<Course> allCourses = Course.GetAll();

        List<Course> availableCourses = new List<Course> {};
        foreach(Course course in allCourses)
        {
          // If the student is already enrolled in a course it will return an index of 0 or above.
          if(currentCourses.IndexOf(course) < 0)
          {
            availableCourses.Add(course);
          }
        }
        model.Add("student", selectedStudent);
        model.Add("available-courses", availableCourses);
        return View["AddCourseToStudent.cshtml", model];
      };

      Post["/student/{id}/courses/added"] = parameters => {
        int courseId = Request.Form["course-id"];
        Course selectedCourse = Course.Find(courseId);

        Student selectedStudent = Student.Find(parameters.id);
        selectedStudent.AddCourse(selectedCourse);

        return View["course_added.cshtml", selectedStudent];
      };

    }
  }
}
