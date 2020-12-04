using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestSharp;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using MNBEC.Domain;
using MNBEC.ServiceProviderInterface;

namespace MNBEC.ServiceProvider
{
    /// <summary>
    /// SMSServiceProvider class inherits BaseServiceProvider and provide implementation for SMS Service.
    /// </summary>
    public class SMSServiceProvider : BaseServiceProvider, ISMSServiceProvider
    {
        #region Constructor
        /// <summary>
        /// SMSServiceProvider initializes class object.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="logger"></param>
        public SMSServiceProvider(IConfiguration configuration, ILogger<SMSServiceProvider> logger) : base(configuration, logger)
        {
        }
        #endregion

        #region Properties and Data Members
        #endregion

        #region Methods
        /// <summary>
        /// Send process SMS and send to service provider.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        /// 
        public async Task<bool> Send(SMS message)
        {
            //TODO: SMS send logic will be implemented after getting sms service details.
            try
            { 
                if (this.Configuration["General:ApplicationCountry"].ToString().Equals("Turkey"))
                {
                    await SendSMSUsingSmartMessageForTurkey(message);
                }
                else
                {
                    await SendSMSUsingJazzServiceForPaksitan(message);
                }

            }
            catch (Exception ex)
            {
                this.Logger.LogError("MNBEC.ServiceProvider.Send: " + ex.Message);
            }

            return true;
        }

        private async Task SendSMSUsingJazzServiceForPaksitan(SMS message)
        {
            string configURL = await Task.FromResult(GetSMSURL());

            configURL = configURL.Replace("{{USERNAME}}", this.Configuration["JazzMessage:Username"].ToString());
            configURL = configURL.Replace("{{PASSWORD}}", this.Configuration["JazzMessage:Password"].ToString());
            configURL = configURL.Replace("{{MASK}}", this.Configuration["JazzMessage:Mask"].ToString());
            configURL = configURL.Replace("{{RECEIVER}}", message.Receiver);
            configURL = configURL.Replace("{{MESSAGE}}", message.Message);

            var client = new RestClient(configURL);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cache-Control", "no-cache");

            IRestResponse response = client.Execute(request);
        }

        private async Task SendSMSUsingSmartMessageForTurkey(SMS message)
        {
            string XMLRequest = await Task.FromResult<string>(CreateXMLRequestWithAuthorizationForSMS(message));

            var client1 = new RestClient(this.Configuration["SmartMessage:SendSMSURL"].ToString());
            var request1 = new RestRequest(Method.POST);

            request1.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request1.AddParameter("undefined", "data=" + HttpUtility.UrlEncode(XMLRequest), ParameterType.RequestBody);

            IRestResponse response1 = client1.Execute(request1);
            string xmlResponse = response1.Content;
            var rootElement = XElement.Parse(xmlResponse);
        }

        private string CreateXMLRequestWithAuthorizationForSMS(SMS message)
        {
            string JIDTag = "{{JID}}";
            string UsernameTag = "{{Username}}";
            string PasswordTag = "{{Password}}";
            string messageTag = "{{MESSAGE}}";
            string mobileNumberTag = "{{MOBILENUMBER}}";
            string SendSMSDataWithoutToken = GetXMLString();

            string username = this.Configuration["SmartMessage:Username"].ToString();
            string password = this.Configuration["SmartMessage:Password"].ToString();

            SendSMSDataWithoutToken = SendSMSDataWithoutToken.Replace(JIDTag, this.Configuration["SmartMessage:JID"].ToString());
            SendSMSDataWithoutToken = SendSMSDataWithoutToken.Replace(messageTag, message.Message);
            SendSMSDataWithoutToken = SendSMSDataWithoutToken.Replace(mobileNumberTag, message.Receiver);
            SendSMSDataWithoutToken = SendSMSDataWithoutToken.Replace(UsernameTag, username);
            SendSMSDataWithoutToken = SendSMSDataWithoutToken.Replace(PasswordTag, password);

            return  SendSMSDataWithoutToken;
        }
        private string GetSMSURL()
        {
            return this.Configuration["JazzMessage:SendSMSURL"].ToString();
        }

        private static string GetXMLString()
        {
            return                          "<SENDSMS>" +
                                               "<USR>{{Username}}</USR>" +
                                                "<PWD>{{Password}}</PWD>" +
                                                  "<JID>{{JID}}</JID>" +
                                               "<MSG>{{MESSAGE}}</MSG>" +
                                               "<CG>Standart</CG>" +
                                               "<VP>1</VP>" +
                                               "<RCPT_LIST>" +
                                                   "<RCPT>" +
                                                       "<TA>{{MOBILENUMBER}}</TA>" +
                                                       "<EID>external id</EID>" +
                                                       "<VAR>variables</VAR>" +
                                                   "</RCPT>" +
                                               "</RCPT_LIST>" +
                                               "<INT_PROVIDER/>" +
                                             "</SENDSMS>";
        }

        //public async Task<bool> Send(SMS message)
        //{
        //    //TODO: SMS send logic will be implemented after getting sms service details.
        //    try
        //    {
        //        this.Logger?.LogInformation($"{this.GetType().FullName} is about to send SMS");

        //        if(base.Configuration["SMS:SMSAlert"].ToString() == "1")
        //        {
        //            CreateConnection();
        //            var textMessage = MessageResource.Create(
        //            body: message.Message,
        //            from: new Twilio.Types.PhoneNumber(base.Configuration["SMS:MessageFrom"]),
        //            to: new Twilio.Types.PhoneNumber(message.Receiver)
        //            );
        //        }
        //        this.Logger?.LogInformation($"{this.GetType().FullName} SMS Sent successfully on:" + message.Receiver);
        //    }
        //    catch (Exception ex)
        //    {
        //        var messageReciever = message.Receiver?? " (EMPTY) ";
        //        var messageBody = message.Message ?? " (EMPTY) ";
        //        this.Logger.LogError("Failed to send SMS in MNBEC.ServiceProvider.SMSServiceProvider.Send MessageReciever:" + messageReciever + ";MessageBody: " + messageBody + ";", ex);
        //    }

        //    return true ;
        //}

        public void Sendsms(SMS message)
        {
            if (this.Configuration["SMSEnable"].ToString() == "1")
            {
                Task.Factory.StartNew(() => Send(message));
            }
        }
        private void CreateConnection()
        {
            TwilioClient.Init(base.Configuration["SMS:accountSid"], base.Configuration["SMS:authToken"]);
        }

        #endregion
    }
}
