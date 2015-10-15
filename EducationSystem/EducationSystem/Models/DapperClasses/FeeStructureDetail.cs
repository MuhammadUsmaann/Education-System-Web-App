using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper;

namespace EducationSystem.Models.DapperClasses
{
    public class FeeStructureDetail : IMethods<FeeStructure>
    {
        public override IEnumerable<FeeStructure> GetAll(int school_id)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* FROM {0} JOIN {1} ON {1}.school_id= {0}.school_id WHERE {0}.school_id= @school_id ", DBTables.FEE_STRUCTURE, DBTables.SCHOOL).ToString();
            var result = _db.Query<FeeStructure>(query, new { school_id });
            return result;
        }
        
        public override IEnumerable<FeeStructure> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }

        public override FeeStructure FindSingle(int id, int school_id)
        {
            throw new NotImplementedException();
        }
        public override bool Insert(FeeStructure obj)
        {
            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Insert into {0} (type, school_id,updated_by,created_by,updated_date, created_date) values(@type,@school_id, @updated_by,@updated_by, {1},{1});", DBTables.FEE_STRUCTURE, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.type, obj.updated_by, obj.school_id });
            return true;
        }
        public override bool Update(FeeStructure obj)
        {
            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Update {0} Set fee = @fee ,updated_by=@updated_by,updated_date = {1} where {0}.school_id = @school_id and {0}.type = @type", DBTables.FEE_STRUCTURE, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.fee, obj.type, obj.updated_by, obj.school_id});
            return true;
        }
        public bool UpdateStudentFee(StudentFee obj)
        {
            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Update {0} Set {0}.paid_status=@paid_status,paid_date=@paid_date, updated_by=@updated_by,updated_date = {1} where {0}.school_id = @school_id and {0}.student_fee_id=@student_fee_id", DBTables.STUDENT_FEE, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.fee, obj.paid_status, obj.type, obj.updated_by, obj.paid_date, obj.school_id, obj.student_fee_id });
            return true;
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
        public Boolean Delete(StudentFee obj)
        {
            _sb.Clear();
            string query_for_user = _sb.AppendFormat("Update {0} Set {0}.paid_status=@paid_status,month=@month, updated_by=@updated_by,updated_date = {1} where {0}.school_id = @school_id and {0}.student_id=@student_id and {0}.class_id = @class_id", DBTables.STUDENT_FEE, "GETDATE()").ToString();
            var user_id = _db.Query<int>(query_for_user, new { obj.paid_status, obj.updated_by, obj.month, obj.school_id, obj.student_id, obj.class_id });
            return true;
        }
        ~FeeStructureDetail()
        {
            _db.Dispose();
        }
    }
}