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
    public  class StudentDetails:IMethods<Student>
    {
        public override IEnumerable<Student> GetAll(int school_id)
        {
            string sq = "SELECT std.*, usr.*";
            string fq = " FROM {0} std, {1} usr";
            string wq = " WHERE std.school_id = @school_id and usr.status = @ACTIVE and usr.user_id = std.user_id and usr.role_code = '"+RoleCode.STUDENT+"'";
            _sb.Clear();
            string query = _sb.AppendFormat(sq + fq + wq, DBTables.STUDENT, DBTables.USERS, DBTables.CLASS).ToString();
            var result = _db.Query<Student>(query, new { school_id, ActiveStatus.ACTIVE });
            return result;
        }
        public IEnumerable<Student> GetAll(int school_id, int class_id)
        {
            string sq = "SELECT std.*";
            string fq = " FROM {0} std ,{1} cls, {2} usr";
            string wq = " WHERE std.school_id = @school_id and cls.class_id = @class_id and usr.status = @ACTIVE and usr.user_id = std.user_id and usr.role_code = '"+RoleCode.STUDENT+"'";
            _sb.Clear();
            string query = _sb.AppendFormat(sq + fq + wq, DBTables.STUDENT,DBTables.CLASS,  DBTables.USERS).ToString();
            var result = _db.Query<Student>(query, new { ActiveStatus.ACTIVE, school_id, class_id});
            return result;
        }
        public IEnumerable<Student> GetAll(int school_id, string class_id)
        {
            string sq = "select * from {0}";
            string fq = " join {1} on {1}.school_id = {0}.school_id "
                        + " join {2} on {2}.class_id = {0}.class_id and {2}.status = @ACTIVE"
                        + " join {3} on users.user_id = {0}.user_id";
            string wq = " where {2}.class_id in {4} and {1}.school_id = @school_id ";
            _sb.Clear();
            string query = _sb.AppendFormat(sq + fq + wq, DBTables.STUDENT, DBTables.SCHOOL, DBTables.CLASS, DBTables.USERS,class_id).ToString();
            var result = _db.Query<Student>(query, new { school_id, ActiveStatus.ACTIVE });
            return result;
        }
        public IEnumerable<Student> GetAllChildren(int school_id, int parent_id)
        {
            string sq = "select stds.* from {0} stds join {1} on {1}.user_id = stds.parent_id and {1}.status = @ACTIVE";
            string wq = " where stds.parent_id = @parent_id and stds.school_id = @school_id ";
            _sb.Clear();
            string query = _sb.AppendFormat(sq + wq, DBTables.STUDENT, DBTables.USERS).ToString();
            var result = _db.Query<Student>(query, new { school_id, ActiveStatus.ACTIVE, parent_id });
            return result;
        }
        public override Student FindSingle(int id, int school_id)
        {
            _sb.Clear();

            string sq = "SELECT std.*,schl.name AS school_name";
            string fq = " FROM {0} std, {1} schl, {2} usr";
            string wq = " WHERE schl.school_id = std.school_id and std.user_id =@id and usr.user_id = std.user_id and schl.school_id = @school_id and usr.status = @ACTIVE";
            string query = _sb.AppendFormat(sq + fq + wq, DBTables.STUDENT, DBTables.SCHOOL, DBTables.USERS).ToString();

            var result = _db.Query<Student>(query, new { id, school_id,ActiveStatus.ACTIVE }).FirstOrDefault<Student>();
            return result;
        }
        
        public override IEnumerable<Student> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }


        public override bool Insert(Student obj)
        {
            throw new Exception();
        }
        public  int Insert(Student obj, int nothing =0)
        {

            obj.role_code = RoleCode.STUDENT;

            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Insert into {0} (username, password, role_code,status,updated_date,updated_by,created_by, created_date, school_id) OUTPUT Inserted.user_id values(@first_name, @password,@role_code, @ACTIVE,{2},{1},{1},{2},@school_id);", DBTables.USERS, obj.created_by, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.first_name, obj.password, obj.role_code, obj.school_id, ActiveStatus.ACTIVE }).Single();

            var username = Utils.MakeUserName(user_id, obj.first_name);
            var sys_id = Utils.MakeID(user_id);

            _sb.Clear();
            string update = _sb.AppendFormat("Update {0} set username = @username where user_id=@user_id", DBTables.USERS).ToString();
            var u = _db.Query<int>(update, new { username, user_id });

            string insert = "(first_name, last_name,sys_id,birthday, school_id, user_id,address,gender, class_id, roll,parent_id,monthly_fee,discount,admission_fee, examination_fee,other_charges,updated_by,created_by,updated_date, created_date)";
            string values = "(@first_name, @last_name,@sys_id,@birthday, @school_id, @user_id,@address,@gender, @class_id, @roll,@parent_id,@monthly_fee,@discount,@admission_fee, @examination_fee,@other_charges,@updated_by,@created_by,GETDATE(),GETDATE())";
            _sb.Clear();
            string query_for_student = _sb.AppendFormat("Insert into {0} "+insert + " values" + values, DBTables.STUDENT).ToString();
            _db.Execute(query_for_student, new { obj.first_name, obj.last_name,sys_id, obj.birthday, obj.school_id,user_id, obj.address, obj.gender, obj.class_id, obj.roll, obj.parent_id, obj.monthly_fee, obj.discount, obj.admission_fee, obj.examination_fee, obj.other_charges, obj.updated_by, obj.created_by});
            return user_id;
        }

        public  override bool Update(Student obj)
        {
            obj.role_code = RoleCode.STUDENT;
            string user_query = "password=@password, updated_date=GETDATE(),updated_by=@updated_by";
            string student_query = "first_name=@first_name, last_name=@last_name,birthday=@birthday, address=@address,gender=@gender, class_id=@class_id, roll=@roll,parent_id=@parent_id,monthly_fee=@monthly_fee,discount=@discount,admission_fee=@admission_fee, examination_fee=@examination_fee,other_charges=@other_charges,updated_by=@updated_by";
            _sb.Clear();
            string uquery = _sb.AppendFormat("update {0} set " + user_query + " where user_id=@user_id and role_code=@role_code", DBTables.USERS).ToString();
            _sb.Clear();
            string dquery = _sb.AppendFormat("update {0} set " + student_query + " where user_id=@user_id", DBTables.STUDENT, obj.updated_by, "GETDATE()").ToString();
            _db.Execute(uquery, new { obj.password, obj.user_id,obj.updated_by,obj.role_code});
            _db.Execute(dquery, new { obj.first_name, obj.last_name,obj.birthday, obj.address, obj.gender, obj.class_id, obj.roll, obj.parent_id, obj.monthly_fee, obj.discount, obj.admission_fee, obj.examination_fee, obj.other_charges, obj.updated_by});
            return true;
        }

        public Boolean Update(int school_id, int student_id,int class_id, int discount, int month= -1)
        {
            string ss = "discount=@discount"; 
            if(month > 0)
            {
                ss += " , monthly_fee=@month";
            }
            _sb.Clear();
            string dquery = _sb.AppendFormat("update {0} set " + ss + "  where {0}.user_id=@student_id and {0}.school_id=@school_id and {0}.class_id=@class_id", DBTables.STUDENT, "GETDATE()").ToString();
            _db.Execute(dquery, new { discount, month, student_id, school_id, class_id });
            return true;
        }
        public  override bool Delete(int id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("update {0} set status=@INACTIVE where user_id = @id and role_code = '{1}'", DBTables.USERS, RoleCode.STUDENT).ToString();
            _db.Execute(query, new { id , ActiveStatus.INACTIVE});
            return true;
        }

        ~StudentDetails()
        {
            _db.Dispose();
        }
    }
}