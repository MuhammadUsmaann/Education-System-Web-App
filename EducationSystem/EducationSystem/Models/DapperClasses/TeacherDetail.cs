using System;
using System.Collections.Generic;
using System.Linq;
using EducationSystem.Models;
using EducationSystem.Models.Entities;
using System.Data.SqlClient;
using Dapper;
using System.Text;
namespace EducationSystem.Models.DapperClasses
{
    public class TeacherDetail:IMethods<Teacher>
    {
        public override IEnumerable<Teacher> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("select {0}.*,{1}.*  FROM {0} JOIN	{1} ON {0}.user_id = {1}.user_id and {0}.role_code ='{2}' and {0}.school_id = @school_id and {0}.status=@ACTIVE", DBTables.USERS, DBTables.TEACHER, RoleCode.TEACHER).ToString();
            var result = _db.Query<Teacher>(query, new { school_id, ActiveStatus.ACTIVE });
            return result;
        }
        public override Teacher FindSingle(int id, int school_id)
        {
            string query = _sb.AppendFormat(("SELECT {0}.*,{1}.*  FROM {0} JOIN	{1} ON {0}.user_id = {1}.user_id and {0}.school_id = @school_id and {0}.user_id =@id  and {0}.status=@ACTIVE"), DBTables.USERS, DBTables.TEACHER).ToString();
            var result = _db.Query<Teacher>(query, new { school_id , id, ActiveStatus.ACTIVE}).FirstOrDefault<Teacher>();
            return result;
        }
        public override IEnumerable<Teacher> Find(params string[] conditions)
        {
            string query = _sb.AppendFormat("select * from {0}", DBTables.ADMIN).ToString();
            var result = _db.Query<Teacher>(query);
            return result;
        }
        public override bool Insert(Teacher obj)
        {
            obj.role_code = RoleCode.TEACHER;

            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Insert into {0} (username, password, role_code,status,updated_date,updated_by,created_by, created_date, school_id) OUTPUT Inserted.user_id values(@first_name, @password,@role_code, @ACTIVE,{2},{1},{1},{2},@school_id);", DBTables.USERS, obj.created_by, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.first_name, obj.password, obj.role_code,obj.school_id, ActiveStatus.ACTIVE }).Single();

            var username = Utils.MakeUserName(user_id, obj.first_name);
            var sys_id = Utils.MakeID(user_id);

            _sb.Clear();
            string update = _sb.AppendFormat("Update {0} set username = @username where user_id=@user_id", DBTables.USERS).ToString();
            var u = _db.Query<int>(update, new { username, user_id });

            string iq = "(first_name,last_name,middle_name,gender,other_detail,school_id,created_by,user_id,updated_by,address,phone,birthday,salary,landline,mother_name,father_name,cnic,sys_id,email,updated_date,created_date)";
            string vq = "(@first_name,@last_name,@middle_name,@gender,@other_detail,@school_id,@created_by,@user_id,@updated_by,@address,@phone,@birthday,@salary,@landline,@mother_name,@father_name,@cnic,@sys_id,@email,GETDATE(),GETDATE())";

            _sb.Clear();
            string query_for_teacher = _sb.AppendFormat("Insert into {0} " + iq + " values " + vq, DBTables.TEACHER, obj.created_by).ToString();
            _db.Execute(query_for_teacher, new { obj.first_name, obj.last_name, obj.middle_name, obj.gender, obj.other_detail, obj.school_id, obj.created_by, user_id, obj.updated_by, obj.address, obj.phone, obj.birthday, obj.salary, obj.landline, obj.mother_name, obj.father_name, obj.cnic, sys_id, obj.email});

            return true;
        }
        public override bool Update(Teacher obj)
        {
            string user_query = "password=@password, updated_date={2},updated_by={1}";
            string user_detail_query = "first_name=@first_name, last_name=@last_name,birthday=@birthday, school_id=@school_id,address=@address,phone=@phone updated_date={2},updated_by={1}";
            _sb.Clear();
            string uquery = _sb.AppendFormat("update {0} set " + user_query + " where user_id=@user_id", DBTables.USERS,obj.created_by, "GETDATE()").ToString();
            _sb.Clear();
            string dquery = _sb.AppendFormat("update {0} set " + user_detail_query + " where user_id=@user_id", DBTables.TEACHER, obj.created_by, "GETDATE()").ToString();
            _db.Execute(uquery, new { obj.password,obj.user_id });
            _db.Execute(dquery, new { obj.first_name,obj.last_name,obj.birthday, obj.school_id,obj.address,obj.phone,obj.other_detail, obj.user_id});
            return true;
        }
        public override bool Delete(int id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("Update {0} set status=@INACTIVE where user_id = @id", DBTables.USERS).ToString();
            _db.Execute(query, new { id, ActiveStatus.INACTIVE });
            return true;
        }
        ~TeacherDetail()
        {
            _db.Dispose();
        }
    }
}