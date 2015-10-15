using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EducationSystem.Models;
using EducationSystem.Models.Entities;
using System.Data.SqlClient;
using Dapper;
using System.Text;

namespace EducationSystem.Models.DapperClasses
{
    public  class SchoolDetails : IMethods<School>
    {
        
        public  override IEnumerable<School> GetAll(int s=0)
        {
            _sb.Clear();
            string query = _sb.AppendFormat("SELECT {0}.* , {1}.name AS city_name FROM {0} JOIN {1} ON {0}.city_id = {1}.id",DBTables.SCHOOL,DBTables.EXAMS).ToString();
            var result = _db.Query<School>(query);
            return result;
        }
        public  override IEnumerable<School> Find(params string[] conditions)
        {
            string query = _sb.AppendFormat("select * from {0}", DBTables.SCHOOL).ToString();
            var result = _db.Query<School>(query);
            return result;
        }
        public  override School FindSingle(int id, int sc=0)
        {
            string query = _sb.AppendFormat(("select TOP 1 * from {0} where id =" + id),DBTables.SCHOOL).ToString();
            var result = _db.Query<School>(query).FirstOrDefault<School>();
            return result;
        }
        public  override bool Insert(School obj)
        {
            string query = _sb.AppendFormat("Insert into {0} (name, city_id, phone) values(@name, @city_id, @phone)", DBTables.SCHOOL).ToString();
            _db.Execute(query, new {obj.name, obj.city_id, obj.phone, obj.id });
            return true;
        }
        public  override bool Update(School obj)
        {
            string query = _sb.AppendFormat("update {0} set name=@name,city_id=@city_id,phone=@phone where id =@id", DBTables.SCHOOL).ToString();
            _db.Execute(query, new { obj.name, obj.city_id, obj.phone, obj.id });
            return true;
        }
        public  override bool Delete(int id)
        {
            string query = _sb.AppendFormat("Delete from {0} where id = @id",DBTables.SCHOOL).ToString();
            _db.Execute(query, new { id });
            return true;
        }


        ~SchoolDetails()
        {
            _db.Dispose();
        }
    }
}