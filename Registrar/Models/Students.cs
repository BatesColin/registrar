using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Registrar.Models
{
  public class Student
  {
    private int _studentId;
    private string _studentName;

    public Student(string StudentName, int StudentId = 0)
    {
      _studentId = StudentId;
      _studentName = StudentName;
    }
    public int GetStudentId()
    {
      return _studentId;
    }
    public string GetStudentName()
    {
      return _studentName;
    }
    public override bool Equals(System.Object otherStudent)
    {
      if(!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool idEquality = this.GetStudentId().Equals(newStudent.GetStudentId());
        bool nameEquality = this.GetStudentName().Equals(newStudent.GetStudentName());
        return (idEquality && nameEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetStudentId().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name) VALUES (@name);";

      cmd.Parameters.Add(new MySqlParameter("@name", _studentName));

      cmd.ExecuteNonQuery();
      _studentId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Student> GetAllStudents()
    {
      List<Student> allStudents = new List<Student> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int StudentId = rdr.GetInt32(0);
        string StudentName = rdr.GetString(1);
        Student newStudent = new Student(StudentName, StudentId);
        allStudents.Add(newStudent);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      // return new List<Student>{};
      return allStudents;
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAllJoin()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students_courses;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Student Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM students WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int StudentId = 0;
      string StudentName = "";

      while(rdr.Read())
      {
        StudentId = rdr.GetInt32(0);
        StudentName = rdr.GetString(1);
      }
      Student newStudent = new Student(StudentName, StudentId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return newStudent;
      // return new Student("");
    }
    public void AddCourse(Course newCourse)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students_courses (course_id, student_id) VALUES (@CourseId, @StudentId);";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", _studentId));
      cmd.Parameters.Add(new MySqlParameter("@CourseId", newCourse.GetCourseId()));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Course> GetCourses()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT courses.* FROM students
      JOIN students_courses ON (students.id = students_courses.student_id)
      JOIN courses ON (students_courses.course_id = courses.id)
      WHERE students.id = @StudentId;";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", _studentId));

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Course> courses = new List<Course>{};

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string enrollDate = rdr.GetString(1);
        string courseName = rdr.GetString(2);
        int courseNumber = rdr.GetInt32(3);
        Course newCourse = new Course(enrollDate, courseName, courseNumber, studentId);
        courses.Add(newCourse);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return courses;
      // return new List<Student>{};
    }
    public void Edit(string newStudentName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE students SET name = @newStudentName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _studentId));
      cmd.Parameters.Add(new MySqlParameter("@newStudentName", newStudentName));

      cmd.ExecuteNonQuery();
      _studentName = newStudentName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students WHERE id = @StudentId; DELETE FROM students_courses WHERE student_id = @StudentId;";

      cmd.Parameters.Add(new MySqlParameter("@StudentId", this.GetStudentId()));

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
