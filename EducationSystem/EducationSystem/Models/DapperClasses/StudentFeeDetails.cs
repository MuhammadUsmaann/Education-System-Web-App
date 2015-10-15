using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace EducationSystem.Models.DapperClasses
{
    public class StudentFeeDetails:IMethods<StudentFee>
    {
        public override IEnumerable<StudentFee> GetAll(int school_id)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<StudentFee> GetAll(int school_id, int student_id, int class_id, int session_id = -1)
        {
            _sb.Clear();
            string q = "SELECT {0}.* FROM {0} , {1} Where {0}.school_id =@school_id and {0}.class_id=@class_id and {0}.student_id=@student_id and {1}.user_id = {0}.student_id and {1}.status = '" + ActiveStatus.ACTIVE + "'";

            if (session_id > 0)
            {
                q += " and {0}.session_id=@session_id ";
            }
            string query = _sb.AppendFormat(q, DBTables.STUDENT_FEE, DBTables.USERS).ToString();
            var result = _db.Query<StudentFee>(query, new { school_id, student_id, class_id, session_id });
            return result;
        }
        public override IEnumerable<StudentFee> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<StudentFee> GetAll(int school_id, string stds_ids)
        {
            string sq = "select sf.* from {0} sf"
                        +" join {1} on {1}.user_id = sf.student_id and {1}.status = @ACTIVE"
                        +" join {2} on {2}.school_id = sf.school_id and sf.school_id = @school_id"
                        +" where sf.student_id in {3}";
            _sb.Clear();
            string query = _sb.AppendFormat(sq , DBTables.STUDENT_FEE, DBTables.USERS , DBTables.SCHOOL, stds_ids).ToString();
            var result = _db.Query<StudentFee>(query, new { school_id, ActiveStatus.ACTIVE });
            return result;
        }
        public override StudentFee FindSingle(int id, int school_id)
        {
            throw new NotImplementedException();
        }

        public override bool Insert(StudentFee obj)
        {
            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Insert into {0} (month,type,paid_status,student_id, school_id,class_id,session_id,updated_by,created_by,updated_date, created_date) values(@month,@type,@paid_status,@student_id, @school_id,@class_id,@session_id, @updated_by,@updated_by, {1},{1});", DBTables.STUDENT_FEE, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.type, obj.paid_status, obj.school_id, obj.student_id, obj.updated_by, obj.class_id ,obj.session_id, obj.month});
            return true;
        }

        public override bool Update(StudentFee obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}