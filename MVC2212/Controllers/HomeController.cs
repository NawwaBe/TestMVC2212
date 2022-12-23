using Npgsql;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Configuration;

namespace MVC2212.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index() => View();

        [HttpPost]
        public string Index(string username, string password)
        {
            var id = 0;
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            NpgsqlConnection conn = new NpgsqlConnection(connectionString);
            var commandText = "SELECT id FROM users WHERE login = '" + username + "' AND password = '" + password + "'";
            var command = new NpgsqlCommand(commandText, conn);

            conn.Open();

            NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                id = int.Parse(reader["id"].ToString());
            }

            conn.Close();

            if (id != 0)
            {
                return "Вы успешно вошли на сайт!";
            }
            else
            {
                return "Неправильный логин или пароль!";
            }
            
        }
    }
}
