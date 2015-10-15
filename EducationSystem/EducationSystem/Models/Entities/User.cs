using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EducationSystem.Models.Entities
{
    public class User
    {
        public int user_id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role_code { get; set; }
        public string datetime { get; set; }
        public int created_by { get; set; }
        public int school_id { get; set; }
        public string created_date { get; set; }
    }
}