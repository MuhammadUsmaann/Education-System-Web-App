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
    public  class ClassDetails:IMethods<Class>
    {
        public override IEnumerable<Class> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.*, {1}.name AS school_name FROM {0} JOIN {1} ON {1}.school_id= {0}.school_id WHERE {1}.school_id = @school_id and {0}.status=@ACTIVE", DBTables.CLASS, DBTables.SCHOOL).ToString();
            var result = _db.Query<Class>(query, new { school_id, ActiveStatus.ACTIVE});
            return result;
        }
        public IEnumerable<Class> GetAll(int school_id, int class_id, string date="")
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.*, {1}.name AS school_name FROM {0} JOIN {1} ON {1}.school_id= {0}.school_id WHERE {1}.school_id = @school_id and {0}.status=@ACTIVE", DBTables.CLASS, DBTables.SCHOOL).ToString();
            var result = _db.Query<Class>(query, new { school_id, ActiveStatus.ACTIVE });
            return result;
        }
        public override Class FindSingle(int id, int school_id)
        {
            string query = _sb.AppendFormat("SELECT {0}.*, {1}.name AS school_name FROM {0} JOIN {1} ON {1}.school_id= {0}.school_id WHERE {1}.school_id = @school_id and {0}.status=@ACTIVE and {0}.class_id = @id ", DBTables.CLASS, DBTables.SCHOOL).ToString();
            var result = _db.Query<Class>(query, new { id, school_id, ActiveStatus.ACTIVE }).FirstOrDefault<Class>();
            return result;
        }
        public  override IEnumerable<Class> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }
        public  override bool Insert(Class obj)
        {
            string q = _sb.AppendFormat(("select TOP 1 * from {0} where name =@name and numeric_num = @numeric_num and school_id =@school_id"), DBTables.CLASS).ToString();
            var cls = _db.Query<Class>(q, new { obj.name, obj.numeric_num, obj.school_id }).FirstOrDefault();

            if (cls != null)
            {
                return false;
            }
            _sb.Clear();
            string query = _sb.AppendFormat("Insert into {0} (name, numeric_num,school_id, teacher_id,status, updated_date,updated_by,created_by, created_date) values(@name, @numeric_num,@school_id, @teacher_id,@ACTIVE,GETDATE(),{1},{1},GETDATE())", DBTables.CLASS, obj.created_by).ToString();
            _db.Execute(query, new { obj.name, obj.numeric_num, obj.school_id, obj.teacher_id, ActiveStatus.ACTIVE });
            return true;
        }

        public  override bool Update(Class obj)
        {
            string user_query = "name=@name, numeric_num=@numeric_num,school_id=@school_id,teacher_id=@teacher_id,updated_by={1},updated_date = {2}";
            _sb.Clear();
            string query = _sb.AppendFormat("update {0} set " + user_query + " where class_id=@class_id", DBTables.CLASS, obj.updated_by, "GETDATE()").ToString();
            _db.Execute(query, new { obj.name, obj.numeric_num, obj.school_id, obj.teacher_id, obj.class_id});
            return true;
        }

        public  override bool Delete(int id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("Update {0} status=@INACTIVE where class_id = @id", DBTables.CLASS).ToString();
            _db.Execute(query, new { id,ActiveStatus.INACTIVE });
            return true;
        }
        ~ClassDetails()
        {
            _db.Dispose();
        }
    }
}