using APIMongoDBsample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace APIMongoDBsample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));
            var dblist = dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").AsQueryable();
            return new JsonResult(dblist);
        }
        [HttpPost]
        public JsonResult Post(Employee emp)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));
            int LastEmployeeId = dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").AsQueryable().Count();
            emp.EmployeeId = LastEmployeeId + 1;
            dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").InsertOne(emp);
            return new JsonResult("Added Sucessfully");
        }
        [HttpPut]
        public JsonResult Put(Employee emp)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var filter = Builders<Employee>.Filter.Eq("EmployeeId", emp.EmployeeId);

            var update = Builders<Employee>.Update.Set("EmployeeName", emp.EmployeeName)
                .Set("EmployeeName", emp.EmployeeName)
                .Set("Department", emp.Department)
                .Set("DateOfJoining", emp.DateOfJoining);


            dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").UpdateOne(filter, update);
            return new JsonResult("Updated Sucessfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var filter = Builders<Employee>.Filter.Eq("EmployeeId", id);


            dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").DeleteOne(filter);
            return new JsonResult("deleted Sucessfully");
        }
    }
}
