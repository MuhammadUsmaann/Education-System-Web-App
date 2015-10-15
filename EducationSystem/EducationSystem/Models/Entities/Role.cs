using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class Role
    {
        public int id { get; set; }
        public string role_code { get; set; }
        public string role_description { get; set; }
        public string sys_id { get; set; }
    }
}