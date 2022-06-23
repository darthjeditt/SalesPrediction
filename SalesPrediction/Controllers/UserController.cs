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
        public JsonResult SaveFile() {
            var httpRequest = Request.Form;
            var postedFile = httpRequest.Files[0];
            string filename = postedFile.FileName;
            string sqlDataSource = _configuration.GetConnectionString("Database");
            DataTable table = new DataTable();
            string csvData = System.IO.File.ReadAllText(filename);
            try {
                foreach (string row in csvData.Split('\n')) {
                    if (!string.IsNullOrEmpty(row)) {
                        table.Rows.Add();
                        int i = 0;
                        foreach (string cell in row.Split(',')) {
                            table.Rows[table.Rows.Count - 1][i] = cell;
                            i++;
                        }
                    }
                }

                using (SqlConnection myCon = new SqlConnection(sqlDataSource)) {
                    myCon.Open();
                    using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(myCon)) {
                        //Set the database table name.
                        sqlBulkCopy.DestinationTableName = "dbo.TblSales";
                        myCon.Open();
                        sqlBulkCopy.WriteToServer(table);
                        myCon.Close();
                    }
                }

                return new JsonResult("Successfully imported");
            } catch (Exception) {

                return new JsonResult("Error");
            }
        }
    }
}
