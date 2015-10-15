using System;

namespace EducationSystem.Models
{

    /// <summary>
    /// Configuration class that we will use for configuartion of the project like server settings and Database settings etc 
    /// </summary>

    public static class Configuration
    {

        /* Usman's DB*/

        public static string USERNAME = "admin";
        public static string PASSWORD = "admin";
        public static string DBNAME = "education_system";
        public static string SERVERNAME = @"M-USMAN\SQLEXPRESS";

        
        public static string SESSIONNAME = "Session";
        public static string SESSION_ROLE = "role";
        public static string SESSION_USER_ID = "userid";
        public static string SESSION_USER_FULLNAME = "fullname";
        public static string SESSION_USER_SCHOOL_ID = "school_id";
        public static string SESSION_USER_SCHOOL_CODE = "school_CODE";
        public static string SESSION_USER_SCHOOL_NAME = "school_name";
        public static string SESSION_USER_USERNAME = "username";
        public static string SESSION_SESSION_ACTIVE = "session_active";
        public static string SESSION_SESSION_ID = "session_id";

        public static bool DEVELOPMENT_MODE = true;

        public const string DATE_FORMAT = "MM/dd/yyyy";

    }
    public static class DBTables
    {
        public const string SCHOOL = "schools";
        public const string STUDENT = "students";
        public const string PARENT = "parents";
        public const string TEACHER = "teachers";
        public const string CLASS = "classes";
        public const string SESSION = "session";
        public const string ROLES = "roles";
        public const string USERS = "users";
        public const string ADMIN = "admins";
        public const string COUNTRIES = "countries";
        public const string SUBJECT = "subjects";
        public const string EXAMS = "exams";
        public const string SUBJECT_EXAMS = "subject_exam";
        public const string SUBJECT_MARKS = "subject_marks";
        public const string STUDENT_FEE = "student_fee";
        public const string STUDENT_ATTENDANCE = "attendance";
        public const string FEE_STRUCTURE = "fee";
    }
    public static class FeeType
    {
        public const string ADMISSION_FEE = "00001";
        public const string LAB_CHARGES = "00002";
        public const string EXAMINATION_FEE = "00003";
        public const string OTHER_CHARGES = "00004";
        public const string MONTHLY_FEE = "00005";
    }
    public static class FeePaidStatus
    {
        public const string UNPAID = "00001";
        public const string PAID = "00002";
    }
    public static class RoleCode
    {
        public const string OWNER = "00001";
        public const string ADMIN = "00002";
        public const string TEACHER = "00003";
        public const string PARENT = "00004";
        public const string STUDENT = "00005";
    }
    public static class AttendanceStatus
    {
        public const int PRESENT = 1;
        public const int ABSENT = 0;
        public const int NONE = -1;
    }
    public static class ActiveStatus
    {
        public const string ACTIVE = "00001";
        public const string INACTIVE = "00002";
    }
    public static class GenderCode
    {
        public const string MALE = "00001";
        public const string FEMALE = "00002";
        public const string OTHER = "00003";
    }
    public static class MonthCode
    {
        public const string JANUARY = "00001";
        public const string FEBRAURY = "00002";
        public const string MARCH = "00003";
        public const string APRIL = "00004";
        public const string MAY = "00005";
        public const string JUNE = "00006";
        public const string JULY = "00007";
        public const string AUGUST = "00008";
        public const string SEPTEMBER = "00009";
        public const string OCTOBER = "000010";
        public const string NOVEMBER = "00011";
        public const string DECEMBER = "00012";
    }
}