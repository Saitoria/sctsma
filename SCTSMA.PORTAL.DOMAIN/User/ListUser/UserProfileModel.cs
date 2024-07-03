namespace SCTSMA.PORTAL.DOMAIN.User.ListUser
{
    public class UserProfileModel
    {
        public string? gender { get; set; }
        public string? bio { get; set; }
        public string? birth_date { get; set; }
        public string country { get; set; }
        public string city { get; set; }
        public string district { get; set; }
        public string? street_address { get; set; }
        public int? postal_code { get; set; }
        public string? profile_photo { get; set; }
        public string language { get; set; }
        public string account_type { get; set; }
        public int otp { get; set; }
        public DateTime created { get; set; }
        public DateTime updated { get; set; }
    }

}
