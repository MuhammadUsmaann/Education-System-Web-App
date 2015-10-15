using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class City
    {
        public int id { get; set; }
        public string name { get; set; }
        public int country_id { get; set; }
        public int state_id { get; set; }
    }
}