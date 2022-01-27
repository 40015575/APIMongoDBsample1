using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace APIMongoDBsample.Models
{
    public class Department
    {
        public ObjectId Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
