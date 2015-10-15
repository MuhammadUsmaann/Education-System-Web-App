using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace EducationSystem.Models.DapperClasses
{
    public class AttendanceDetails:IMethods<Attendance>
    {

        public override IEnumerable<Attendance> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.*,CONCAT({1}.first_name , ' ' ,{1}.last_name) AS teacher_name, {2}.name AS school_name FROM {0} JOIN {2} ON {2}.id= {0}.school_id JOIN {1} ON {0}.teacher_id = {1}.user_id WHERE {1}.role_code = '{3}' and {2}.id = {4}", DBTables.CLASS, DBTables.ADMIN, DBTables.SCHOOL, RoleCode.TEACHER, school_id).ToString();
            var result = _db.Query<Attendance>(query);
            return result;
        }
        public IEnumerable<Attendance> GetAll(int school_id, int class_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.school_id = {0}.school_id WHERE {0}.school_id = {2} and {0}.class_id = {3}", DBTables.STUDENT_ATTENDANCE, DBTables.SCHOOL, school_id, class_id).ToString();
            var result = _db.Query<Attendance>(query);
            return result;
        }
        public IEnumerable<Attendance> GetAll(int school_id, int class_id, string date)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.school_id = {0}.school_id WHERE {0}.school_id = {2} and {0}.class_id = {3} and {0}.date = '{4}' order by {0}.date desc", DBTables.STUDENT_ATTENDANCE, DBTables.SCHOOL, school_id, class_id, date).ToString();
            var result = _db.Query<Attendance>(query);
            return result;
        }
        public IEnumerable<Attendance> GetAll(int school_id, int class_id, string startdate, string enddate)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.school_id = {0}.school_id WHERE {0}.school_id = @school_id and {0}.class_id = @class_id and {0}.date >= @startdate and {0}.date <= @enddate", DBTables.STUDENT_ATTENDANCE, DBTables.SCHOOL).ToString();
            var result = _db.Query<Attendance>(query, new { school_id, class_id, startdate, enddate });
            return result;

            //_sb.Clear();
            //string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.id = {0}.school_id WHERE {0}.school_id = {2} and {0}.class_id = {3} and {0}.date = '{4}' order by {0}.date desc", DataBaseTables.STUDENT_ATTENDANCE, DataBaseTables.SCHOOL, school_id, class_id,date).ToString();
            //var result = _db.Query<Attendance>(query);
            //return result;
        }
        public IEnumerable<Attendance> GetAll(int school_id, int class_id, int student_id, string startdate,string enddate)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.school_id = {0}.school_id WHERE {0}.school_id = @school_id and {0}.class_id = @class_id and {0}.student_id = @student_id and {0}.date >= @startdate and {0}.date <= @enddate", DBTables.STUDENT_ATTENDANCE, DBTables.SCHOOL).ToString();
            var result = _db.Query<Attendance>(query, new { school_id, class_id, student_id, startdate, enddate });
            return result;
        }

        public override IEnumerable<Attendance> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }

        public override Attendance FindSingle(int student_id, int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.school_id = {0}.school_id WHERE {0}.school_id = @school_id and {0}.student_id = @student_id", DBTables.STUDENT_ATTENDANCE, DBTables.SCHOOL).ToString();
            var result = _db.Query<Attendance>(query, new { school_id, student_id }).FirstOrDefault();
            return result;
        }
        public Attendance FindSingle(int student_id, int school_id, string date)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.id = {0}.school_id WHERE {0}.school_id = @school_id and {0}.student_id = @student_id and {0}.date = @date", DBTables.STUDENT_ATTENDANCE, DBTables.SCHOOL).ToString();
            var result = _db.Query<Attendance>(query, new { school_id, student_id, date }).FirstOrDefault();
            return result;
        }

        public override bool Insert(Attendance atnd)
        {
            _sb.Clear();
            _sb.AppendFormat("Insert into {0} (student_id,class_id,school_id,date,updated_date,created_date,updated_by,created_by,status) values(@student_id,@class_id,@school_id,@date,{1},{1},{2},{2},@status);", DBTables.STUDENT_ATTENDANCE, "GETDATE()", atnd.updated_by);
            var query = _sb.ToString();
            var result = _db.Query<Attendance>(query, new { atnd.student_id, atnd.class_id, atnd.school_id, atnd.date, atnd.status });
            return true;
        }
        public Attendance InsertData(Attendance atnd)
        {
            _sb.Clear();
            _sb.AppendFormat("Insert into {0} (student_id,class_id,school_id,date,updated_date,created_date,updated_by,created_by,status) OUTPUT Inserted.attendance_id values(@student_id,@class_id,@school_id,@date,{1},{1},{2},{2},@status);", DBTables.STUDENT_ATTENDANCE, "GETDATE()", atnd.updated_by);
            var query = _sb.ToString();
            int attendance_id = _db.Query<int>(query, new { atnd.student_id, atnd.class_id, atnd.school_id, atnd.date, atnd.status }).Single();

            _sb.Clear();
            string qry = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.id = {0}.school_id WHERE {0}.attendance_id= @attendance_id", DBTables.STUDENT_ATTENDANCE, DBTables.SCHOOL).ToString();
            var result = _db.Query<Attendance>(qry, new { attendance_id }).FirstOrDefault();
            return result;
        }

        public bool Insert(IEnumerable<Attendance> attendence)
        {

            _sb.Clear();

            foreach (var atnd in attendence)
            {
                _sb.AppendFormat("Insert into {0} (student_id,class_id,school_id,date,updated_date,created_date,updated_by,created_by,status) values({1}, {2},{3}, '{4}',{5},{5}, {6},{6},{7});", DBTables.STUDENT_ATTENDANCE, atnd.student_id, atnd.class_id, atnd.school_id, atnd.date, "GETDATE()", atnd.updated_by,atnd.status);
            }

            var query = _sb.ToString();
            var result = _db.Query<Attendance>(query);
            return true;
        }
        public override bool Update(Attendance attendence)
        {
            _sb.Clear();
            var q = "UPDATE {0} SET {0}.status = @status, {0}.updated_by = @updated_by,{0}.updated_date = {1}  where {0}.student_id = @student_id and {0}.school_id = @school_id and {0}.class_id = @class_id and {0}.date = @date";
            var query = _sb.AppendFormat(q, DBTables.STUDENT_ATTENDANCE, "GETDATE()").ToString();
            var result = _db.Query<Attendance>(query, new { attendence.status, attendence.date, attendence.student_id, attendence.school_id, attendence.updated_by, attendence.class_id });
            return true;
        }
        public bool Update(int id, int status, int user_id)
        {
            _sb.Clear();
            var q = "UPDATE {0} SET {0}.status = @status, {0}.updated_by = @user_id,{0}.updated_date = {1}  where {0}.attendance_id = @id";
            var query = _sb.AppendFormat(q, DBTables.STUDENT_ATTENDANCE, "GETDATE()").ToString();
            var result = _db.Query<Attendance>(query, new { status, user_id, id});
            return true;
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}