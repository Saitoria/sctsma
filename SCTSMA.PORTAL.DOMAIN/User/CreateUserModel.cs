namespace SCTSMA.PORTAL.DOMAIN.User
{
    public class CreateUserModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string phone_number { get; set; }
        public string gender { get; set; }
        public string bio { get; set; }
        public string birth_date { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string street_address { get; set; }
        public string postal_code { get; set; }
        public string language { get; set; } = "English";
    }

}
