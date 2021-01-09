using System;
namespace App1.Models
{
    public class Meal
    {
        //public int id { get; set; }
        public string MealTitle { get; set; }
        public int Rating { get; set; }
        public string Location { get; set; }
        public DateTime DateEaten { get; set; }
        public string Comments { get; set; }
    }
}
