using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OnlineFood.Models;

namespace OnlineFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogininController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LogininController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select UserId, Email, Userpass from
                            Loginin
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using(SqlConnection myCon=new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using(SqlCommand myComand=new SqlCommand(query, myCon))
                {
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);

        }
        [HttpPost]
        public JsonResult Post(Loginin log)
        {
            string query = @"
                            insert into Loginin
                            (Email,Userpass)
                            values (@Email,@Userpass)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@Email", log.Email);
                    myComand.Parameters.AddWithValue("@Userpass", log.Userpass);
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        public JsonResult Put(Loginin log)
        {
            string query = @"
                            update Loginin
                            set Email= @Email,
                            Userpass=@Userpass
                            where UserId=@UserId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@UserId", log.UserId);
                    myComand.Parameters.AddWithValue("@Email", log.Email);
                    myComand.Parameters.AddWithValue("@Userpass", log.Userpass);
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Updatet Successfully");

        }
        [HttpDelete("{id}")]
        public JsonResult Put(int id)
        {
            string query = @"
                            delete from Loginin
                            where UserId=@UserId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@UserId", id);
                    
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deletet Successfully");

        }
    }
}
