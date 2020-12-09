using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithAngular.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApiWithAngular10;

namespace AngularProjectWithCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId,DepartmentName from dbo.Department";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeConn");
            SqlDataReader myReader;

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Department dep)
        {
            string query = @"
                    insert into dbo.Department values 
                    ('" + dep.DepartmentName + @"')
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeConn");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department dep)
        {
            string query = @"update dbo.department set
                   DepartmentName='" + dep.DepartmentName + @"'
                    where DepartmentId=" + dep.DepartmentId + @"";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeConn");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Updated Successfully!");

        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"Delete from dbo.Department where DepartmentId=" + id + @"";
            DataTable table = new DataTable();
            string sqDataSource = _configuration.GetConnectionString("EmployeeConn");
            SqlDataReader myReader;
            using (SqlConnection con = new SqlConnection(sqDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    myReader = cmd.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    con.Close();
                }
            }
            return new JsonResult("Deleted Successfully");
        }

    }
}
