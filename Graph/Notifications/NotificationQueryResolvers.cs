using Sukalibur.Graph.Orders;

namespace Sukalibur.Graph.Notifications
{
    [ExtendObjectType(typeof(Query))]
    public class NotificationQueryResolvers
    {
        [UsePaging]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Notification> GetNotifications(AppDbContext context)
        {
            return context.Notifications;
        }
    }
}
