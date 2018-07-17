using Microsoft.AspNetCore.Mvc;
using Registrar.Models;
using System.Collections.Generic;
using System;

namespace Registrar.Controllers
{
  public class CoursesController : Controller
  {

    [HttpGet("/courses")]
    public ActionResult Index()
    {
        List<Course> allCourses = Course.GetAllCourses();
        return View(allCourses);
    }

    [HttpGet("/courses/new")]
    public ActionResult CreateForm()
    {
      return View();
    }
    [HttpPost("/courses")]
    public ActionResult Create()
    {
      Course newCourse = new Course(Request.Form["newenrolldate"], Request.Form["newcoursename"], int.Parse(Request.Form["newcoursenumber"]));
      newCourse.Save();
      return RedirectToAction("Success", "Home");
    }
    [HttpGet("/courses/{id}/update")]
    public ActionResult UpdateForm(int id)
    {
      Course thisCourse = Course.Find(id);
      return View(thisCourse);
    }
    [HttpPost("/courses/{id}/update")]
    public ActionResult Update(int id)
    {
      Course thisCourse = Course.Find(id);
      thisCourse.Edit(Request.Form["updateenrolldate"], Request.Form["updatecoursename"], int.Parse(Request.Form["updatecoursenumber"]));
      return RedirectToAction("Index");
    }

    [HttpGet("/courses/{id}/delete")]
    public ActionResult Delete(int id)
    {
      Course thisCourse = Course.Find(id);
      thisCourse.Delete();
      return RedirectToAction("Index");
    }
    // [HttpPost("/courses/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Course.ClearAll();
    //   return View();
    // }
    [HttpGet("/courses/{id}")]
    public ActionResult Details(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Course selectedCourse = Course.Find(id);
      List<Student> courseStudents = selectedCourse.GetStudents();
      List<Student> allStudents = Student.GetAllStudents();
      model.Add("selectedCourse", selectedCourse);
      model.Add("courseStudents", courseStudents);
      model.Add("allStudents", allStudents);
      return View(model);
    }
    [HttpPost("/courses/{courseId}/students/new")]
    public ActionResult AddStudent(int courseId)
    {
      Course course = Course.Find(courseId);
      Student student = Student.Find(int.Parse(Request.Form["studentid"]));
      course.AddStudent(student);
      return RedirectToAction("Details",  new { id = courseId });
    }
  }
}
