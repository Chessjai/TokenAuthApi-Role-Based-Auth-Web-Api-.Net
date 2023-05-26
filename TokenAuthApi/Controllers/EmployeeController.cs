using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TokenAuthApi.Data;
using TokenAuthApi.Models;

namespace TokenAuthApi.Controllers
{
    public class EmployeeController : ApiController
    {
        ApiDbContext apiDbContext = new ApiDbContext();
        [Authorize(Roles =("User"))]
        public HttpResponseMessage GetEmployeeById(int id)
        {
            var user = apiDbContext.Employees.FirstOrDefault(e => e.Id == id);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
        [Authorize(Roles = ("Admin,SuperAdmin"))]
        [Route("api/Employee/GetSomeEmployees")]
        public HttpResponseMessage GetSomeEmployees()
        {
            var user = apiDbContext.Employees.Where(e => e.Id <= 10);
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Employee/GetEmployees")]

        public HttpResponseMessage GetEmployees()
        {
            var user = apiDbContext.Employees.ToList();
            return Request.CreateResponse(HttpStatusCode.OK, user);
        }
        [Authorize(Roles = ("SuperAdmin"))]
        [Route("api/Employee/PostNewStudent")]
        public IHttpActionResult PostNewStudent(Employee student)
        {


            using (var ctx = new ApiDbContext())
            {
                ctx.Employees.Add(new Employee()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Gender = student.Gender,
                    CreatedDate = student.CreatedDate,
                    Email = student.Email
                });

                ctx.SaveChanges();

            }
            return Ok();

        }
        [Authorize(Roles = ("SuperAdmin,Admin"))]

        public IHttpActionResult Put(Employee student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new ApiDbContext())
            {
                var existingStudent = ctx.Employees.Where(s => s.Id == student.Id)
                                                        .FirstOrDefault<Employee>();

                if (existingStudent != null)
                {
                    existingStudent.FirstName = student.FirstName;
                    existingStudent.LastName = student.LastName;
                    existingStudent.Email = student.Email;
                    existingStudent.Gender = student.Gender;
                    existingStudent.CreatedDate = student.CreatedDate;


                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }

            return Ok();
        }
        [Authorize(Roles = ("SuperAdmin,Admin"))]

        public IHttpActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid student id");

            using (var ctx = new ApiDbContext())
            {
                var student = ctx.Employees
                    .Where(s => s.Id == id)
                    .FirstOrDefault();

                ctx.Entry(student).State = System.Data.Entity.EntityState.Deleted;
                ctx.SaveChanges();
            }

            return Ok();
        }
    }
}

