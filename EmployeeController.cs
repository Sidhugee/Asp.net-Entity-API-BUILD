using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using WebAPIEmployee.Models;


namespace WebAPIEmployee.Controllers
{
    public class EmployeeController : ApiController
    {
        //           {-- Requests --}
        // Ok for 200 means everything is OK.
        // Not Found for 404 means data is not found.
        // Bad Request for 400 means bad request.
        // Create Data for 201 means created data.

        private Entities db = new Entities();
        // GET DATA METHOD 
        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees;
        }
        // GET BY ID METHOD 
        public IHttpActionResult GetEmployee(long id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        // PUT METHOD
        public IHttpActionResult PutEmployee(int id, Employee employee)
        {
            var entity = db.Employees.Find(id);
            if (id == null)
            {
                return NotFound();
            }
            entity.Name = employee.Name;
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Ok();


           
        }
        // POST METHOD 
        public IHttpActionResult PostEmployee(Employee employee)
        {
            var data = db.Employees.FirstOrDefault(e => e.Name == employee.Name);
            if (data == null)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return CreatedAtRoute("DefaultApi", new
                {
                    id = employee.Id
                }, employee);
            }
            return BadRequest();
           
        }
        // DELETE METHOD 
        public IHttpActionResult DeleteEmployee(long id)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            db.Entry(employee).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Ok(employee);
        }
      
    }

} 


    
    
