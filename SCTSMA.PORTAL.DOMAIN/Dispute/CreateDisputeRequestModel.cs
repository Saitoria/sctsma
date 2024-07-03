namespace SCTSMA.PORTAL.DOMAIN.Dispute
{
    public class CreateDisputeRequestModel
    {
        public int? id { get; set; }
        public int order { get; set; }
        public string description { get; set; }
        public byte[]? image { get; set; }
        public int status { get; set; }
    }

}
