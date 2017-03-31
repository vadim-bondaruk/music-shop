namespace Shop.BLL.Utils
{
    using System;
    using System.Net.Mail;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for sending mail
    /// </summary>
    public static class MailDispatch
    {
        /// <summary>
        /// From the email you are sending
        /// </summary>
        private static string _emailOfSendr = "q1music-shop@yandex.ru";

        /// <summary>
        /// Sendr name
        /// </summary>
        private static string _nameOfSendr = "Music Shop";

        /// <summary>
        /// Password from the box
        /// </summary>
        private static string _passwordBox = "f,kznjuhfa";

        /// <summary>
        /// SMTP client address
        /// </summary>
        private static string _smtpClient = "smtp.yandex.ru";

        /// <summary>
        /// Port for SMTP
        /// </summary>
        private static int _smtpPort = 587;


        /// <summary>
        /// Sends a message to the address usetEmail
        /// </summary>
        /// <param name="usetEmail">Recipient's email</param>
        /// <param name="subject">Subject</param>
        /// <param name="body">Body</param>
        /// <returns></returns>
        public static bool SendingMail (string usetEmail, string subject, string body)
        {
            if (usetEmail == null)
            {
                throw new Exception();
            }
            if (subject == null)
            {
                throw new Exception();
            }
            if (body == null)
            {
                throw new Exception();
            }
            try
            {
                MailAddress from = new MailAddress( _emailOfSendr, _nameOfSendr);
            
                MailAddress to = new MailAddress(usetEmail);
           
                MailMessage message = new MailMessage(from, to);
            
                message.Subject = subject;           
                message.Body = string.Format(body);
                message.IsBodyHtml = true;            
                SmtpClient smtp = new SmtpClient(_smtpClient, _smtpPort);            
                smtp.Credentials = new System.Net.NetworkCredential(_emailOfSendr, _passwordBox);

                //Use SSH Encryption
                smtp.EnableSsl = true;

                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                // TODO: write data to log
                return false;
            }
            
        }

    }
}
