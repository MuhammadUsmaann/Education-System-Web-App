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
    public  class SubjectDetails:IMethods<Subject>
    {
        public override IEnumerable<Subject> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.*,CONCAT({1}.first_name , ' ' ,{1}.last_name) AS teacher_name,{3}.name AS class_name , {2}.name AS school_name FROM {0} JOIN {2} ON {2}.id= {0}.school_id JOIN {1} ON {0}.teacher_id = {1}.user_id JOIN {3} ON {0}.class_id = {3}.class_id where {2}.id = {4}", DBTables.SUBJECT, DBTables.ADMIN, DBTables.SCHOOL, DBTables.CLASS, school_id).ToString();
            var result = _db.Query<Subject>(query);
            return result;
        }

        public override Subject FindSingle(int id, int school_id)
        {
            string query = _sb.AppendFormat("SELECT {0}.*,CONCAT({1}.first_name , ' ' ,{1}.last_name) AS teacher_name,{4}.name as class_name , {2}.name AS school_name FROM {0} JOIN {2} ON {2}.id= {0}.school_id JOIN {1} ON {0}.teacher_id = {1}.user_id JOIN {4} ON {0}.class_id = {4}.class_id where {0}.subject_id = {3} and {2}.id = {5}", DBTables.SUBJECT, DBTables.ADMIN, DBTables.SCHOOL, id, DBTables.CLASS, school_id).ToString();
            var result = _db.Query<Subject>(query).FirstOrDefault<Subject>();
            return result;
        }


        //public  override IEnumerable<Subject> GetAll()
        //{
        //    _sb.Clear();
        //    string query = _sb.AppendFormat("SELECT {0}.*,CONCAT({1}.first_name , ' ' ,{1}.last_name) AS teacher_name,{3}.name AS class_name , {2}.name AS school_name FROM {0} JOIN {2} ON {2}.id= {0}.school_id JOIN {1} ON {0}.teacher_id = {1}.user_id JOIN {3} ON {0}.class_id = {3}.class_id", DataBaseTables.SUBJECT, DataBaseTables.USERS_DETAIL, DataBaseTables.SCHOOL, DataBaseTables.CLASS).ToString();
        //    var result = _db.Query<Subject>(query);
        //    return result;
        //}

        public  override IEnumerable<Subject> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }

        //public  override Subject FindSingle(int id)
        //{
        //    string query = _sb.AppendFormat("SELECT {0}.*,CONCAT({1}.first_name , ' ' ,{1}.last_name) AS teacher_name,{4}.name as class_name , {2}.name AS school_name FROM {0} JOIN {2} ON {2}.id= {0}.school_id JOIN {1} ON {0}.teacher_id = {1}.user_id JOIN {4} ON {0}.class_id = {4}.class_id where {0}.subject_id = {3}", DataBaseTables.SUBJECT, DataBaseTables.USERS_DETAIL, DataBaseTables.SCHOOL, id, DataBaseTables.CLASS).ToString();
        //    var result = _db.Query<Subject>(query).FirstOrDefault<Subject>();
        //    return result;
        //}

        public  override bool Insert(Subject obj)
        {
            string q = _sb.AppendFormat(("select TOP 1 * from {0} where name ='{1}' and class_id = {2} and school_id ={3} and teacher_id = {4}"), DBTables.CLASS, obj.name, obj.class_id, obj.school_id, obj.teacher_id).ToString();
            var cls = _db.Query<Subject>(q).FirstOrDefault();

            if (cls != null)
            {
                return false;
            }
            _sb.Clear();
            string query = _sb.AppendFormat("Insert into {0} (name, class_id,school_id, teacher_id, updated_date,updated_by,created_by, created_date) values(@name, @class_id,@school_id, @teacher_id,GETDATE(),{1},{1},GETDATE())", DBTables.SUBJECT, obj.created_by).ToString();
            _db.Execute(query, new { obj.name, obj.class_id, obj.school_id, obj.teacher_id });
            return true;
        }

        public  override bool Update(Subject obj)
        {
            string user_query = "name=@name, class_id=@class_id,school_id=@school_id,teacher_id=@teacher_id,updated_by={1},updated_date = {2}";
            _sb.Clear();
            string query = _sb.AppendFormat("update {0} set " + user_query + " where subject_id=@subject_id", DBTables.SUBJECT, obj.updated_by, "GETDATE()").ToString();
            _db.Execute(query, new { obj.name, obj.class_id, obj.school_id, obj.teacher_id, obj.subject_id });
            return true;
        }

        public  override bool Delete(int id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("Delete from {0} where subject_id = @id", DBTables.SUBJECT).ToString();
            _db.Execute(query, new { id });
            return true;
        }
        ~SubjectDetails()
        {
            _db.Dispose();
        }
    }
}