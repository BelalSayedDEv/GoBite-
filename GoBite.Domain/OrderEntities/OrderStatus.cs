using GoBite.Domain.Enums;

namespace GoBite.Domain.OrderEntities
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public int DisplayOrder { get; set; }

        public OrderStatusOptions Status { get; set; }
        public ICollection<Order> orders = new List<Order>();

    }
}
