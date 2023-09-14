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
    public class OfertaController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public OfertaController(IConfiguration configuration, IWebHostEnvironment env)

        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select OfertaId, PhotoFileName, EmriOfertes, Cmimi,DateOfOrder from
                            Oferta
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
        public JsonResult Post(Oferta of)
        {
            string query = @"
                            insert into Oferta
                            (PhotoFileName,EmriOfertes,Cmimi,DateOfOrder)
                            values (@PhotoFileName,@EmriOfertes,@Cmimi,@DateOfOrder)
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@PhotoFileName", of.PhotoFileName);
                    myComand.Parameters.AddWithValue("@EmriOfertes", of.EmriOfertes);
                    myComand.Parameters.AddWithValue("@Cmimi", of.Cmimi);
                    myComand.Parameters.AddWithValue("@DateOfOrder", of.DateOfOrder);
                    myReader = myComand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult("Added Successfully");

        }
        [HttpPut]
        public JsonResult Put(Oferta of)
        {
            string query = @"
                            update Oferta
                            set PhotoFileName= @PhotoFileName,
                            EmriOfertes=@EmriOfertes,
                            Cmimi=@Cmimi,
                            DateOfOrder=@DateOfOrder
                            where OfertaId=@OfertaId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@OfertaId", of.OfertaId);
                    myComand.Parameters.AddWithValue("@PhotoFileName", of.PhotoFileName);
                    myComand.Parameters.AddWithValue("@EmriOfertes", of.EmriOfertes);
                    myComand.Parameters.AddWithValue("@Cmimi", of.Cmimi);
                    myComand.Parameters.AddWithValue("@DateOfOrder", of.DateOfOrder);
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
                            delete from Oferta
                            where OfertaId=@OfertaId
                            ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("OnlineFoodAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myComand = new SqlCommand(query, myCon))
                {
                    myComand.Parameters.AddWithValue("@OfertaId", id);

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
