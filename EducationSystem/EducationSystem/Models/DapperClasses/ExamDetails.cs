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
    public  class ExamDetails:IMethods<Exam>
    {
        public override IEnumerable<Exam> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* , {1}.name AS school_name FROM {0} JOIN {1} ON {0}.school_id = {1}.id where {1}.id = {2}", DBTables.EXAMS, DBTables.SCHOOL, school_id).ToString();
            var result = _db.Query<Exam>(query);
            return result;
        }
        public IEnumerable<SubjectExam> FindMarksOnCondition(int school_id, int class_id, int subject_id, int exam_id)
        {
            _sb.Clear();
            string q = "SELECT {0}.*,{1}.name AS school_name, {3}.name AS subject_name, {2}.name AS class_name ,CONCAT({5}.first_name , ' ' ,{5}.last_name) AS student_name,{5}.user_id as student_id, {4}.name AS exam_name FROM {0}"
                        + " JOIN {1} ON {0}.school_id = {1}.id"
                        + " JOIN {2} ON {0}.class_id = {2}.class_id"
                        + " JOIN {3} ON {0}.subject_id = {3}.subject_id"
                        + " JOIN {4} ON {0}.exam_id = {4}.exam_id "
                        + " JOIN {5} ON {0}.school_id = {1}.id "
                        + " Where {0}.student_id = {5}.user_id and {1}.id = " + school_id + " and {2}.class_id = " + class_id + " and {3}.subject_id = " + subject_id + " and {4}.exam_id =" + exam_id;
            string query = _sb.AppendFormat(q, DBTables.SUBJECT_MARKS, DBTables.SCHOOL, DBTables.CLASS, DBTables.SUBJECT, DBTables.EXAMS, DBTables.STUDENT).ToString();
            var result = _db.Query<SubjectExam>(query);
            return result;
        }
        public override Exam FindSingle(int id, int schoo_id)
        {
            string query = _sb.AppendFormat(("SELECT {0}.* , {1}.name AS school_name FROM {0} JOIN {1} ON {0}.school_id = {1}.id where {0}.exam_id ={2} and {1}.id = {3}"), DBTables.EXAMS, DBTables.SCHOOL, id,schoo_id).ToString();
            var result = _db.Query<Exam>(query).FirstOrDefault<Exam>();
            return result;
        }

        //public  override IEnumerable<Exam> GetAll()
        //{
        //    _sb.Clear();
        //    string query = _sb.AppendFormat("SELECT {0}.* , {1}.name AS school_name FROM {0} JOIN {1} ON {0}.school_id = {1}.id", DataBaseTables.EXAMS, DataBaseTables.SCHOOL).ToString();
        //    var result = _db.Query<Exam>(query);
        //    return result;
        //}

        public  override IEnumerable<Exam> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }

        //public  override Exam FindSingle(int id)
        //{
        //    string query = _sb.AppendFormat(("SELECT {0}.* , {1}.name AS school_name FROM {0} JOIN {1} ON {0}.school_id = {1}.id where {0}.exam_id =" + id), DataBaseTables.EXAMS, DataBaseTables.SCHOOL).ToString();
        //    var result = _db.Query<Exam>(query).FirstOrDefault<Exam>();
        //    return result;
        //}

        public  override bool Insert(Exam obj)
        {
            string query = _sb.AppendFormat("Insert into {0} (name, comment, school_id, date, created_by, updated_by,created_date, updated_date) values(@name, @comment, @school_id,@date, {1},{1}, {2},{2})", DBTables.EXAMS,obj.created_by,"GETDATE()").ToString();
            _db.Execute(query, new { obj.name, obj.comment, obj.school_id, obj.date});
            return true;
        }

        public  override bool Update(Exam obj)
        {
            string query = _sb.AppendFormat("update {0} set name=@name,school_id=@school_id,comment=@comment, updated_by=@updated_by, updated_date=GETDATE(), where exam_id =@exam_id", DBTables.EXAMS).ToString();
            _db.Execute(query, new { obj.name, obj.school_id, obj.comment, obj.updated_by, obj.exam_id });
            return true;
        }

        public  override bool Delete(int id)
        {
            string query = _sb.AppendFormat("Delete from {0} where exam_id = @id", DBTables.EXAMS).ToString();
            _db.Execute(query, new { id });
            return true;
        }

        public bool UpdateStudentMarks(int id, int marks , string comment = "") {

            string q = "Update {0} set {0}.marks = @marks , {0}.comment='@comment' where {0}.id = @id";
            _sb.Clear();
            string query = _sb.AppendFormat(q, DBTables.SUBJECT_MARKS).ToString();
            var result = _db.Execute(query, new { id,marks,comment });
            return true;
        }

        public bool UpdateExamSubjectMarks(SubjectExam obj)
        {
            _sb.Clear();
            string q = "Update {0} Set {0}.marks={1} ,{0}.comment = '{4}' where subject_exam.id = {2} and {0}.school_id = {3}";
            string query = _sb.AppendFormat(q, DBTables.SUBJECT_EXAMS, obj.marks, obj.id, obj.school_id,obj.comment).ToString();
            var res = _db.Execute(query);
            return true;
        }

        public IEnumerable<SubjectExam> FindOnCondition(int school_id, int class_id, int subject_id)
        {
            _sb.Clear();
            string q = "SELECT {0}.*,dbo.schools.name AS school_name, dbo.subjects.name AS subject_name, dbo.classes.name AS class_name , dbo.exams.name AS exam_name FROM {0}"
                        + " JOIN {1} ON {0}.school_id = {1}.id"
                        + " JOIN {2} ON {0}.class_id = {2}.class_id"
                        + " JOIN {3} ON {0}.subject_id = {3}.subject_id"
                        + " JOIN {4} ON {0}.exam_id = {4}.exam_id "
                        + " Where {1}.id = "+school_id+" and {2}.class_id = "+class_id+" and {3}.subject_id = "+subject_id;
            string query = _sb.AppendFormat(q, DBTables.SUBJECT_EXAMS, DBTables.SCHOOL, DBTables.CLASS, DBTables.SUBJECT, DBTables.EXAMS).ToString();
            var result = _db.Query<SubjectExam>(query);
            return result;
        }
        public bool InsertSubjectExam(int school_id, int subject_id, int exam_id , int class_id, int user_id)
        {
            string q = "INSERT INTO dbo.subject_exam (subject_id ,exam_id,school_id,marks,created_by,updated_by,created_date,updated_date,class_id)"
                        + "VALUES (@subject_id,@exam_id,@school_id,0,{0},{0},{1},{1},@class_id)";
            _sb.Clear();
            string query = _sb.AppendFormat(q, user_id, "GETDATE()").ToString();
            var res = _db.Execute(query, new { subject_id, exam_id, school_id, class_id });
            return true;
        }
        public bool InsertSubjectMarks(int school_id, int subject_id, int exam_id, int class_id, int student_id, int user_id)
        {
            string q = "INSERT INTO {2} (student_id,subject_id ,exam_id,school_id,marks,created_by,updated_by,created_date,updated_date,class_id)"
                        + "VALUES (@student_id,@subject_id,@exam_id,@school_id,0,{0},{0},{1},{1},@class_id)";
            _sb.Clear();
            string query = _sb.AppendFormat(q, user_id, "GETDATE()", DBTables.SUBJECT_MARKS).ToString();
            var res = _db.Execute(query, new { student_id, subject_id, exam_id, school_id, class_id });
            return true;
        }

        ~ExamDetails()
        {
            _db.Dispose();
        }
    }
}