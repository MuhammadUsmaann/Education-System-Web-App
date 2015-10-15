using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class School
    {
        public int id { get; set; }
        public string name { get; set; }
        public int city_id { get; set; }
        public string code { get; set; }
        public string phone { get; set; }
    }
}