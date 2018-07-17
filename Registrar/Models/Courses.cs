using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace Registrar.Models
{
  public class Course
  {
    private int _courseId;
    private string _enrollDate;
    private string _courseName;
    private int _courseNumber;

    public Course(string EnrollDate, string CourseName, int CourseNumber, int CourseId = 0)
    {
      _courseId = CourseId;
      _enrollDate = EnrollDate;
      _courseName = CourseName;
      _courseNumber = CourseNumber;
    }
    public int GetCourseId()
    {
      return _courseId;
    }
    public string GetEnrollDate()
    {
      return _enrollDate;
    }
    public string GetCourseName()
    {
      return _courseName;
    }
    public int GetCourseNumber()
    {
      return _courseNumber;
    }
    public override bool Equals(System.Object otherCourse)
    {
      if(!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = this.GetCourseId().Equals(newCourse.GetCourseId());
        bool enrollEquality = this.GetEnrollDate().Equals(newCourse.GetEnrollDate());
        bool nameEquality = this.GetCourseName().Equals(newCourse.GetCourseName());
        bool numberEquality = this.GetCourseNumber().Equals(newCourse.GetCourseNumber());
        return (idEquality && nameEquality && enrollEquality && numberEquality);
      }
    }
    public override int GetHashCode()
    {
      return this.GetCourseId().GetHashCode();
    }
    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO courses (enroll_date, course_name, course_number) VALUES (@enrollDate, @courseName, @courseNumber);";

      cmd.Parameters.Add(new MySqlParameter("@enrollDate", _enrollDate));
      cmd.Parameters.Add(new MySqlParameter("@courseName", _courseName));
      cmd.Parameters.Add(new MySqlParameter("@courseNumber", _courseNumber));

      cmd.ExecuteNonQuery();
      _courseId = (int) cmd.LastInsertedId;
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Course> GetAllCourses()
    {
      List<Course> allCourses = new List<Course> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int CourseId = rdr.GetInt32(0);
        string EnrollDate = rdr.GetString(1);
        string CourseName = rdr.GetString(2);
        int CourseNumber = rdr.GetInt32(3);
        Course newCourse = new Course(EnrollDate, CourseName, CourseNumber, CourseId);
        allCourses.Add(newCourse);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCourses;
      // return new List<Course>{};
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM courses;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static Course Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM courses WHERE id = (@searchId);";

      cmd.Parameters.Add(new MySqlParameter("@searchId", id));

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int CourseId = 0;
      string EnrollDate = "";
      string CourseName = "";
      int CourseNumber = 0;

      while(rdr.Read())
      {
        CourseId = rdr.GetInt32(0);
        EnrollDate = rdr.GetString(1);
        CourseName = rdr.GetString(2);
        CourseNumber = rdr.GetInt32(3);
      }
      Course newCourse = new Course(EnrollDate, CourseName, CourseNumber, CourseId);
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      // return new Course("", "", 0);
      return newCourse;
    }
    public void AddStudent(Student newStudent)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students_courses (course_id, student_id) VALUES (@CourseId, @StudentId);";

      cmd.Parameters.Add(new MySqlParameter("@CourseId", _courseId));
      cmd.Parameters.Add(new MySqlParameter("@StudentId", newStudent.GetStudentId()));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Student> GetStudents()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT students.* FROM courses
      JOIN students_courses ON (courses.id = students_courses.course_id)
      JOIN students ON (students_courses.student_id = students.id)
      WHERE courses.id = @CourseId;";

      cmd.Parameters.Add(new MySqlParameter("@CourseId", _courseId));

      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Student> students = new List<Student>{};

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        Student newStudent = new Student(studentName, studentId);
        students.Add(newStudent);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return students;
      // return new List<Student>{};
    }
    public void Edit(string newEnrollDate, string newCourseName, int newCourseNumber)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE courses SET enroll_date = @newEnrollDate, course_name = @newCourseName, course_number = @newCourseNumber WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _courseId));
      cmd.Parameters.Add(new MySqlParameter("@newEnrollDate", newEnrollDate));
      cmd.Parameters.Add(new MySqlParameter("@newCourseName", newCourseName));
      cmd.Parameters.Add(new MySqlParameter("@newCourseNumber", newCourseNumber));

      cmd.ExecuteNonQuery();
      _enrollDate = newEnrollDate;
      _courseName = newCourseName;
      _courseNumber = newCourseNumber;

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
      cmd.CommandText = @"DELETE FROM courses WHERE id = @CourseId; DELETE FROM students_courses WHERE course_id = @CourseId;";

      cmd.Parameters.Add(new MySqlParameter("@CourseId", this.GetCourseId()));

      cmd.ExecuteNonQuery();
      if (conn != null)
      {
        conn.Close();
      }
    }
  }
}
