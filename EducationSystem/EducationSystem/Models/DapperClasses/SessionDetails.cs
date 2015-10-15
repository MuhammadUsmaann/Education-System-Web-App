using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace EducationSystem.Models.DapperClasses
{
    public class SessionDetails:IMethods<Session>
    {
        public override IEnumerable<Session> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT * from {0} where school_id= @school_id and status=@ACTIVE", DBTables.SESSION).ToString();
            var result = _db.Query<Session>(query, new { school_id, ActiveStatus.ACTIVE });
            return result;
        }
        public override IEnumerable<Session> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }

        public override Session FindSingle(int session_id, int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT * from {0} where session_id=@session_id and school_id= @school_id and status=@ACTIVE", DBTables.SESSION).ToString();
            var result = _db.Query<Session>(query, new { school_id, ActiveStatus.ACTIVE, session_id }).FirstOrDefault<Session>();
            return result;
        }

        public override bool Insert(Session obj)
        {
            _sb.Clear();
            obj.status = ActiveStatus.ACTIVE;
            obj.isDefault = ActiveStatus.INACTIVE;
            string query = _sb.AppendFormat("insert {0} (start_date, end_date,isDefault, status,school_id, created_by, updated_by, created_date, updated_date) values(@start_date, @end_date,@isDefault, @status, @school_id,@updated_by, @updated_by, GETDATE(), GETDATE())", DBTables.SESSION).ToString();
            _db.Execute(query, new { obj.start_date, obj.end_date,obj.isDefault, obj.status, obj.updated_by, obj.school_id });
            return true;
        }

        public override bool Update(Session obj)
        {
            _sb.Clear();
            obj.status = ActiveStatus.ACTIVE;
            var uq = "start_date=@start_date, end_date=@end_date, updated_by=@updated_by,updated_date=GETDATE()";
            string query = _sb.AppendFormat("Update {0} Set " + uq +" Where school_id=@school_id And session_id=@session_id", DBTables.SESSION).ToString();
            _db.Execute(query, new { obj.start_date, obj.end_date, obj.updated_by, obj.school_id, obj.session_id });
            return true;
        }
        public bool MakeDefault(int session_id , int school_id, int updated_by)
        {
            _sb.Clear();
            var status = ActiveStatus.INACTIVE;

            var uq = "isDefault = @status, updated_by=@updated_by,updated_date=GETDATE()";
            string query = _sb.AppendFormat("Update {0} Set " + uq + " Where school_id=@school_id", DBTables.SESSION).ToString();
            _db.Execute(query, new { session_id, school_id, status, updated_by });

            _sb.Clear();
            var status_default = ActiveStatus.ACTIVE;
            var uq1 = "isDefault = @status_default, updated_by=@updated_by,updated_date=GETDATE()";
            string query1 = _sb.AppendFormat("Update {0} Set " + uq1 + " Where school_id=@school_id And session_id=@session_id", DBTables.SESSION).ToString();
            _db.Execute(query1, new { session_id, school_id, status_default, updated_by });
            return true;
        }
        public override bool Delete(int id)
        {
            _sb.Clear();
            var status = ActiveStatus.INACTIVE;
            string query = _sb.AppendFormat("Update {0} set status=@status where session_id=@id ", DBTables.SESSION).ToString();
            _db.Execute(query, new {  status, id });
            return true;
        }
    }
}