namespace SCTSMA.PORTAL.DOMAIN.Order
{
    public class OrderModel
    {
        public int? OrderID { get; set; }
        public int? UserID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal? TotalPrice { get; set; }
        public string? OrderStatus { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentStatus { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
