using HotChocolate.Authorization;
using RTools_NTS.Util;

namespace Sukalibur.Graph.Notifications
{
    [ExtendObjectType(typeof(Mutation))]
    public class NotificationMutationResolvers
    {
        public NotificationMutationResolvers() { }

        [UseMutationConvention(PayloadFieldName = "data")]
        [Authorize]
        public async Task<FcmToken> UpdateFcmToken(UpdateFcmTokenInput input, [Service] NotificationService notificationService)
        {
            var token = await notificationService.UpdateFcmTokenAsync(input);
            return token;
        }

        [UseMutationConvention(PayloadFieldName = "data")]
        public async Task<FcmToken> RemoveFcmToken(RemoveFcmTokenInput input, [Service] NotificationService notificationService)
        {
            var token = await notificationService.RemoveFcmTokenAsync(input);
            return token;
        }
    }
}
