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
    public class SingupController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SingupController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select UserSignId, UserName, UserEmail, UserpassW from
                            Singup
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
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
        public JsonResult Post(Singup sg)
        {
            string query = @"
                            insert into Singup
                            (UserName,UserEmail,UserpassW)
                            values (@UserName,@UserEmail,@UserpassW)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@UserName", sg.UserName);
                    myComand.Parameters.AddWithValue("@UserEmail", sg.UserEmail);
                    myComand.Parameters.AddWithValue("@UserpassW", sg.UserpassW);
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        public JsonResult Put(Singup sg)
        {
            string query = @"
                            update Singup
                            set UserName= @UserName,
                            UserEmail=@UserEmail,
                            UserpassW=@UserpassW
                            where UserSignId=@UserSignId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@UserSignId", sg.UserSignId);
                    myComand.Parameters.AddWithValue("@UserName", sg.UserName);
                    myComand.Parameters.AddWithValue("@UserEmail", sg.UserEmail);
                    myComand.Parameters.AddWithValue("@UserpassW", sg.UserpassW);
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
                            delete from Singup
                            where UserSignId=@UserSignId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@UserSignId", id);

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

