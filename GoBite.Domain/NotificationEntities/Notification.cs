using GoBite.Domain.Entities;
using GoBite.Domain.Enums;

namespace GoBite.Domain.NotificationُEntities
{
    public class Notification
    {
        public int Id { get; set; }

        public string UserId { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = default!;


        public string Title { get; set; } = default!;
        public string Message { get; set; } = default!;

        public NotificationType NotificationType { get; set; }
        public NotificationTarget NotificationTarget { get; set; }

        public int? ReferenceId { get; set; }

        public bool IsRead { get; set; } = false;
        public DateTime? ReadAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
