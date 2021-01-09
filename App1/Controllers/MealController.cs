using System;
using System.Data.SQLite;
using Microsoft.AspNetCore.Mvc;
using App1.Models;

namespace App1.Controllers
{
    public class MealController : Controller
    {
        public IActionResult AddMeal()
        {
            return View();
        }

        public IActionResult MealList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Meal meal)
        {
            try
            {
                string cs = @"Data Source=/Users/andrea/Projects/Database/App1Db.db";

                using var connect = new SQLiteConnection(cs);
                //using var connect = new SQLiteConnection("Data Source=App1Db.db;");
                connect.Open();

                using var cmd = new SQLiteCommand(connect);
                cmd.CommandText = "INSERT INTO Meals(title, rating, location, dateEaten, comments) VALUES(@title, @rating, @location, @dateEaten, @comments)";

                cmd.Parameters.AddWithValue("@title", meal.MealTitle);
                cmd.Parameters.AddWithValue("@rating", meal.Rating);
                cmd.Parameters.AddWithValue("@location", meal.Location);
                cmd.Parameters.AddWithValue("@dateEaten", meal.DateEaten);
                cmd.Parameters.AddWithValue("@comments", meal.Comments);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                Console.WriteLine("row inserted");

                return Redirect("MealList");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error caught here!!!");
                Console.WriteLine(e);
                return Redirect("AddMeal");
            }
        }

    }
}
