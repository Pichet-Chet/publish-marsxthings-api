//using System.Text;
//using MarsXApps.Models.ThirdParty.SMSMKT.OtpSend;
//using MarsXApps.Models.ThirdParty.SMSMKT.OtpValidate;
//using Microsoft.Extensions.Configuration;
//using Newtonsoft.Json;

//namespace MarsXApps.Module.SMS.Repositories.SmsMtkRepositories
//{
//    public class SmsMtkRepositories
//    {
//        public static IConfigurationRoot config = new ConfigurationBuilder()
//                   .SetBasePath(Directory.GetCurrentDirectory())
//                   .AddJsonFile("appsettings.json")
//                   .Build();

//        private static string URL_OTP_SEND = config["SMS:URL_OTP_SEND"];

//        private static string URL_OTP_VALIDATE = config["SMS:URL_OTP_VALIDATE"];

//        private static string PROJECT_KEY = config["SMS:PROJECT_KEY"];

//        private static string API_KEY = config["SMS:API_KEY"];

//        private static string SECRET_KEY = config["SMS:SECRET_KEY"];



//        public async Task<OtpSendReturn> OTPSend(string phone)
//        {
//            OtpSendRequest smsMktRequest = new OtpSendRequest();

//            OtpSendReturn smsMktReturn = new OtpSendReturn();

//            try
//            {
//                var client = new HttpClient();

//                var request = new HttpRequestMessage(HttpMethod.Post, URL_OTP_SEND);

//                request.Headers.Add("api_key", API_KEY);

//                request.Headers.Add("secret_key", SECRET_KEY);

//                smsMktRequest.project_key = PROJECT_KEY;

//                smsMktRequest.phone = phone;

//                smsMktRequest.ref_code = Helper.GenerateShortGuid();

//                var jsonTxt = JsonConvert.SerializeObject(smsMktRequest);

//                request.Content = new StringContent(jsonTxt, Encoding.UTF8, "application/json");

//                using (HttpResponseMessage response = await client.SendAsync(request))
//                {
//                    using (HttpContent content = response.Content)
//                    {
//                        var json = content.ReadAsStringAsync().Result;

//                        smsMktReturn = JsonConvert.DeserializeObject<OtpSendReturn>(json);

//                        smsMktReturn.result.phone = phone;
//                    }
//                }
//            }
//            catch
//            {
//                smsMktReturn.code = "000";
//                smsMktReturn.detail = "Error";
//            }

//            return smsMktReturn;
//        }

//        public OtpValidateReturn OTPValidate(OtpValidateRequest param)
//        {
//            OtpValidateRequest smsMktRequest = new OtpValidateRequest();

//            OtpValidateReturn smsMktReturn = new OtpValidateReturn();

//            try
//            {
//                var client = new HttpClient();

//                var request = new HttpRequestMessage(HttpMethod.Post, URL_OTP_VALIDATE);

//                request.Headers.Add("api_key", API_KEY);

//                request.Headers.Add("secret_key", SECRET_KEY);

//                smsMktRequest.token = param.token;

//                smsMktRequest.otp_code = param.otp_code;

//                smsMktRequest.ref_code = param.ref_code;

//                var jsonTxt = JsonConvert.SerializeObject(smsMktRequest);

//                request.Content = new StringContent(jsonTxt, Encoding.UTF8, "application/json");

//                using (HttpResponseMessage response = client.SendAsync(request).Result)
//                {
//                    using (HttpContent content = response.Content)
//                    {
//                        var json = content.ReadAsStringAsync().Result;

//                        smsMktReturn = JsonConvert.DeserializeObject<OtpValidateReturn>(json);

//                    }
//                }
//            }
//            catch
//            {
//                smsMktReturn.code = "000";
//                smsMktReturn.detail = "Error";
//                smsMktReturn.result.status = false;
//            }

//            return smsMktReturn;

//        }


//    }
//}

