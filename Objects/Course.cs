using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Registrar.Objects
{
  public class Course
  {

    public int Id {get; set;}
    public string Name {get; set;}
    public string Number {get; set;}

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
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Course> allCourses = new List<Course> {};

      while(rdr.Read())
      {
        int foundId = rdr.GetInt32(0);
        string foundName = rdr.GetString(1);
        string foundNumber = rdr.GetString(2);

        Course newCourse = new Course(foundName, foundNumber, foundId);
        allCourses.Add(newCourse);
      }

      if (rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
      return allCourses;
    }


    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (name, number) OUTPUT INSERTED.id VALUES (@CourseName, @CourseNumber);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CourseName";
      nameParameter.Value = this.Name;

      SqlParameter numberParameter = new SqlParameter();
      numberParameter.ParameterName = "@CourseNumber";
      numberParameter.Value = this.Number;

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(numberParameter);

      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())  //
      {
        this.Id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }

      if(conn != null)
      {
        conn.Close();
      }
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
