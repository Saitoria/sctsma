using Microsoft.AspNetCore.Components;
using SCTSMA.PORTAL.DOMAIN.Order;

namespace SCTSMA.PORTAL.SERVICES.OrderManagement
{
    public class OrderManagementBase : ComponentBase
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public IEnumerable<OrderModel> Orders { get; set; } = Enumerable.Empty<OrderModel>();
        //General notifiers
        public bool isLoading { get; set; } = false;
        public string errorMessage { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                isLoading = true;
                errorMessage = string.Empty;

                // Initialize Orders with 10 sample Order data
                Orders = InitializeSampleOrders();

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                isLoading = false;
            }
        }

        private IEnumerable<OrderModel> InitializeSampleOrders()
        {
            var orders = new List<OrderModel>();
            var random = new Random();

            var orderStatuses = new[] { "Pending", "Shipped", "Delivered", "Cancelled" };
            var paymentMethods = new[] { "Credit Card", "PayPal", "Bank Transfer", "Cash on Delivery" };
            var paymentStatuses = new[] { "Paid", "Unpaid", "Refunded" };

            // Add 10 sample orders
            for (int i = 1; i <= 10; i++)
            {
                var order = new OrderModel
                {
                    OrderID = i,
                    UserID = i, // Assuming UserID corresponds to OrderID for simplicity
                    ProductID = i, // Assuming ProductID corresponds to OrderID for simplicity
                    ProductName = GetProductName(i), // Get realistic product names
                    Quantity = i,
                    TotalPrice = i * 100, // Just for demonstration
                    OrderStatus = orderStatuses[random.Next(orderStatuses.Length)], // Random order status
                    PaymentMethod = paymentMethods[random.Next(paymentMethods.Length)], // Random payment method
                    PaymentStatus = paymentStatuses[random.Next(paymentStatuses.Length)], // Random payment status
                    OrderDate = DateTime.Now.AddDays(-i) // Example order date
                };

                orders.Add(order);
            }

            return orders;
        }

        // Get realistic product names
        private string GetProductName(int index)
        {
            // Add your realistic product names here
            string[] productNames = { "Shoes", "Phones", "Laptop", "Headphones", "Smartwatch", "Camera", "Tablet", "Speaker", "Gaming Console", "Monitor" };

            // Just a simple mapping of index to product name
            return productNames[(index - 1) % productNames.Length];
        }
    }


}
