using EducationSystem.Models.DapperClasses;
using EducationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Script.Serialization;

namespace EducationSystem.Models
{
    public static class Utils
    {
        
        public static string MakeID(int id)
        {
            return SessionHandler.GetSchoolCode() + "-"+ id.ToString().PadLeft(6, '0');
        }
        public static string MakeUserName(int id, string first_name)
        {
            return SessionHandler.GetSchoolCode() + "-" + id.ToString().PadLeft(6, '0') + "-" +first_name;
        }
        public static IEnumerable<Class> GetClasses(int school_id)
        {
            var classes = new ClassDetails().GetAll(school_id);
            return classes;
        }
        
        public static IEnumerable<Student> GetClassStudent(int school_id, int class_id)
        {
            var classes = new ClassDetails().GetAll(school_id);
            var length = classes.Count();
            string cls_ids = "(";
            int i = 0;
            foreach (var cls in classes)
            {
                cls_ids += cls.class_id;
                if ((i + 1) != length)
                {
                    cls_ids += ",";
                }
                i++;
            }
            cls_ids += ")";
            int indexOfComma = cls_ids.LastIndexOf(',');
            var _sd = new StudentDetails().GetAll(school_id, cls_ids);

            return _sd;
        }
        public static IEnumerable<Parent> GetParents(int school_id)
        {
            var Parents = new ParentDetails().GetAll(school_id);
            return Parents;
        }
        public static IEnumerable<Student> GetParentStudent(int school_id, int class_id)
        {
            var parents = new ParentDetails().GetAll(school_id);
            var length = parents.Count();
            string parent_ids = "(";
            int i = 0;
            foreach (var prnt in parents)
            {
                parent_ids += prnt.user_id;
                if ((i + 1) != length)
                {
                    parent_ids += ",";
                }
                i++;
            }
            parent_ids += ")";
            int indexOfComma = parent_ids.LastIndexOf(',');
            var _sd = new StudentDetails().GetAll(school_id, parent_ids);

            return _sd;
        }
        public static SchoolFeeStructure GetFeeStructure(int school_id)
        {
            var fees = new FeeStructureDetail().GetAll(school_id);
            SchoolFeeStructure feeStr = new SchoolFeeStructure();
            foreach (var fee in fees)
            {
                if (fee.type == FeeType.EXAMINATION_FEE)
                    feeStr.ExaminationFee = fee.fee;
                else if (fee.type == FeeType.ADMISSION_FEE)
                    feeStr.AdmissionFee = fee.fee;
                else if (fee.type == FeeType.OTHER_CHARGES)
                    feeStr.OtherCharges = fee.fee;
            }
            return feeStr;
        }
        public static string ToJSON(this object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(obj);
        }
        public static string Response(Boolean status, string message)
        {
            var response = "{status:" + status + ",message:" + message + "}";
            return response.ToJSON();
        }
        public static Boolean IsValidDate(this string date)
        {
            DateTime temp ;
            if(DateTime.TryParse(date, out temp))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean IsLowerDate(this string start,string end)
        {
            DateTime date1 = Convert.ToDateTime(start);
            DateTime date2 = Convert.ToDateTime(end);

            int result = DateTime.Compare(date1, date2);

            if (result <= 0)
            {
                return true;
            }
            else
            { return false; }

        }
        public static string GetDayName(this string date)
        {
            return Convert.ToDateTime(date).ToString("dddd");
        }
        public static int GetDayOfMonth(this string date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                return dt.Day;
            }
            catch
            {
                return -1;
            }
        }
        public static Boolean ComapareDate(this string date, string date2)
        {
            try
            {
                string dt1 = Convert.ToDateTime(date).ToString(Configuration.DATE_FORMAT);
                string dt2 = Convert.ToDateTime(date2).ToString(Configuration.DATE_FORMAT);

                return dt1 == dt2;
            }
            catch
            {
                return false;
            }
        }
        public static int GetDaysInMonth(this string date)
        {
            try
            {
                DateTime dt = Convert.ToDateTime(date);
                int year = dt.Year;
                int month = dt.Month;
                return DateTime.DaysInMonth(year, month);
            }
            catch
            {
                return -1;
            }
        }
        public static IEnumerable<String> GetMonths(string startDate, string endDate)
        {
            List<String> months = new List<String>();

            DateTime sDate = Convert.ToDateTime(startDate);
            DateTime eDate = Convert.ToDateTime(endDate);
            
            Boolean isTrue = true;
            while(isTrue)
            {
                var id = DateTime.Compare(sDate, eDate);
                if (id >= 0)
                {
                    isTrue = false;
                };
                var month = sDate.ToString("MMMM");
                sDate = sDate.AddMonths(1);
                months.Add(month);
            }
            return months;
        }
        public static void makeDateString(this string date, ref string startDate, ref string endDate)
        {
            DateTime dt = Convert.ToDateTime(date);

            int daysInMonth = date.GetDaysInMonth();
            string month = dt.Month < 10 ? (String.Concat("0", dt.Month)) : dt.Month.ToString();
            string year = dt.Year < 10 ? (String.Concat("0", dt.Year)) : dt.Year.ToString();
            string days = daysInMonth < 10 ? (String.Concat("0", daysInMonth)) : daysInMonth.ToString();
            startDate = String.Concat(month, "/01", "/", year);
            endDate = String.Concat(month, "/", days, "/", year);
        }
        public static void getDayAndDate(this string CurDate, ref string DayName, ref string reqDate, int addDays)
        {

            DateTime dt = Convert.ToDateTime(CurDate);

            DateTime sd = Convert.ToDateTime(String.Format("{0:" + Configuration.DATE_FORMAT + "}", CurDate.Trim()), CultureInfo.CurrentCulture);
            DateTime newDate = sd.AddDays(addDays);

            DayName = newDate.ToString("dddd");

            reqDate = newDate.ToString(Configuration.DATE_FORMAT);
        }
        public static string AddDaysInDate(this string date1, int day)
        {
            try
            {
                string date = Convert.ToDateTime(date1).AddDays(day).ToString(Configuration.DATE_FORMAT);

                return date;
            }
            catch
            {
                return null;
            }
        }
    }
}