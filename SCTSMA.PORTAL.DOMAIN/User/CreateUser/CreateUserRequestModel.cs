namespace SCTSMA.PORTAL.DOMAIN.User.CreateUser
{
    public class CreateUserRequestModel
    {
        public string username { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string account_type { get; set; } = "Business";
        public string password { get; set; }
        public string country { get; set; } = "Tanzania";
        public string city { get; set; } = "Dar es salaam";
        public string district { get; set; } = "Kinondoni";
        public string language { get; set; } = "English";
    }

}
