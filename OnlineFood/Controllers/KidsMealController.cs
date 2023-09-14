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
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace OnlineFood.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidsMealController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public KidsMealController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select KidsMealId, PhotoFileName, EmriKidsMeal, Cmimi,DateOfOrder from
                            KidsMeal
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
        public JsonResult Post(KidsMeal km)
        {
            string query = @"
                            insert into KidsMeal
                            (PhotoFileName,EmriKidsMeal,Cmimi,DateOfOrder)
                            values (@PhotoFileName,@EmriKidsMeal,@Cmimi,@DateOfOrder)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@PhotoFileName", km.PhotoFileName);
                    myComand.Parameters.AddWithValue("@EmriKidsMeal", km.EmriKidsMeal);
                    myComand.Parameters.AddWithValue("@Cmimi", km.Cmimi);
                    myComand.Parameters.AddWithValue("@DateOfOrder", km.DateOfOrder);
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        public JsonResult Put(KidsMeal km)
        {
            string query = @"
                            update KidsMeal
                            set PhotoFileName= @PhotoFileName,
                            EmriKidsMeal=@EmriKidsMeal,
                            Cmimi=@Cmimi,
                            DateOfOrder=@DateOfOrder
                            where KidsMealId=@KidsMealId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@KidsMealId", km.KidsMealId);
                    myComand.Parameters.AddWithValue("@PhotoFileName", km.PhotoFileName);
                    myComand.Parameters.AddWithValue("@EmriKidsMeal", km.EmriKidsMeal);
                    myComand.Parameters.AddWithValue("@Cmimi", km.Cmimi);
                    myComand.Parameters.AddWithValue("@DateOfOrder", km.DateOfOrder);
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
                            delete from KidsMeal
                            where KidsMealId=@KidsMealId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@KidsMealId", id);

                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Deletet Successfully");

        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photo/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }
    }
}
