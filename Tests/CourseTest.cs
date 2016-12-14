using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Registrar.Objects;

namespace Registrar
{
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=registrar_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      int result = Course.GetAll().Count;

      Assert.Equal(0, result);
    }

    [Fact]
    public void Equal_AreObjectsEquivalent_true()
    {
      Course firstCourse = new Course("History", "HIST100");
      Course secondCourse = new Course("History", "HIST100");

      Assert.Equal(firstCourse, secondCourse);
    }

    [Fact]
    public void GetAll_SaveNewCourseToDatabase_ListOfEnteredCategories()
    {
      Course testCourse = new Course("History", "HIST100");
      testCourse.Save();

      List<Course> result = Course.GetAll();
      List<Course> testList = new List<Course>{testCourse};

      Assert.Equal(testList, result);
    }

    [Fact]
    public void Save_SaveAssignsIdToObject_Id()
    {
      Course testCourse = new Course("History", "HIST100");
      testCourse.Save();

      Course savedCourse = Course.GetAll()[0];

      int result = savedCourse.Id;
      int testId = testCourse.Id;

      Assert.Equal(testId, result);
    }
    
    // [Fact]
    // public void Find_FindsCourseInDatabase_true()
    // {
    //   Course testCourse = new Course("History", "HIST100");
    //   testCourse.Save();
    //
    //   Course result = Course.Find(testCourse.Id);
    //
    //   Assert.Equal(testCourse, result);
    // }
    //
    // [Fact]
    // public void Delete_DeletesCourseFromDatabase_testCourse2()
    // {
    //   string name1 = "History";
    //   string number1 = "HIST100";
    //   Course testCourse1 = new Course(name1, number1);
    //   testCourse1.Save();
    //
    //   string name2 = "Japanese";
    //   string number2 = "JAPN100";
    //   Course testCourse2 = new Course(name2, number2);
    //   testCourse2.Save();
    //
    //   testCourse1.Delete();
    //   List<Course> resultCategories = Course.GetAll();
    //   List<Course> testCourseList = new List<Course> {testCourse2};
    //
    //   Assert.Equal(testCourseList, resultCategories);
    // }
    //
    // [Fact]
    // public void GetStudents_ReturnsAllCourseStudent_studentList()
    // {
    //   Course testCourse = new Course("History", "HIST100");
    //   testCourse.Save();
    //
    //   Student testStudent1 = new Student("Annie", "3/2/2014");
    //   testStudent1.Save();
    //
    //   Student testStudent2 = new Student("Bryant", "4/9/2014");
    //   testStudent2.Save();
    //
    //   testCourse.AddStudent(testStudent1);
    //   List<Student> savedStudent = testCourse.GetStudents();
    //   List<Student> testList = new List<Student> {testStudent1};
    //
    //   Assert.Equal(testList, savedStudent);
    // }
    //
    // [Fact]
    // public void AddStudent_AddsStudentToCourse_studentList()
    // {
    //   Course testCourse = new Course("History", "HIST100");
    //   testCourse.Save();
    //
    //   Student testStudent = new Student("Annie", "3/2/2014");
    //   testStudent.Save();
    //
    //   Student testStudent2 = new Student("Bryant", "4/2/2013");
    //   testStudent2.Save();
    //
    //   testCourse.AddStudent(testStudent);
    //   testCourse.AddStudent(testStudent2);
    //
    //   List<Student> result = testCourse.GetStudents();
    //   List<Student> testList = new List<Student>{testStudent, testStudent2};
    //
    //   Assert.Equal(testList, result);
    // }
    //
    // [Fact]
    // public void Update_UpdateCourseNameAndNumber_NameAndNumberUpdatedInDatabase()
    // {
    //   Course testCourse = new Course("History", "HIST100");
    //   testCourse.Save();
    //
    //   testCourse.Update("Japanese", "JAPN100");
    //   Course result = Course.GetAll()[0];
    //
    //   Assert.Equal(result, testCourse);
    // }

    // [Fact]
    // public void Test_Delete_DeletesCourseAssociationsFromDatabase()
    // {
    //   Student testStudent = new Student("Mow the lawn");
    //   testStudent.Save();
    //
    //   string testName = "Home stuff";
    //   Course testCourse = new Course(testName);
    //   testCourse.Save();
    //
    //   testCourse.AddStudent(testStudent);
    //   testCourse.Delete();
    //
    //   List<Course> resultStudentCategories = testStudent.GetCategories();
    //   List<Course> testStudentCategories = new List<Course> {};
    //
    //   Assert.Equal(testStudentCategories, resultStudentCategories);
    // }

    public void Dispose()
    {
      // Student.DeleteAll();
      Course.DeleteAll();
    }
  }
}
