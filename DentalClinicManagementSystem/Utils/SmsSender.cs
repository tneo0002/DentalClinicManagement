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
        private const String accountSid = "AC1eeb80b8e0dce51f0957c4638a9f595d";
        private const String authToken = "1cf3d846b21f83136fdd177c10fbafcc";
        private const String twilioPhone = "+19498065098";

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