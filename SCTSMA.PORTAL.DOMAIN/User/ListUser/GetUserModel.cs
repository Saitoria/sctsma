﻿namespace SCTSMA.PORTAL.DOMAIN.User.ListUser
{
    public class GetUserModel
    {
        public string first_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public bool is_active { get; set; }
        public DateTime date_joined { get; set; }
        public UserProfileModel profile { get; set; }
    }

}
