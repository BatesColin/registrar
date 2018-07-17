using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Registrar.Models;

namespace Registrar.Tests
{
  [TestClass]
  public class CourseTests : IDisposable
  {
    public void Dispose()
    {
      Course.DeleteAll();
      Student.DeleteAll();
      Student.DeleteAllJoin();
    }
    public CourseTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=registrar_test;";
    }
    [TestMethod]
    public void Save_GetAllCourses_Test()
    {
      //Arrange
      Course newCourse = new Course("2018-07-20", "MAT", 201);
      Course newCourse1 = new Course("2018-07-20", "PSY", 101);
      newCourse.Save();
      newCourse1.Save();

      //Act
      List<Course> expectedResult = new List<Course>{newCourse, newCourse1};
      List<Course> result = Course.GetAllCourses();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void Find_Test()
    {
      //Arrange
      Course newCourse = new Course("2018-07-20", "HIS", 221);
      newCourse.Save();

      //Act
      Course result = Course.Find(newCourse.GetCourseId());

      //Assert
      Assert.AreEqual(newCourse, result);
    }
    [TestMethod]
    public void GetStudents_Test()
    {
      //Arrange
      Course newCourse = new Course("2018-07-20", "EGR", 311);
      newCourse.Save();
      Student newStudent = new Student("Swati Sahay");
      newStudent.Save();
      Student newStudent1 = new Student("Lan Dam");
      newStudent1.Save();

      //Act
      newCourse.AddStudent(newStudent);
      newCourse.AddStudent(newStudent1);

      List<Student> expectedResult = new List<Student>{newStudent, newStudent1};
      List<Student> result = newCourse.GetStudents();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void Edit_Test()
    {
      //Arrange
      Course newCourse = new Course("2018-07-20", "SCI", 402);
      newCourse.Save();
      Course expectedCourse = new Course("2018-07-26", "SCI", 322, newCourse.GetCourseId());
      //Act
      newCourse.Edit("2018-07-26", "SCI", 322);

      //Assert
      Assert.AreEqual(expectedCourse, newCourse);
    }
    [TestMethod]
    public void Delete_Test()
    {
      //Arrange
      Course newCourse = new Course("2018-07-30", "MAT", 242);
      newCourse.Save();
      Course newCourse1 = new Course("2018-07-30", "EGR", 101);
      newCourse1.Save();

      //Act
      newCourse.Delete();
      List<Course> result = Course.GetAllCourses();
      List<Course> expectedResult = new List<Course>{newCourse1};
      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
  }
}
