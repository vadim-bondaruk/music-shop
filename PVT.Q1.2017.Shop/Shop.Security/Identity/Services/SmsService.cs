namespace PVT.Q1._2017.Shop.Security.Identity.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Sms service
    /// </summary>
    public class SmsService : IIdentityMessageService
    {
        /// <summary>
        /// Send sms
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }
}