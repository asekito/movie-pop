using System;
using System.Collections.Generic;
namespace App1.Models
{
    public class Movie
    {
        //public int id { get; set; }
        public string title { get; set; }
        public string director { get; set; }
        public DateTime releaseDate { get; set; }
        public List<string> genre { get; set; }
        public string duration { get; set; }
        public string summary { get; set; }
    }
}
