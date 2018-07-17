using Microsoft.AspNetCore.Mvc;
using Registrar.Models;
using System.Collections.Generic;
using System;

namespace Registrar.Controllers
{
  public class StudentsController : Controller
  {

    [HttpGet("/students")]
    public ActionResult Index()
    {
        List<Student> allStudents = Student.GetAllStudents();
        return View(allStudents);
    }

    [HttpGet("/students/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/students")]
    public ActionResult Create()
    {
      Student newStudent = new Student(Request.Form["newstudent"]);
      newStudent.Save();
      return RedirectToAction("Success", "Home");
    }
    [HttpGet("/students/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Student thisStudent = Student.Find(id);
      return View(thisStudent);
    }
    [HttpPost("/students/{id}/update")]
    public ActionResult Update(int id)
    {
      Student thisStudent = Student.Find(id);
      thisStudent.Edit(Request.Form["updatestudent"]);
      return RedirectToAction("Index");
    }

    [HttpGet("/students/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Student thisStudent = Student.Find(id);
      thisStudent.Delete();
      return RedirectToAction("Index");
    }
    // [HttpPost("/students/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Student.ClearAll();
    //   return View();
    // }
    [HttpGet("/students/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Student selectedStudent = Student.Find(id);
      List<Course> studentCourses = selectedStudent.GetCourses();
      List<Course> allCourses = Course.GetAllCourses();
      model.Add("selectedStudent", selectedStudent);
      model.Add("studentCourses", studentCourses);
      model.Add("allCourses", allCourses);
      return View(model);
    }
    [HttpPost("/students/{studentId}/courses/new")]
    public ActionResult AddCourse(int studentId)
    {
      Student student = Student.Find(studentId);
      Course course = Course.Find(int.Parse(Request.Form["courseid"]));
      course.AddStudent(student);
      return RedirectToAction("Details",  new { id = studentId });
    }
  }
}
