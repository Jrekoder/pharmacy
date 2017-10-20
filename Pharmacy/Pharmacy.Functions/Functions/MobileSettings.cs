using CognitiveLocator.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pharmacy.Functions
{
    public static class MobileSettings
    {
        [FunctionName(nameof(MobileSettings))]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "MobileSettings/")]HttpRequestMessage req, TraceWriter log)
        {
            Domain.MobileSettingsRequest request = await req.Content.ReadAsAsync<Domain.MobileSettingsRequest>();

            var decrypted_token = SecurityHelper.Decrypt(request.Token, Settings.CryptographyKey);

            byte[] data = Convert.FromBase64String(decrypted_token);
            DateTime when = DateTime.FromBinary(BitConverter.ToInt64(data, 0));
            if (when < DateTime.UtcNow.AddMinutes(-5))
            {
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            result.Add(nameof(Settings.MobileCenterID_Android), Settings.MobileCenterID_Android);
            result.Add(nameof(Settings.NotificationAccessSignature), Settings.NotificationAccessSignature);
            result.Add(nameof(Settings.NotificationHubName), Settings.NotificationHubName);
            result.Add(nameof(Settings.WebChatUrl), Settings.WebChatUrl); 
            result.Add(nameof(Settings.FirebaseAppSenderId), Settings.FirebaseAppSenderId); 
            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}