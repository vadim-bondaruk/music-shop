namespace PVT.Q1._2017.Shop.Security.Identity.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Email service
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send a text message.
            return Task.FromResult(0);
        }
    }
}