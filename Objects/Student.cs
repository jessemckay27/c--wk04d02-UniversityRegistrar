// using System;
// using System.Collections.Generic;
// using System.Data.SqlClient;
//
// namespace Registrar.Objects
// {
//   public class Student
//   {
//
//     public int Id {get; set;}
//     public string Name {get; set;}
//     public string Date {get; set;}
// 
//     public Student(string name, string date, int id = 0)
//     {
//       this.Id = id;
//       this.Name = name;
//       this.Date = date;
//     }
//
//     public override bool Equals(System.Object otherStudent)
//     {
//       if (!(otherStudent is Student))
//       {
//         return false;
//       }
//       else
//       {
//         Student newStudent = (Student) otherStudent;
//         bool idEquality = (this.Id == newStudent.Id);
//         bool nameEquality = (this.Name == newStudent.Name);
//         bool dateEquality = (this.Date == newStudent.Date);
//         return (idEquality && nameEquality && dateEquality);
//       }
//     }
//
//     public override int GetHashCode()
//     {
//       return this.Name.GetHashCode();
//     }
//
//     public static List<Student> GetAll()
//     {
//       List<Student> AllStudents = new List<Student>{};
//
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
//       SqlDataReader rdr = cmd.ExecuteReader();
//
//       while(rdr.Read())
//       {
//         int studentId = rdr.GetInt32(0);
//         string studentName = rdr.GetString(1);
//         string studentDate = rdr.GetDateTime(2).ToShortDateString();
//         Student newStudent = new Student(studentName, studentDate, studentId);
//         AllStudents.Add(newStudent);
//       }
//
//       if (rdr != null)
//       {
//         rdr.Close();
//       }
//       if (conn != null)
//       {
//         conn.Close();
//       }
//
//       return AllStudents;
//     }
//
//
//     public void Save()
//
//     {
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("INSERT INTO students (name, date) OUTPUT INSERTED.id VALUES (@StudentName, @StudentDate);", conn);
//
//       SqlParameter nameParam = new SqlParameter();
//       nameParam.ParameterName = "@StudentName";
//       nameParam.Value = this.Name;
//
//       SqlParameter dateParam = new SqlParameter();
//       dateParam.ParameterName = "@StudentDate";
//       dateParam.Value = this.Date;
//
//       cmd.Parameters.Add(nameParam);
//       cmd.Parameters.Add(dateParam);
//
//       SqlDataReader rdr = cmd.ExecuteReader();
//
//
//       while(rdr.Read())
//       {
//         this.Id = rdr.GetInt32(0);
//       }
//       if (rdr != null)
//       {
//         rdr.Close();
//       }
//       if (conn != null)
//       {
//         conn.Close();
//       }
//     }
//
//
//     public static Student Find(int id)
//     {
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);
//       SqlParameter studentIdParameter = new SqlParameter();
//       studentIdParameter.ParameterName = "@StudentId";
//       studentIdParameter.Value = id.ToString();
//       cmd.Parameters.Add(studentIdParameter);
//       SqlDataReader rdr = cmd.ExecuteReader();
//
//       int foundStudentId = 0;
//       string foundStudentName = null;
//       string foundStudentDate = null;
//
//       while(rdr.Read())
//       {
//         foundStudentId = rdr.GetInt32(0);
//         foundStudentName = rdr.GetString(1);
//         foundStudentDate = rdr.GetDateTime(2).ToShortDateString();
//       }
//       Student foundStudent = new Student(foundStudentName, foundStudentDate, foundStudentId);
//
//       if (rdr != null)
//       {
//         rdr.Close();
//       }
//       if (conn != null)
//       {
//         conn.Close();
//       }
//
//       return foundStudent;
//     }
//
//     public void Delete()
//     {
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id = @StudentId; DELETE FROM courses_students WHERE student_id = @StudentId;", conn);
//
//       SqlParameter studentIdParameter = new SqlParameter();
//       studentIdParameter.ParameterName = "@StudentId";
//       studentIdParameter.Value = this.Id;
//
//       cmd.Parameters.Add(studentIdParameter);
//       cmd.ExecuteNonQuery();
//
//       if (conn != null)
//       {
//         conn.Close();
//       }
//     }
//
//     public void AddCourse(Course newCourse)
//     {
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("INSERT INTO courses_students (course_id, student_id) VALUES (@CourseId, @StudentId);", conn);
//
//       SqlParameter courseIdParameter = new SqlParameter();
//       courseIdParameter.ParameterName = "@CourseId";
//       courseIdParameter.Value = newCourse.Id;
//       cmd.Parameters.Add(courseIdParameter);
//
//       SqlParameter studentIdParameter = new SqlParameter();
//       studentIdParameter.ParameterName = "@StudentId";
//       studentIdParameter.Value = this.Id;
//       cmd.Parameters.Add(studentIdParameter);
//
//       cmd.ExecuteNonQuery();
//
//       if (conn != null)
//       {
//         conn.Close();
//       }
//     }
//
//     public List<Course> GetCourses()
//     {
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("SELECT courses.* FROM courses JOIN courses_students ON (courses.id = courses_students.course_id) JOIN students ON (courses_students.student_id = students.id) WHERE students.id = @StudentId", conn);
//
//       SqlParameter studentIdParameter = new SqlParameter();
//       studentIdParameter.ParameterName = "@StudentId";
//       studentIdParameter.Value = this.Id;
//       cmd.Parameters.Add(studentIdParameter);
//
//       SqlDataReader rdr = cmd.ExecuteReader();
//       List<Course> courses = new List<Course> {};
//
//       while (rdr.Read())
//       {
//         int thisCourseId = rdr.GetInt32(0);
//         string courseName = rdr.GetString(1);
//         string courseNumber = rdr.GetString(2);
//         Course foundCourse = new Course (courseName, courseNumber, thisCourseId);
//         courses.Add(foundCourse);
//
//       }
//       if (rdr != null)
//       {
//         rdr.Close();
//       }
//
//       if (conn != null)
//       {
//         conn.Close();
//       }
//       return courses;
//     }
//
//     public void Update(string newName, string newDate)
//     {
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("UPDATE students SET name = @newName, date = @newDate OUTPUT INSERTED.name, INSERTED.date WHERE id = @StudentId;", conn);
//
//       SqlParameter courseNameParameter = new SqlParameter();
//       courseNameParameter.ParameterName = "@newName";
//       courseNameParameter.Value = newName;
//       cmd.Parameters.Add(courseNameParameter);
//
//       SqlParameter dateParameter = new SqlParameter();
//       dateParameter.ParameterName = "@newDate";
//       dateParameter.Value = newDate;
//       cmd.Parameters.Add(dateParameter);
//
//
//       SqlParameter studentIdParameter = new SqlParameter();
//       studentIdParameter.ParameterName = "@StudentId";
//       studentIdParameter.Value = this.Id;
//       cmd.Parameters.Add(studentIdParameter);
//       SqlDataReader rdr = cmd.ExecuteReader();
//
//       while(rdr.Read())
//       {
//        this.Name = rdr.GetString(0);
//        this.Date = rdr.GetDateTime(1).ToShortDateString();
//       }
//
//       if (rdr != null)
//       {
//        rdr.Close();
//       }
//
//       if (conn != null)
//       {
//        conn.Close();
//       }
//     }
//
//     public static void DeleteAll()
//     {
//       SqlConnection conn = DB.Connection();
//       conn.Open();
//
//       SqlCommand cmd = new SqlCommand("DELETE FROM students;", conn);
//       cmd.ExecuteNonQuery();
//       conn.Close();
//     }
//
//   }
// }
