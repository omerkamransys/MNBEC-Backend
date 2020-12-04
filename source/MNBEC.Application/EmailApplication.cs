using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using MNBEC.ApplicationInterface;
using MNBEC.Domain;
using MNBEC.Domain.Enumerations;
using MNBEC.InfrastructureInterface;
using MNBEC.ServiceConnectorInterface;

namespace MNBEC.Application
{
    /// <summary>
    /// EmailApplication inherits from BaseApplication. It provides the implementation for Email related operations.
    /// </summary>
    public class EmailApplication : BaseApplication, IEmailApplication
    {
        #region Constructor
        /// <summary>
        /// EmailApplication initailizes object instance.
        /// </summary>
        /// <param name="emailInfrastructure"></param>
        /// <param name="emailServiceConnector"></param>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>

        public EmailApplication(IEmailInfrastructure emailInfrastructure, IEmailServiceConnector emailServiceConnector, IConfiguration configuration, ILogger<EmailApplication> logger) : base(configuration, logger)
        {
            this.EmailInfrastructure = emailInfrastructure;
            this.EmailServiceConnector = emailServiceConnector;           
        }
        #endregion

        #region Properties and Data Members
        /// <summary>
        /// EmailInfrastructure holds the Infrastructure object.
        /// </summary>
        /// 

        public IEmailInfrastructure EmailInfrastructure { get; }
        private IEmailServiceConnector EmailServiceConnector { get; }

      


        private const string DealerCode = "Dealer";
        private const string EmployeeCode = "Employee";
        private const string InspectorCode = "Inspector";
        private const string CustomerSupportEmailCode = "CustSuppor";
        private const string CustomerSupportAcknowledgementEmailCode = "CustAck";
        private const string EmployeeCreateEmailCode = "EmpCreate";
        private const string PasswordResetEmailCode = "PwdRes";
        private const string DealerCreateEmailCode = "DealerCrea";
        private const string AppointmentCreateEmailCode = "AppCreate";
        private const string NoEvaulationAppointmentCreateEmailCode = "NEAppCre";
        private const string AppointmentUpdationEmailCode = "AppUpdate";
        private const string AppointmentCancellationEmailCode = "AppCancel";
        private const string AppointmentReminderEmailCode = "AppRmndr";
        private const string NoEvaulationAppointmentReminderEmailCode = "NEAppRmndr";
        private const string DealerRegistration = "DealerReg";
        private const string VehiclePurchase = "VehPur";
        private const string FleetSalesCode = "FleetCon";
        private const string AuctionBidEmailTemplateNotificationCode = "DealerPB";
        private const string AuctionOutBidEmailTemplateNotificationCode = "DealerOB";
        private const string AuctionBuyItNowEmailTemplateNotificationCode = "DealerBN";
        private const string AuctionLastBidEmailTemplateNotificationCode = "DealerAL";
        private const string AuctionWonEmailTemplateNotificationCode = "DealerAW";
        private const string RandomEmailTemplateNotificationCode = "RandomEM";
        private const string ApproveTemplateNotificationCode = "APPRV";

        private const string RequesterNameTag = "{{RequesterName}}";
        private const string RequesterEmailTag = "{{RequesterEmail}}";
        private const string MessageTag = "{{Message}}";
        private const string ResetUrlKeyTag = "{{ResetUrlKey}}";
        private const string UserEmailKeyTag = "{{UserEmailKey}}";
        private const string CdnUrlKeyTag = "{{CdnUrl}}";
        private const string AmountKeyTag = "{{AMOUNT}}";
        private const string CdnFontUrlKeyTag = "{{CdnFontUrl}}";
        private const string CustomerPortalFAQURLKeyTag = "{{CustomerPortalFAQURL}}";
        private const string DealerRegisterEmailCode = "DEALERSUPP";
        private const string DealeRConfirmationEmailCode = "DEALEREMAI";


        private const string Message = @"Please find below the dealership\seller details:";

        private const string FullNameTag = "{{FullName}}";
        private const string MobileNumberTag = "{{MobileNumber}}";
        private const string EmailTag = "{{Email}}";
        private const string DealshipNameTag = "{{DealshipName}}";
        private const string DealershipLocationTag = "{{DealershipLocation}}";

        private const string CustomerNameKeyTag = "{{CustomerName}}";
        private const string CustomerEmailKeyTag = "{{CustomerEmail}}";
        private const string AppointmentTimeKeyTag = "{{AppointmentTime}}";
        private const string AppointmentDateKeyTag = "{{AppointmentDate}}";
        private const string DateKeyTag = "{{DATE}}";
        private const string AppointmentDayKeyTag = "{{Day}}";
        private const string InspectionCentreNameKeyTag = "{{InspectionCentreName}}";
        private const string AddressKeyTag = "{{Address}}";
        private const string CenterLocationUrlTag = "{{CenterLocationUrl}}";
        private const string CustomerPortalUrlTag = "{{CustomerPortalUrl}}";

        private const string BranchAddressUrlTag = "{{BranchAddress}}";
        private const string BranchNameUrlTag = "{{BranchName}}";
        private const string ResetPasswordURLTag = "{{ResetPasswordURL}}";

        private const string ValuationMinPriceTag = "{{ValuationMinPrice}}";
        private const string ValuationMaxPriceTag = "{{ValuationMaxPrice}}";
        private const string AppointmentIdTag = "{{AppointmentId}}";
        private const string displayTag = "{{display}}";
        private const string displayNotFoundTag = "{{displayNotFound}}";

        private const string MakeTag = "{{Make}}";
        private const string ModelTag = "{{Model}}";
        private const string TrimTag = "{{Trim}}";
        private const string RegistrationNumberTag = "{{RegistrationNumber}}";
        private const string PurchasePriceTag = "{{PurchasePrice}}";
        private const string CentreNameTag = "{{CentreName}}";
        private const string CentreAddressKeyTag = "{{CentreAddress}}";
        private const string CentreEmailTag = "{{CentreEmail}}";
        private const string CentrePhoneTag = "{{CentrePhone}}";
        private const string AddressTag = "{{Address}}";

        private const string IdKeyTag = "{{ID}}";
        private const string FullNameKeyTag = "{{FullName}}";
        private const string BidPriceKeyTag = "{{BIDPRICE}}";
        private const string BidUserNameKeyTag = "{{BidUserName}}";
        private const string BidMakeNameKeyTag = "{{BidMake}}";
        private const string AuctionIdKeyTag = "{{AUCTIONID}}";
        private const string MakeNameKeyTag = "{{MAKENAME}}";
        private const string ModelNameKeyTag = "{{MODELNAME}}";
        private const string TrimLevelKeyTag = "{{TRIMLEVEL}}";
        private const string NumberOfBidsTag = "{{NumberOfBidsTag}}";

        private const string EndPriceKeyTag = "{{ENDPRICE}}";
        private const string HighestBidKeyTag = "{{HIGHESTBID}}";
        private const string MaxBidKeyTag = "{{MAXBID}}";
        private const string CurrentBidKeyTag = "{{CURRENTBID}}";
        private const string AuctionEndTimeKeyTag = "{{AUCTIONENDTIME}}";
        private const string EndTimeKeyTag = "{{ENDTIME}}";
        private const string DealershipNameKeyTag = "{{DEALERSHIPNAME}}";
        private const string EmailKeyTag = "{{EMAIL}}";

        #endregion

        #region Interface IEmailApplication Implementation


        public async Task<bool> SendEmployeeCreationEmail(ApplicationUser employee, bool useLanguage)
        {
            var response = false;
            var emailTemplate = await GetNotificationEmailTemplate(EmailApplication.EmployeeCreateEmailCode);

            if (emailTemplate != null)
            {
                var email = new Email();

                email.Subject = (useLanguage) ? emailTemplate.Subject : emailTemplate.SubjectTranslation ?? emailTemplate.Subject;
                email.Message = (useLanguage) ? emailTemplate.Message : emailTemplate.MessageTranslation ?? emailTemplate.Message;
                email.Message = email.Message.Replace(EmailApplication.ResetPasswordURLTag, this.buildResetPasswordUrl(employee));
                email.Message = email.Message.Replace(EmailApplication.CdnUrlKeyTag, base.Configuration["Urls:CdnUrl"]);
                email.Message = email.Message.Replace(EmailApplication.CdnFontUrlKeyTag, base.Configuration["Urls:CdnFontUrl"]);
                email.To = employee.Email;

                this.EmailServiceConnector.Sendemail(email);
                response = true;
            }

            return response;
        }

        public async Task<bool> EmployeeSendForgotPasswordEmail(ApplicationUser employee, bool useLanguage)
        {
            return await this.SendForgotPasswordEmail(employee, EmailApplication.PasswordResetEmailCode, useLanguage, EmailApplication.EmployeeCode);

        }

        public async Task<bool> DealerSendForgotPasswordEmail(ApplicationUser dealer, bool useLanguage)
        {
            return await this.SendForgotPasswordEmail(dealer, EmailApplication.PasswordResetEmailCode, useLanguage, EmailApplication.DealerCode);
        }

        private async Task<bool> SendForgotPasswordEmail(ApplicationUser employee, string emailTemplateCode, bool useLanguage, string applicationUser)
        {
            var response = false;

            // Common method for getting template from database based email template code
            var emailTemplate = await GetNotificationEmailTemplate(emailTemplateCode);
            if (emailTemplate != null)
            {
                var email = new Email();

                email.Subject = (useLanguage) ? emailTemplate.Subject : emailTemplate.SubjectTranslation ?? emailTemplate.Subject;
                email.Message = (useLanguage) ? emailTemplate.Message : emailTemplate.MessageTranslation ?? emailTemplate.Message;
                var resetPasswordURL = GetResetPasswordURL(employee, applicationUser);
                email.Message = email.Message.Replace(EmailApplication.ResetPasswordURLTag, resetPasswordURL);
                email.Message = email.Message.Replace(EmailApplication.CdnUrlKeyTag, base.Configuration["Urls:CdnUrl"]);
                email.Message = email.Message.Replace(EmailApplication.CdnFontUrlKeyTag, base.Configuration["Urls:CdnFontUrl"]);
                email.To = employee.Email;
                //TODO: Need to update email sending logic
                this.EmailServiceConnector.Sendemail(email);
                response = true;
            }
            return response;
        }

        private string GetResetPasswordURL(ApplicationUser employee, string applicationUser)
        {
            if (EmailApplication.DealerCode.Equals(applicationUser))
            {
                return this.buildDealerResetPasswordUrl(employee);
            }
            else if (EmailApplication.InspectorCode.Equals(applicationUser))
            {
                return this.buildInspectorResetPasswordUrl(employee);
            }
            else
            {
                return this.buildResetPasswordUrl(employee);
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> SendDealerCreationEmail(ApplicationUser employee, bool useLanguage)
        {
            return await this.DealerSendResetPasswordEmail(employee, EmailApplication.DealerCreateEmailCode, useLanguage);

        }

        public async Task<bool> DealerSendResetPasswordEmail(ApplicationUser dealer, string emailTemplateCode, bool useLanguage)
        {
            var response = false;

            // Common method for getting template from database based email template code
            var emailTemplate = await GetNotificationEmailTemplate(emailTemplateCode);
            if (emailTemplate != null)
            {
                var email = new Email();

                email.Subject = (useLanguage) ? emailTemplate.Subject : emailTemplate.SubjectTranslation ?? emailTemplate.Subject;
                email.Message = (useLanguage) ? emailTemplate.Message : emailTemplate.MessageTranslation ?? emailTemplate.Message;

                email.Message = email.Message.Replace(EmailApplication.ResetPasswordURLTag, this.buildDealerResetPasswordUrl(dealer));
                email.Message = email.Message.Replace(EmailApplication.DealershipNameKeyTag, dealer.DealershipName);
                email.Message = email.Message.Replace(EmailApplication.FullNameTag, dealer.FirstName);

                email.Message = email.Message.Replace(EmailApplication.ResetPasswordURLTag, this.buildDealerResetPasswordUrl(dealer));
                email.Message = email.Message.Replace(EmailApplication.DealershipNameKeyTag, dealer.DealershipName);
                email.Message = email.Message.Replace(EmailApplication.FullNameTag, dealer.FirstName);
                email.Message = email.Message.Replace(EmailApplication.CdnUrlKeyTag, base.Configuration["Urls:CdnUrl"]);
                email.Message = email.Message.Replace(EmailApplication.CdnFontUrlKeyTag, base.Configuration["Urls:CdnFontUrl"]);
                email.To = dealer.Email;
                //TODO: Need to update email sending logic
                this.EmailServiceConnector.Sendemail(email);
                response = true;
            }
            return response;
        }

        private string buildMapUrl(string longitude, string latitude)
        {
            return $@"{base.Configuration["Urls:MapUrl"]}?q={longitude},{latitude}";
        }



        private string buildResetPasswordUrl(ApplicationUser user)
        {
            return $@"{base.Configuration["Urls:AdminPortalUrl"]}?UserId={user.UserId}&code={user.ResetUrlKey}";
        }

        private string buildInspectorResetPasswordUrl(ApplicationUser user)
        {
            return $@"{base.Configuration["Urls:InspectorPortalResetPasswordUrl"]}?UserId={user.UserId}&code={user.ResetUrlKey}";
        }

        private string buildDealerResetPasswordUrl(ApplicationUser user)
        {
            return $@"{base.Configuration["Urls:DealerPortalUrl"]}?UserId={user.UserId}&code={user.ResetUrlKey}";
        }       

        private Email EmailFactory(ApplicationUser applicationUser, bool useLanguage, EmailTemplate emailTemplate)
        {
            var email = new Email();

            email.Subject = (useLanguage) ? emailTemplate.Subject : emailTemplate.SubjectTranslation ?? emailTemplate.Subject;
            email.Message = (useLanguage) ? emailTemplate.Message : emailTemplate.MessageTranslation ?? emailTemplate.Message;
            email.Message = email.Message.Replace(EmailApplication.CdnUrlKeyTag, base.Configuration["Urls:CdnUrl"]);

            email.To = applicationUser.Email;
            return email;
        }

        public async Task<EmailTemplate> GetNotificationEmailTemplate(string code)
        {
            var emailRequest = new EmailTemplate
            {
                EmailTemplateCode = code
            };

            EmailTemplate emailTemplate = default(EmailTemplate);

                emailTemplate = await this.EmailInfrastructure.Get(emailRequest);
  

            return emailTemplate;
        }
        public async Task<List<EmailTemplate>> GetAllEmailTemplate()
        {
            return await this.EmailInfrastructure.GetAllEmailTemplate();
        }
       
        private string SetDate(DateTime? dt, bool useLanguage)
        {
            CultureInfo culture;
            string dateTime = dt.ToString();

            culture = new CultureInfo(base.Configuration["General:SecondaryLanguage"]);

            dateTime = String.Format(culture, "{0:dddd,d MMMM yyyy}", Convert.ToDateTime(dateTime));

            string[] splitDa = dateTime.Split(",");

            return splitDa[0] + " " + splitDa[1];
        }

 

        /// <summary>
        /// SetEmailSubjectAndBodyByCurrentCountry set the correct language for the Subject and Message for current Application
        /// It is used for the emails that are generated by background tasks
        /// </summary>
        /// <param name="emailTemplate"></param>
        /// <returns></returns>
        private EmailTemplate SetEmailSubjectAndBodyByCurrentCountry(EmailTemplate emailTemplate)
        {
            emailTemplate.Subject = Configuration["General:ApplicationCountry"] != "Turkey" ? emailTemplate.Subject : emailTemplate.SubjectTranslation ?? emailTemplate.Subject;
            emailTemplate.Message = Configuration["General:ApplicationCountry"] != "Turkey" ? emailTemplate.Message : emailTemplate.MessageTranslation ?? emailTemplate.Message;

            return emailTemplate;
        }

  

        #endregion
    }
}
