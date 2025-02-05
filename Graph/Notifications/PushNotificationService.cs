using Amazon.SimpleEmailV2.Model;
using FirebaseAdmin.Messaging;
using Microsoft.EntityFrameworkCore;
using Sukalibur.Graph.Payments;

namespace Sukalibur.Graph.Notifications
{
    public class PushNotificationService : IAsyncDisposable
    {
        private readonly AppDbContext _context;
        private readonly FirebaseMessaging _firebaseMessaging;
        public PushNotificationService(IDbContextFactory<AppDbContext> contextFactory, FirebaseMessaging firebaseMessaging)
        {
            _context = contextFactory.CreateDbContext();
            _firebaseMessaging = firebaseMessaging;
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task SendPaymentCreatedNotification(Payment payment)
        {
            var invalidTokens = new List<FcmToken>();
            var userId = payment.UserId;
            var tokens = await _context.FcmTokens.Where(ft => ft.UserId == userId).ToListAsync();
            foreach (var token in tokens)
            {
                try
                {
                    await _firebaseMessaging.SendAsync(new FirebaseAdmin.Messaging.Message
                    {
                        Token = token.Token,
                        Notification = new FirebaseAdmin.Messaging.Notification
                        {
                            Title = "Segera Lakukan Pembayaran",
                            Body = "Transaksi Anda sudah kami terima. Kami akan segera memproses setelah pembayaran selesai."
                        }
                    });
                }
                catch (FirebaseMessagingException e)
                {
                    if (e.ErrorCode == FirebaseAdmin.ErrorCode.InvalidArgument && e.Message.Contains("registration token"))
                    {
                        invalidTokens.Add(token);
                    }
                    Console.Error.WriteLine(e);
                }
                catch (Exception e) { }
            }
            if (invalidTokens.Count > 0)
            {
                _context.RemoveRange(invalidTokens);
                await _context.SaveChangesAsync();
            }
        }
    }
}
