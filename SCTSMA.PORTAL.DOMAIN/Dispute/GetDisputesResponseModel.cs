namespace SCTSMA.PORTAL.DOMAIN.Dispute
{
    public class GetDisputesResponseModel
    {
        public int id { get; set; }
        public string? description { get; set; }
        public string? image { get; set; }
        public int? status { get; set; }
        public DateTime? created { get; set; }
        public DateTime? updated { get; set; }
        public int order { get; set; }
    }
}
