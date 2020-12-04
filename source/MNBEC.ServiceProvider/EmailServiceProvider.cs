using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using MNBEC.Domain;
using MNBEC.ServiceProviderInterface;

namespace MNBEC.ServiceProvider
{
    /// <summary>
    /// EmailServiceProvider class inherits BaseServiceProvider and provide implementation for Email Service.
    /// </summary>
    public class EmailServiceProvider : BaseServiceProvider, IEmailServiceProvider
    {
        #region Constructor
        /// <summary>
        ///  MakeController initializes class object.
        /// </summary>
        /// <param name="logger"></param>
        public EmailServiceProvider(IConfiguration configuration, ILogger<EmailServiceProvider> logger) : base(configuration, logger)
        {
        }
        #endregion

        #region Properties and Data Members

        private const string AuctionWonEmailAttachment = "2019 İş Takip Vekaleti-v2.doc";
        #endregion

        #region Methods
        /// <summary>
        /// Send process Email and send to service provider.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<bool> Send(Email message)
        {
            var smtpClient = new SmtpClient
            {
                Host = base.Configuration["Email:Host"], // set your SMTP server name here
                Port = Convert.ToInt32(base.Configuration["Email:Port"]), // Port 
                EnableSsl = Convert.ToBoolean(base.Configuration["Email:EnableSsl"]),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(base.Configuration["Email:EmailID"], base.Configuration["Email:Password"])
            };

            using (var mailMessage = new MailMessage(base.Configuration["Email:EmailID"], message.To, message.Subject, message.Message))
            {
                mailMessage.IsBodyHtml = true;

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    var messageTo = message.To ?? " (EMPTY) ";
                    var messageSubject = message.Subject ?? " (EMPTY) ";
                    this.Logger.LogError("Failed to send email in MNBEC.ServiceProvider.EmailServiceProvider.Send: EmailTo:" + messageTo + "; Subject" + messageSubject + ";", ex);
                }
            }

            return true;
        }

        /// <summary>
        /// This method is called from service connector for sending email
        /// </summary>
        /// <param name="message"></param>
        public void Sendemail(Email message)
        {
            if (base.Configuration["Email:EmailTrigger"].ToString().ToUpper().Equals("TRUE"))
            {
                sendEmail(message);
            }
           
        }

        private bool sendEmail(Email message)
        {
            var smtpClient = new SmtpClient
            {
                Host = base.Configuration["Email:Host"], // set your SMTP server name here
                Port = Convert.ToInt32(base.Configuration["Email:Port"]), // Port 
                EnableSsl = Convert.ToBoolean(base.Configuration["Email:EnableSsl"]),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(base.Configuration["Email:EmailID"], base.Configuration["Email:Password"])
            };

            using (var mailMessage = new MailMessage(base.Configuration["Email:EmailID"], message.To, message.Subject, message.Message))
            {
                mailMessage.IsBodyHtml = true;

                //This attachment is only for AuctionWon Email
                if (message.HasAttachment && File.Exists(AuctionWonEmailAttachment))
                {
                    mailMessage.Attachments.Add(new Attachment(AuctionWonEmailAttachment));
                }

                try
                {
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    var messageTo = message.To ?? " (EMPTY) ";
                    var messageSubject = message.Subject ?? " (EMPTY) ";
                    this.Logger.LogError("Failed to send email in MNBEC.ServiceProvider.EmailServiceProvider.sendEmail: EmailTo:" + messageTo + "; Subject" + messageSubject + ";", ex);

                }
            }

            return true;
        }



        private AlternateView getAlternateView(String filePath, string htmlBody)
        {
            LinkedResource res = new LinkedResource(filePath, "image/png");
            res.ContentId = "logo001";
            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(htmlBody, null, MediaTypeNames.Text.Html);
            alternateView.LinkedResources.Add(res);
            return alternateView;
        }


        #endregion
    }
}
