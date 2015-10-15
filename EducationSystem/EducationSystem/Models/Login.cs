using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace EducationSystem.Models
{
    public class Auth
    {
        SqlConnection _db = DBModel.Get_DB_Connection();
        StringBuilder _sb = new StringBuilder();
        public UserProfileSessionData Login(string username, string password)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("select top 1 * from {0} where password=@password and username=@username and status = @ACTIVE",DBTables.USERS).ToString();
            var result = _db.Query<User>(query, new { username, password, ActiveStatus.ACTIVE }).FirstOrDefault();

            if (result !=null)
            {
                UserProfileSessionData up = new UserProfileSessionData();
                up.password = password;
                up.username = username;
                
                _sb.Clear();
                string query1 = "";
                if(result.role_code == RoleCode.STUDENT)
                {
                    query1 = _sb.AppendFormat("select top 1 * from {0} where user_id=@user_id", DBTables.STUDENT).ToString();
                    var result1 = _db.Query<Student>(query1, new { result.user_id, result.role_code }).FirstOrDefault();
                    up.fullname = result1.full_name;
               
                }
                else if (result.role_code == RoleCode.PARENT)
                {
                    query1 = _sb.AppendFormat("select top 1 * from {0} where user_id=@user_id", DBTables.PARENT).ToString();
                    var result1 = _db.Query<Parent>(query1, new { result.user_id, result.role_code }).FirstOrDefault();
                    up.fullname = result1.full_name;
                    
                }
                else if (result.role_code == RoleCode.TEACHER)
                {
                    query1 = _sb.AppendFormat("select top 1 * from {0} where user_id=@user_id", DBTables.TEACHER).ToString();
                    var result1 = _db.Query<Parent>(query1, new { result.user_id, result.role_code }).FirstOrDefault();
                    up.fullname = result1.full_name;
                }
                else if (result.role_code == RoleCode.ADMIN)
                {
                    query1 = _sb.AppendFormat("select top 1 * from {0} where user_id=@user_id", DBTables.ADMIN).ToString();
                    var result1 = _db.Query<Users_detail>(query1, new { result.user_id}).FirstOrDefault();
                    up.fullname = result1.first_name + " " + result1.last_name;
                }

                if (result.role_code == RoleCode.TEACHER || result.role_code == RoleCode.ADMIN)
                {
                    var status = ActiveStatus.ACTIVE;
                    var query2 = "Select * from " + DBTables.SESSION + " where school_id = @school_id and isDefault=@status and status=@status";
                    var session = _db.Query<Session>(query2, new { result.school_id, status }).FirstOrDefault();
                    if(session !=  null)
                    {
                        up.session_id = session.session_id;
                        up.session_is_active = true;
                    }
                }
                else
                {
                    up.session_is_active = false;
                }


                up.user_id = result.user_id;
                up.role_code = result.role_code;
                up.school_id = result.school_id;
               
                _sb.Clear();
                string s1 = _sb.AppendFormat("select top 1 * from {0} where {0}.school_id=@school_id", DBTables.SCHOOL).ToString();
                var school = _db.Query<School>(s1, new { result.school_id }).FirstOrDefault();
                up.school_code = school.code;
                up.school_name = school.name;
                _db.Dispose();

                return up;
            }
            return null;
        }

        public static bool Authentication(UserProfileSessionData up)
        {

            return true;
            //if (up == null)
            //    return false;
            //SqlConnection _dba = DBModel.Get_DB_Connection();
            //StringBuilder _sba = new StringBuilder();

            //string query = _sba.AppendFormat("select top 1 * from {0} where password=@password and username=@username", DataBaseTables.USERS).ToString();
            //var result = _dba.Query<User>(query, new { up.username, up.password}).FirstOrDefault();
            //_dba.Dispose();
            //if (result != null)
            //{
            //    return true;
            //}
            //return false;
        }

        // Method to get role of the current user
        public Role GetRole(string username)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("select {0}.* from {0} inner join {1} on {0}.role_code={1}.role_code where  {1}.username=@username", DBTables.ROLES, DBTables.USERS).ToString();
            var result = _db.Query<Role>(query, new { username }).FirstOrDefault();
            
            return result;
        }

        public  bool IsValidUser(string username, string oldPassword)
        {
            string query = _sb.AppendFormat("select {0}.* from {0} where  {0}.username='"+username+"' and {0}.password='"+oldPassword+"'", DBTables.USERS).ToString();
            var result = _db.Query<User>(query, new { username }).FirstOrDefault();

            if (result != null)
            {
                return true;
            }
            return false;
        }

        public void ChangePassword(string username, string NewPassword)
        {
            string Uquery = _sb.AppendFormat("Update {0} set {0}.password='" + NewPassword + "'  where  {0}.username='"+username+"'", DBTables.USERS).ToString();
            _db.Execute(Uquery, new { username });
        }


        private int GetUserID(string username)
        {
            string query = _sb.AppendFormat("select {0}.* from {0} where {0}.username='"+username+"'",  DBTables.USERS).ToString();
            var result = _db.Query<User>(query, new { username }).FirstOrDefault();

            if (result != null)
            {
                return result.user_id;
            }
            else {
                throw new Exception("User Information does not found.");
            }
        }

        public IEnumerable<Users_detail> GetUserDetails(string username)
        {
            
           // int user_id = GetUserID(username);
            int user_id = 1;
            _sb.Clear();
            string query = _sb.AppendFormat("select Top 1 {0}.* from {0} where {0}.user_id='" + user_id + "'", DBTables.ADMIN).ToString();
            IEnumerable<Users_detail> result = _db.Query<Users_detail>(query, new { user_id });
            return result;
           
        }

        public void UpdateProfile(Users_detail _userDetails)
        {

            string Uquery = _sb.AppendFormat("Update {0} set {0}.first_name='" + _userDetails.first_name + "' , {0}.last_name='" + _userDetails.last_name + "' , {0}.middle_name='" + _userDetails.middle_name + "' , {0}.other_detail='" + _userDetails.other_detail + "' , {0}.address='" + _userDetails.address + "' , {0}.phone='" + _userDetails.phone + "' , {0}.profession='" + _userDetails.profession + "', {0}.image='" + _userDetails.image + "' where {0}.user_id='" + _userDetails.user_id + "'", DBTables.ADMIN).ToString();
            _db.Execute(Uquery, new { _userDetails.user_id });

        }
    }
}