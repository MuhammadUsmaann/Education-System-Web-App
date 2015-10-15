using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace EducationSystem.Models
{
    public abstract class IMethods<T>
    {
        public SqlConnection _db = DBModel.Get_DB_Connection();
        public StringBuilder _sb = new StringBuilder();
        public abstract IEnumerable<T> GetAll(int school_id);
        public abstract  IEnumerable<T> Find(params string[] conditions);
        public abstract T FindSingle(int id,int school_id);
        public abstract bool Insert(T obj);
        public abstract bool Update(T obj);
        public abstract bool Delete(int id);
    }
}