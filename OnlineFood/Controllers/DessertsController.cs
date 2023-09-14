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
    public class DessertsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public DessertsController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select DessertId, PhotoFileName, EmriDessert, Cmimi,DateOfOrder from
                            Desserts
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
        public JsonResult Post(Desserts ds)
        {
            string query = @"
                            insert into Desserts
                            (PhotoFileName,EmriDessert,Cmimi,DateOfOrder)
                            values (@PhotoFileName,@EmriDessert,@Cmimi,@DateOfOrder)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@PhotoFileName", ds.PhotoFileName);
                    myComand.Parameters.AddWithValue("@EmriDessert", ds.EmriDessert);
                    myComand.Parameters.AddWithValue("@Cmimi", ds.Cmimi);
                    myComand.Parameters.AddWithValue("@DateOfOrder", ds.DateOfOrder);
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        public JsonResult Put(Desserts ds)
        {
            string query = @"
                            update Desserts
                            set PhotoFileName= @PhotoFileName,
                            EmriDessert=@EmriDessert,
                            Cmimi=@Cmimi,
                            DateOfOrder=@DateOfOrder
                            where DessertId=@DessertId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@DessertId", ds.DessertId);
                    myComand.Parameters.AddWithValue("@PhotoFileName", ds.PhotoFileName);
                    myComand.Parameters.AddWithValue("@EmriDessert", ds.EmriDessert);
                    myComand.Parameters.AddWithValue("@Cmimi", ds.Cmimi);
                    myComand.Parameters.AddWithValue("@DateOfOrder", ds.DateOfOrder);
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
                            delete from Desserts
                            where DessertId=@DessertId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@DessertId", id);

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
