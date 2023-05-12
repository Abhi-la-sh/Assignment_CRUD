using Assignment_CRUD.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Assignment_CRUD.Controllers
{
    public class HomeController : Controller
    {
        public IConfiguration Configuration { get;}

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        

        public IActionResult Index()
        {
            List<Std>studentlist=new List<Std>();
            string connectionstring = Configuration["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sql = "Select * from StdTable";

                SqlCommand cmd = new SqlCommand(sql, connection);

                using (SqlDataReader datareader = cmd.ExecuteReader())
                {
                    while (datareader.Read())
                    {
                        Std std=new Std();

                        std.ID=Convert.ToInt32(datareader["ID"]);

                        std.FirstName=Convert.ToString(datareader["first_name"]);

                        std.LastName=Convert.ToString(datareader["last_name"]);

                        std.Email=Convert.ToString(datareader["email"]);

                        studentlist.Add(std);

                    }

                }
                connection.Close();
            }
            return View(studentlist);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Std std)
        {
            string connectionstring = Configuration["ConnectionStrings:DefaultConnection"];
            SqlConnection connection;

            using (connection=new SqlConnection(connectionstring))
            {
                string sql = $"Insert Into StdTable(first_name,last_name,email) values('{std.FirstName}','{std.LastName}','{std.Email}')";

                SqlCommand command;
                using (command=new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
            connection.Close();

            return RedirectToAction("Index");
        }


        public IActionResult Update(int Id)
        {
            string connectionstring = Configuration["ConnectionStrings:DefaultConnection"];

            Std std = new Std();

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                connection.Open();
                string sql = $"Select * from StdTable where Id={Id}";

                SqlCommand cmd = new SqlCommand(sql, connection);

                using (SqlDataReader datareader = cmd.ExecuteReader())
                {
                    while (datareader.Read())
                    {
                        std.ID=Convert.ToInt32(datareader["ID"]);

                        std.FirstName=Convert.ToString(datareader["first_name"]);

                        std.LastName=Convert.ToString(datareader["last_name"]);

                        std.Email=Convert.ToString(datareader["email"]);

                    }

                }
                connection.Close();
            }
            return View(std);
        }

        [HttpPost]
        public IActionResult Update(Std std,int Id)
        {
            string connectionstring = Configuration["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(connectionstring))
            {
                string sql = $"Update StdTable SET first_name='{std.FirstName}',last_name='{std.LastName}',email='{std.Email}'where Id={Id}";

                using(SqlCommand command =new SqlCommand(sql, connection))
                {
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                }

            }
            return RedirectToAction("Index");

        }


        public IActionResult Delete(int Id)
        {
            string connectionstring = Configuration["ConnectionStrings:DefaultConnection"];

            SqlConnection connection;
            using(connection = new SqlConnection(connectionstring))
            {
                string sql =$"Delete From StdTable Where Id={Id}";
                SqlCommand command;

                using(command=new SqlCommand(sql, connection))
                {
                    connection.Open();

                    command.ExecuteNonQuery();
                }
            }
            connection.Close();
            return RedirectToAction("Index");
        }  
    }
}