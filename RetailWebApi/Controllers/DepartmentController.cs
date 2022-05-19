using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace RetailWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpGet]
        public JsonResult GetAllDepartment()
        {
            string que = @"select DepartmentID from dbo.tbldepartment";

            DataTable dt = new DataTable();

            string ConnectionString = _configuration.GetConnectionString("AppCon");

            SqlDataReader dr;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(que,con);

                dr = cmd.ExecuteReader();
                dt.Load(dr);

               
                dr.Close();
                con.Close();
            }

            return new JsonResult(dt);
        }
    }
}
