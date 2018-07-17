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
    public List<Course> GetAllCourses()
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
        string CourseNumber = rdr.GetString(3);
        Course newCourse = new Course(EnrollDate, CourseName, CourseNumber, CourseId);
        allCourses.Add(newCourse);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCourses;
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
  }
}
