namespace GoBite.Domain.Enums
{

    public enum OrderStatusOptions
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Preparing = 4,
        Ready = 5,
        OnTheWay = 6,
        Delivered = 7,
        Cancelled = 8
    }


    public enum NotificationType
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Preparing = 4,
        Ready = 5,
        OnTheWay = 6,
        Delivered = 7,
        Cancelled = 8,
        PromotionCreated = 9,
        PromotionExpired = 10,

    }

    public enum NotificationTarget
    {
        Order = 1,
        Promotion = 2,
    }

    public enum DiscountType
    {
        Percentage = 1,
        FixedAmount = 2
    }

}
