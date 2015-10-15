using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using Dapper;

namespace EducationSystem.Models.DapperClasses
{
    public  class ParentDetails : IMethods<Parent>
    {
        public override IEnumerable<Parent> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("select {0}.*,{1}.*  FROM {0} JOIN	{1} ON {0}.user_id = {1}.user_id where  {0}.role_code ='{2}' and {1}.school_id = @school_id and {0}.status = @ACTIVE", DBTables.USERS, DBTables.PARENT, RoleCode.PARENT).ToString();
            var result = _db.Query<Parent>(query, new { school_id, ActiveStatus.ACTIVE });
            return result;
        }

        public override Parent FindSingle(int id, int school_id)
        {
            string query = _sb.AppendFormat(("SELECT {0}.*,{1}.*  FROM {0} JOIN	{1} ON {0}.user_id = {1}.user_id where {0}.user_id =@id and {1}.school_id = @school_id and {0}.status=@ACTIVE"), DBTables.USERS, DBTables.PARENT).ToString();
            var result = _db.Query<Parent>(query, new { ActiveStatus.ACTIVE, id, school_id }).FirstOrDefault<Parent>();
            return result;
        }
        public  override IEnumerable<Parent> Find(params string[] conditions)
        {
            string query = _sb.AppendFormat("select * from {0}", DBTables.PARENT).ToString();
            var result = _db.Query<Parent>(query);
            return result;
        }
        public  override bool Insert(Parent obj)
        {
            obj.role_code = RoleCode.PARENT;
            
            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Insert into {0} (username, password, role_code,status,school_id,updated_date,updated_by,created_by, created_date) OUTPUT Inserted.user_id values(@gfirst_name, @password,@role_code, @ACTIVE,@school_id,{2},{1},{1},{2});", DBTables.USERS, obj.created_by, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.gfirst_name,  obj.password, obj.role_code, obj.school_id,ActiveStatus.ACTIVE }).Single();
            
            var username = Utils.MakeUserName(user_id, obj.gfirst_name);
            var sys_id = Utils.MakeID(user_id);

            _sb.Clear();
            string update = _sb.AppendFormat("Update {0} set username = @username where user_id=@user_id", DBTables.USERS).ToString();
            var u = _db.Query<int>(update, new { username, user_id });
            
            string iq = "(email,cnic, sys_id, pfirst_name, plast_name,landline,income, mother_name,gfirst_name, glast_name, gmiddle_name,school_id, user_id,address,phone,profession ,updated_date,updated_by,created_by, created_date)";
            string vq = "(@email,@cnic, @sys_id, @pfirst_name, @plast_name,@landline,@income, @mother_name,@gfirst_name, @glast_name, @gmiddle_name,@school_id, @user_id, @address,@phone,@profession,GETDATE(),{1},{1},GETDATE())";

            _sb.Clear();
            string query_for_teacher = _sb.AppendFormat("Insert into {0} "+iq+" values " + vq, DBTables.PARENT, obj.created_by).ToString();
            _db.Execute(query_for_teacher, new {obj.email, obj.cnic,sys_id,obj.pfirst_name,obj.plast_name,obj.landline,obj.income,obj.mother_name, obj.gfirst_name, obj.glast_name, obj.gmiddle_name, obj.school_id, user_id, obj.address, obj.phone, obj.role_code,obj.profession});

            return true;
        }
        public  override bool Update(Parent obj)
        {
            string user_query = "password=@password, updated_date=GETDATE(),updated_by=@updated_by";
            string user_detail_query = " email=@email,cnic=@cnic,pfirst_name=@pfirst_name, plast_name=@plast_name,landline=@landline,income=@income, mother_name=@mother_name,gfirst_name=@gfirst_name, glast_name=@glast_name, gmiddle_name=@gmiddle_name,address=@address,phone=@phone,profession=@profession ,updated_date=GETDATE(),updated_by=@updated_by";
            _sb.Clear();
            string uquery = _sb.AppendFormat("update {0} set " + user_query + " where user_id=@user_id", DBTables.USERS).ToString();
            _sb.Clear();
            string dquery = _sb.AppendFormat("update {0} set " + user_detail_query + " where user_id=@user_id", DBTables.PARENT, obj.created_by).ToString();
            _db.Execute(uquery, new { obj.password, obj.user_id, obj.updated_by });
            _db.Execute(dquery, new { obj.email, obj.cnic, obj.pfirst_name, obj.plast_name,obj.user_id, obj.landline, obj.income, obj.mother_name, obj.gfirst_name, obj.glast_name, obj.gmiddle_name, obj.school_id, obj.address, obj.phone, obj.profession, obj.updated_by });
            return true;
        }
        public  override bool Delete(int id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("UPDATE {0} set status=@INACTIVE where user_id = @id", DBTables.USERS).ToString();
            var result = _db.Execute(query, new { id, ActiveStatus.INACTIVE });
            return true;
        }
        ~ParentDetails()
        {
            _db.Dispose();
        }
    }
}