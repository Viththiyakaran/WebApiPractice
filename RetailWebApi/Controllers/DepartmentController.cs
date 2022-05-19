using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using RetailWebApi.Model;

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
            string que = @"select DepartmentID,Department,IsVat,VatPer from dbo.tbldepartment";

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


        [HttpPost]
        public JsonResult CreateDepartment(Department department)
        {
            string que = @"insert into dbo.tbldepartment(DepartmentID,Department,IsVat,VatPer) values
                                        (@DepartmentID,@DepartmentName,@IsVat,@VatPer)";

            DataTable dt = new DataTable();

            string ConnectionString = _configuration.GetConnectionString("AppCon");

            SqlDataReader dr;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(que, con);
                cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                cmd.Parameters.AddWithValue("@IsVat", department.IsVat);
                cmd.Parameters.AddWithValue("@VatPer", department.VatPer);

                dr = cmd.ExecuteReader();
                dt.Load(dr);


                dr.Close();
                con.Close();
            }

            return new JsonResult(dt);
        }


        [HttpPut]
        public JsonResult UpdateDepartment(Department department)
        {
            string que = @"update dbo.tbldepartment set Department =@DepartmentName, IsVat = @IsVat,  VatPer = @VatPer
                                        where DepartmentID = @DepartmentID";

            DataTable dt = new DataTable();

            string ConnectionString = _configuration.GetConnectionString("AppCon");

            SqlDataReader dr;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(que, con);
                cmd.Parameters.AddWithValue("@DepartmentID", department.DepartmentID);
                cmd.Parameters.AddWithValue("@DepartmentName", department.DepartmentName);
                cmd.Parameters.AddWithValue("@IsVat", department.IsVat);
                cmd.Parameters.AddWithValue("@VatPer", department.VatPer);

                dr = cmd.ExecuteReader();
                dt.Load(dr);


                dr.Close();
                con.Close();
            }

            return new JsonResult(dt);
        }


        [HttpDelete]
        public JsonResult DeleteDepartment(int id)
        {
            string que = @"delete from dbo.tbldepartment where DepartmentID = @DepartmentID";

            DataTable dt = new DataTable();

            string ConnectionString = _configuration.GetConnectionString("AppCon");

            SqlDataReader dr;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();


                SqlCommand cmd = new SqlCommand(que, con);
                cmd.Parameters.AddWithValue("@DepartmentID", id);
                

                dr = cmd.ExecuteReader();
                dt.Load(dr);


                dr.Close();
                con.Close();
            }

            return new JsonResult(dt);
        }
    }
}
