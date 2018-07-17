using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using Registrar.Models;

namespace Registrar.Tests
{
  [TestClass]
  public class StudentTests : IDisposable
  {
    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
      Student.DeleteAllJoin();
    }
    public StudentTests()
    {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=registrar_test;";
    }
    [TestMethod]
    public void Save_GetAllStudents_Test()
    {
      //Arrange
      Student newStudent = new Student("Derek Hammer");
      Student newStudent1 = new Student("Eddie Harris");
      newStudent.Save();
      newStudent1.Save();

      //Act
      List<Student> expectedResult = new List<Student>{newStudent, newStudent1};
      List<Student> result = Student.GetAllStudents();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void Find_Test()
    {
      //Arrange
      Student newStudent = new Student("James Hanley");
      newStudent.Save();

      //Act
      Student result = Student.Find(newStudent.GetStudentId());

      //Assert
      Assert.AreEqual(newStudent, result);
    }
    [TestMethod]
    public void GetStudents_Test()
    {
      //Arrange
      Student newStudent = new Student("Billy Kinzig");
      newStudent.Save();
      Course newCourse = new Course("2018-07-20", "MAT", 203);
      newCourse.Save();
      Course newCourse1 = new Course("2018-07-20", "PHY", 240);
      newCourse1.Save();

      //Act
      newStudent.AddCourse(newCourse);
      newStudent.AddCourse(newCourse1);

      List<Course> expectedResult = new List<Course>{newCourse, newCourse1};
      List<Course> result = newStudent.GetCourses();

      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
    [TestMethod]
    public void Edit_Test()
    {
      //Arrange
      Student newStudent = new Student("Curt Cladwell");
      newStudent.Save();
      Student expectedStudent = new Student("Curt Caldwell", newStudent.GetStudentId());
      //Act
      newStudent.Edit("Curt Caldwell");

      //Assert
      Assert.AreEqual(expectedStudent, newStudent);
    }
    [TestMethod]
    public void Delete_Test()
    {
      //Arrange
      Student newStudent = new Student("Austin Barr");
      newStudent.Save();
      Student newStudent1 = new Student("Eliot Charette");
      newStudent1.Save();

      //Act
      newStudent.Delete();
      List<Student> result = Student.GetAllStudents();
      List<Student> expectedResult = new List<Student>{newStudent1};
      //Assert
      CollectionAssert.AreEqual(expectedResult, result);
    }
  }
}
