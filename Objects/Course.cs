using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Registrar.Objects
{
  public class Course
  {

    public int Id {get; set;}
    private string Name {get; set;}
    private string Number {get; set;}

    public Course(string name, string number, int id = 0)
    {
      this.Id = id;
      this.Name = name;
      this.Number = number;
    }

    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = (this.Id == newCourse.Id);
        bool nameEquality = (this.Name == newCourse.Name);
        bool numberEquality = (this.Number == newCourse.Number);
        return (idEquality && nameEquality && numberEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.Name.GetHashCode();
    }

    public static List<Course> GetAll()
    {
      List<Course> AllCourses = new List<Course>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        AllCourses.Add(newCourse);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return AllCourses;
    }


    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (name, number) OUTPUT INSERTED.id VALUES (@CourseName, @CourseNumber);", conn);

      SqlParameter nameParam = new SqlParameter();
      nameParam.ParameterName = "@CourseName";
      nameParam.Value = this.Name;

      SqlParameter numberParam = new SqlParameter();
      numberParam.ParameterName = "@CourseNumber";
      numberParam.Value = this.Number;

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(numberParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this.Id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
    }

    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = id.ToString();
      cmd.Parameters.Add(courseIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundCourseId = 0;
      string foundCourseName = null;
      string foundCourseNumber = null;
      while(rdr.Read())
      {
        foundCourseId = rdr.GetInt32(0);
        foundCourseName = rdr.GetString(1);
        foundCourseNumber = rdr.GetString(2);
      }
      Course foundCourse = new Course(foundCourseName, foundCourseNumber, foundCourseId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }

      return foundCourse;
    }


    public void Update(string newName, string newNumber)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE courses SET name = @newName, number = @newNumber OUTPUT INSERTED.name, INSERTED.number WHERE id = @CourseId;", conn);

      SqlParameter courseNameParameter = new SqlParameter();
      courseNameParameter.ParameterName = "@newName";
      courseNameParameter.Value = newName;
      cmd.Parameters.Add(courseNameParameter);

      SqlParameter numberParameter = new SqlParameter();
      numberParameter.ParameterName = "@newNumber";
      numberParameter.Value = newNumber;
      cmd.Parameters.Add(numberParameter);


      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = this.Id;
      cmd.Parameters.Add(courseIdParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
       this.Name = rdr.GetString(0);
       this.Number = rdr.GetString(1);
      }

      if (rdr != null)
      {
       rdr.Close();
      }

      if (conn != null)
      {
       conn.Close();
      }
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses WHERE id = @CourseId; DELETE FROM courses_students WHERE course_id = @CourseId;", conn);

      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = this.Id;

      cmd.Parameters.Add(courseIdParameter);
      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public void AddStudent(Student newStudent)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses_students (course_id, student_id) VALUES (@CourseId, @StudentId);", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = this.Id;
      cmd.Parameters.Add(courseIdParameter);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = newStudent.Id;
      cmd.Parameters.Add(studentIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Student> GetStudents()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT students.* FROM courses JOIN courses_students ON (courses.id = courses_students.course_id) JOIN students ON (students.id = courses_students.student_id) WHERE course_id = @CourseId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = this.Id;

      cmd.Parameters.Add(courseIdParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Student> allCourseStudents = new List<Student> {};
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        string studentDate = rdr.GetDateTime(2).ToShortDateString();
        Student newStudent = new Student(studentName, studentDate, studentId);
        allCourseStudents.Add(newStudent);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCourseStudents;
    }


    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }

  }
}
