using FirebaseAdmin.Messaging;
using HotChocolate.AspNetCore;
using HotChocolate.Authorization;
using HotChocolate.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Shared;
using System.Linq;

namespace Sukalibur.Graph.Notifications
{
    public class NotificationService : IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private readonly ITopicEventSender _topicEventSender;
        private readonly AuthContext _authContext;
        public NotificationService(IDbContextFactory<AppDbContext> contextFactory, ITopicEventSender topicEventSender, AuthContext authContext)
        {
            _context = contextFactory.CreateDbContext();
            _topicEventSender = topicEventSender;
            _authContext = authContext;
        }
        public ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }

        public async Task<Notification> AddNotificationAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();
            await _topicEventSender.SendAsync($"users::{notification.UserId}::notifications", notification);
            return notification;
        }

        public async Task<FcmToken> UpdateFcmTokenAsync(UpdateFcmTokenInput input)
        {
            var fcmToken = await _context.FcmTokens.Where(ft => ft.Token == input.Token).FirstOrDefaultAsync();
            if (fcmToken == null)
            {
                fcmToken = new FcmToken
                {
                    UserId = _authContext.CurrentUser!.Id,
                    Token = input.Token
                };
                _context.FcmTokens.Add(fcmToken);
            }
            fcmToken.UserId = _authContext.CurrentUser!.Id;
            await _context.SaveChangesAsync();
            return fcmToken;
        }

        public async Task SendPushNotifications(List<Message> messages)
        {
            var invalidTokens = new List<string>();
            foreach (var message in messages)
            {
                try
                {
                    var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);
                }
                catch (FirebaseMessagingException e)
                {
                    if (e.ErrorCode == FirebaseAdmin.ErrorCode.InvalidArgument && e.Message.Contains("registration token"))
                    {
                        invalidTokens.Add(message.Token);
                    }
                    Console.Error.WriteLine(e);
                }
            }
            if (invalidTokens.Count > 0)
            {
                await _context.FcmTokens.Where(t => invalidTokens.Contains(t.Token)).ExecuteDeleteAsync();
            }
        }

        public async Task<FcmToken> RemoveFcmTokenAsync(RemoveFcmTokenInput input)
        {
            var token = await _context.FcmTokens.Where(t => t.Token == input.Token).FirstOrDefaultAsync();
            if (token == null)
                throw new Exception("Token not found");
            _context.FcmTokens.Remove(token);
            await _context.SaveChangesAsync();
            return token;
        }
    }
}
