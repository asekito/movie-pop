﻿using System;
using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using App1.Models;

namespace App1.Controllers
{
    public class MovieController : Controller
    {
        public IActionResult MovieList()
        {
            try
            {
                string cs = @"Data Source=/Users/andrea/Projects/Database/App1Db.db";

                using var connect = new SQLiteConnection(cs);
                //using var connect = new SQLiteConnection("Data Source=App1Db.db;");
                connect.Open();

                string data = "SELECT * FROM Movies";
                using var cmd = new SQLiteCommand(data, connect);
                using SQLiteDataReader rdr = cmd.ExecuteReader();


                //DataTable dt = new DataTable();

                //dt.Load(rdr);


                List<Movie> movieList = new List<Movie>();
                Movie movie = null;

                while (rdr.Read())
                {
                    movie = new Movie();
                    movie.title = rdr["Title"].ToString();
                    movie.director = rdr["Director"].ToString();
                    movie.releaseDate = Convert.ToDateTime(rdr["ReleaseDate"]);
                    movie.genre = rdr["Genre"].ToString();
                    movie.duration = rdr["Duration"].ToString();
                    movie.summary = rdr["Summary"].ToString();

                    movieList.Add(movie);
                }

                return View("MovieList", movieList);
                // need to finish this and handle the data and send to view 
            }
            catch (Exception e)
            {
                return View(e);
            }
        }

        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            try
            {
                string cs = @"Data Source=/Users/andrea/Projects/Database/App1Db.db";

                using var connect = new SQLiteConnection(cs);
                //using var connect = new SQLiteConnection("Data Source=App1Db.db;");
                connect.Open();

                using var cmd = new SQLiteCommand(connect);
                cmd.CommandText = "INSERT INTO Movies(Title, Director, ReleaseDate, Genre, Duration, Summary) VALUES(@title, @director, @releaseDate, @genre, @duration, @summary)";

                cmd.Parameters.AddWithValue("@title", movie.title);
                cmd.Parameters.AddWithValue("@director", movie.director);
                cmd.Parameters.AddWithValue("@releaseDate", movie.releaseDate);
                cmd.Parameters.AddWithValue("@genre", movie.genre);
                cmd.Parameters.AddWithValue("@duration", movie.duration);
                cmd.Parameters.AddWithValue("@summary", movie.summary);
                cmd.Prepare();

                cmd.ExecuteNonQuery();

                Console.WriteLine("row inserted");

                return Redirect("MovieList");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error caught here!!!");
                Console.WriteLine(e);
                return Redirect("AddMovie");
            }
        }

    }
}
