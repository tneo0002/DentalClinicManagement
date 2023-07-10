using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace DentalClinicManagementSystem.Utils
{
    public class SmsSender
    {
        private const String accountSid = "accountSid";
        private const String authToken = "authToken";
        private const String twilioPhone = "twilioPhone";

        public void Send(String toPhone, String contents)
        {
            TwilioClient.Init(accountSid, authToken);
            var to = new PhoneNumber(toPhone);
            var from = new PhoneNumber(twilioPhone);

            var message = MessageResource.Create(
                to: to,
                from: twilioPhone,
                body: contents);
        }
    }
}