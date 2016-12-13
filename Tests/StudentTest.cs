using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Registrar.Objects;

namespace  Registrar
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Student.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Equal_AreObjectsEquivalent_true()
    {
      //Arrange, Act
      Student firstStudent = new Student("Annie", "3/2/2014");
      Student secondStudent = new Student("Annie", "3/2/2014");

      //Assert
      Assert.Equal(firstStudent, secondStudent);
    }

    [Fact]
    public void GetAllAndSave_SaveNewStudentToDatabase_ListOfEnteredStudents()
    {
      //Arrange
      Student testStudent = new Student("Annie", "3/2/2014");
      testStudent.Save();

      //Act
      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_SaveAssignsIdToObject_Id()
    {
      //Arrange
      Student testStudent = new Student("Annie", "3/2/2014");
      testStudent.Save();

      //Act
      Student savedStudent = Student.GetAll()[0];

      int result = savedStudent.Id;
      int testId = testStudent.Id;

      //Assert
      Assert.Equal(testId, result);
    }

    [Fact]
    public void Find_FindsStudentInDatabase_true()
    {
      //Arrange
      Student testStudent = new Student("Annie", "3/2/2014");
      testStudent.Save();

      //Act
      Student result = Student.Find(testStudent.Id);

      //Assert
      Assert.Equal(testStudent, result);
    }

  //   [Fact]
  //   public void AddCourse_AddsCourseToStudent_categoryList()
  //   {
  //     //Arrange
  //     Student testStudent = new Student("Mow the lawn");
  //     testStudent.Save();
  //
  //     Course testCourse = new Course("Home stuff");
  //     testCourse.Save();
  //
  //     //Act
  //     testStudent.AddCourse(testCourse);
  //
  //     List<Course> result = testStudent.GetCourses();
  //     List<Course> testList = new List<Course>{testCourse};
  //
  //     //Assert
  //     Assert.Equal(testList, result);
  //   }

    [Fact]
    public void GetCourses_ReturnsAllStudentCourses_categoryList()
    {
      //Arrange
      Student testStudent = new Student("Annie", "3/2/2014");
      testStudent.Save();

      Course testCourse1 = new Course("History", "HIST100");
      testCourse1.Save();

      Course testCourse2 = new Course("Japanese", "JAPN100");
      testCourse2.Save();

      //Act
      testStudent.AddCourse(testCourse1);
      List<Course> result = testStudent.GetCourses();
      List<Course> testList = new List<Course> {testCourse1};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Delete_DeletesStudentAssociationsFromDatabase()
    {
      //Arrange
      Course testCourse = new Course("History", "HIST100");
      testCourse.Save();

      string testName = "Bryant";
      string testDate = "4/9/2014";
      Student testStudent = new Student(testName, testDate);
      testStudent.Save();

      //Act
      testStudent.AddCourse(testCourse);
      testStudent.Delete();

      List<Student> resultCourseStudents = testCourse.GetStudents();
      List<Student> testCourseStudents = new List<Student> {};

      //Assert
      Assert.Equal(testCourseStudents, resultCourseStudents);
    }

    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
  }
}
