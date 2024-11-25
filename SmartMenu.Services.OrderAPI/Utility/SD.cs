namespace SmartMenu.Services.OrderAPI.Utility
{
    public class SD
    {
        public const string Status_Pending = "Pending";
        public const string Status_Payed = "Payed";
        public const string Status_InDelivery = "In Delivery";
        public const string Status_Delivered = "Delivered";
        public const string Status_Received = "Received";
        public const string Status_Cancelled = "Cancelled";



        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string RoleManager = "MANAGER";

        // Методи оплати
        public const string PaymentMethod_Card = "Card"; // Оплата карткою
        public const string PaymentMethod_OnPickup = "On Pickup"; // Оплата при отриманні

        // Методи доставки
        public const string DeliveryMethod_Courier = "Courier"; // Кур’єрська доставка
        public const string DeliveryMethod_SelfPickup = "Self Pickup"; // Самовивіз із магазину
    }
}
