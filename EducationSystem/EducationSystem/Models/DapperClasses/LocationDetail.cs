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
    public  class LocationDetail:IMethods<Country>
    {
        //public  override IEnumerable<Country> GetAll()
        //{
        //    string query = _sb.AppendFormat("select top 50 * from {0} ", DataBaseTables.CITIES).ToString();
        //    var result = _db.Query<Country>(query);
        //    return result;
        //}

        //public  override IEnumerable<Country> Find(params string[] conditions)
        //{
        //    throw new NotImplementedException();
        //}

        //public  override Country FindSingle(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public  override bool Insert(Country obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public  override bool Update(Country obj)
        //{
        //    throw new NotImplementedException();
        //}

        //public  override bool Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //~LocationDetail()
        //{
        //    _db.Dispose();
        //}
        public override IEnumerable<Country> GetAll(int school_id)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<Country> Find(params string[] conditions)
        {
            throw new NotImplementedException();
        }

        public override Country FindSingle(int id, int school_id)
        {
            throw new NotImplementedException();
        }

        public override bool Insert(Country obj)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Country obj)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}