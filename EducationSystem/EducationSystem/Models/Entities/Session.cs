namespace EducationSystem.Models.Entities
{
    public class Session
    {
        public int session_id { get; set; }
        public int school_id { get; set; }
        public string  isDefault { get; set; }

        public string start_date { get; set; }
        public string end_date { get; set; }
        public string status { get; set; }
        public int created_by { get; set; }
        public int updated_by { get; set; }
        public string created_date { get; set; }
        public string updated_date { get; set; }
    }
}