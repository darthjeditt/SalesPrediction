using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SalesPrediction.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SalesPrediction.Controllers {
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase {

        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public UserController(IConfiguration configuration, IWebHostEnvironment env) {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get() {
            string query = @"
                    select SalesId, Dos, Sales from dbo.TblSales";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpDelete]
        public JsonResult Delete() {
            string query = @"
                    truncate table dbo.TblSales";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("Database");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource)) {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon)) {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [Route("SaveFile")]
        [HttpPost]
        public async Task<IActionResult> UploadAsync() {
            try {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folderName = "cvs";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0) {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create)) {
                        file.CopyTo(stream);
                    }

                //DataTable table = new DataTable();
                //string sqlDataSource = _configuration.GetConnectionString("Database");
                //string csvData = System.IO.File.ReadAllText(dbPath);

                //foreach (string row in csvData.Split('\n')) {
                //    if (!string.IsNullOrEmpty(row)) {
                //        table.Rows.Add();
                //        int i = 0;
                //        foreach (string cell in row.Split(',')) {
                //            table.Rows[table.Rows.Count - 1][i] = cell;
                //            i++;
                //        }
                //    }
                //}

                //using (SqlConnection myCon = new SqlConnection(sqlDataSource)) {
                //    myCon.Open();
                //    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(myCon)) {
                //        //Set the database table name.
                //        sqlBulkCopy.DestinationTableName = "dbo.TblSales";
                //        myCon.Open();
                //        sqlBulkCopy.WriteToServer(table);
                //        myCon.Close();
                //    }
                //}

                    return Ok("Successfully imported");
                } else {
                    return BadRequest();
                }
            } catch (Exception ex) {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }



        //public JsonResult SaveFile() {
            //try {
//                DataTable table = new DataTable();
//        var httpRequest = Request.Form;
//        var postedFile = httpRequest.Files[0];
//        string filename = postedFile.FileName;
//        var physicalPath = _env.ContentRootPath + "/cvs/" + filename;
//        string sqlDataSource = _configuration.GetConnectionString("Database");
//        string csvData = System.IO.File.ReadAllText(filename);

//                using (var stream = new FileStream(physicalPath, FileMode.Create)) {
//                    postedFile.CopyTo(stream);
//                }

//foreach (string row in csvData.Split('\n')) {
//    if (!string.IsNullOrEmpty(row)) {
//        table.Rows.Add();
//        int i = 0;
//        foreach (string cell in row.Split(',')) {
//            table.Rows[table.Rows.Count - 1][i] = cell;
//            i++;
//        }
//    }
//}

//using (SqlConnection myCon = new SqlConnection(sqlDataSource)) {
//    myCon.Open();
//    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(myCon)) {
//        //Set the database table name.
//        sqlBulkCopy.DestinationTableName = "dbo.TblSales";
//        myCon.Open();
//        sqlBulkCopy.WriteToServer(table);
//        myCon.Close();
//    }
//}

        //        return new JsonResult("Successfully imported");
        //    } catch (Exception) {

        //        return new JsonResult("Error");
        //    }
        //}
    }
}
