namespace SCTSMA.PORTAL.DOMAIN.Order
{
    public class OrderRequestModel
    {
        public int? id { get; set; }
        public string? status { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public int? tot_price { get; set; }
        public int? order_number { get; set; }
        public string? address { get; set; }
        //public bool? is_paid { get; set; }
        public byte[] image { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public int? buyer { get; set; }
        public int? seller { get; set; }
    }
}
