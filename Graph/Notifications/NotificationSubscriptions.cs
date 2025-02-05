using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using Sukalibur.Shared;
using System.Security.Claims;

namespace Sukalibur.Graph.Notifications
{
    [ExtendObjectType(typeof(Subscriptions))]
    public class NotificationSubscriptions
    {
        private readonly AuthContext _authContext;
        public NotificationSubscriptions(AuthContext authContext)
        {
            _authContext = authContext;
        }
        public ValueTask<ISourceStream<Notification>> SubscribeToNotificationAdded(ITopicEventReceiver receiver)
        {
            var userId = _authContext.CurrentUser?.Id;
            return receiver.SubscribeAsync<Notification>($"users::{userId}::notifications");
        }

        [Subscribe(With = nameof(SubscribeToNotificationAdded))]
        public Notification NotificationAdded([EventMessage] Notification notif) => notif;
    }
}
