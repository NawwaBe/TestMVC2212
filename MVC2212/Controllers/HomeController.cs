using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            NpgsqlConnection conn = new NpgsqlConnection("Host=localhost:5432;Username=postgres;Password=mvc2212;Database=postgres");
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
