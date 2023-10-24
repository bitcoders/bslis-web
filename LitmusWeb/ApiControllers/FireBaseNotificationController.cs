using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models.CustomModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace LitmusWeb.ApiControllers
{
    public class FireBaseNotificationController : ApiController
    {
        [HttpPost]
        [ActionName("SendFirebaseNotification")]
        [Route("api/FireBaseNotification/SendFirebaseNotification")]
        public HttpResponseMessage ComposeFirebaseMessage([FromBody] FirebaseMessageParameterModel parameter)
        {
            // using the messageType and unitCode get list of the DeviceTokens from registeredDevices Table
            List<RegisteredDevice> registeredDevices = new List<RegisteredDevice>();
            RegisteredDevice registeredDevice = new RegisteredDevice();
            List<string> DeviceTokens = new List<string>();
            List<string> UserCodes = new List<string>();
            List<FirebaseNotificationRight> fbNotificatoonRights = new List<FirebaseNotificationRight>();
            FirebaseNotificationRepository fbRepository = new FirebaseNotificationRepository();



            string firebaseResponse = "";
            fbNotificatoonRights = fbRepository.GetFirebaseNotificationRights(parameter.MessageType, parameter.UnitCode);
            if (fbNotificatoonRights == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Content");
            }

            ///preparing list of users
            foreach (var item in fbNotificatoonRights)
            {
                UserCodes.Add(item.UserCode);
            }

            /// preparing list of device tokens


            foreach (var item in UserCodes)
            {
                registeredDevice = fbRepository.GetRegisteredDevices(item);

                if (registeredDevice != null)
                {
                    DeviceTokens.Add(registeredDevice.DeviceToken);
                }
            }




            if (DeviceTokens.Count <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Content");
            }
            // using the DeviceTokens send firebase cloud message to devices

            foreach (var token in DeviceTokens)
            {
                firebaseResponse = SendFirbaseCloudMessage(token, parameter.Title, parameter.Body);
            }

            var response = new { firebaseResponse };

            return Request.CreateResponse(HttpStatusCode.OK, firebaseResponse);
        }


        [HttpPost]
        [ActionName(name: "SendFirebaseNotificationToAll")]
        public HttpResponseMessage SendFirebaseNotificationToAll([FromBody] FirebaseMessageParameterModel parameter)
        {
            List<RegisteredDevice> registeredDevice = new List<RegisteredDevice>();
            List<string> DeviceTokens = new List<string>();
            List<string> UserCodes = new List<string>();
            List<FirebaseNotificationRight> fbNotificatoonRights = new List<FirebaseNotificationRight>();
            FirebaseNotificationRepository fbRepository = new FirebaseNotificationRepository();

            string firebaseResponse = "";
            registeredDevice = fbRepository.GetRegisteredDevicesList();

            if (registeredDevice == null)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Content");
            }
            foreach (var item in registeredDevice)
            {
                DeviceTokens.Add(item.DeviceToken);
            }

            if (DeviceTokens.Count <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Content");
            }

            foreach (var token in DeviceTokens)
            {
                firebaseResponse = SendFirbaseCloudMessage(token, parameter.Title, parameter.Body);
            }

            var response = new { firebaseResponse };

            return Request.CreateResponse(HttpStatusCode.OK, firebaseResponse);
        }

        public string SendFirbaseCloudMessage(string deviceToken, string messageTitle, string messageBody)
        {
            //Create the web request with fire base API  
            String sResponseFromServer = "{'statusCode' : 500, 'responseMessage' : 'No Content'  }";
            WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            string serverKey = "AIzaSyCZaT1w2zL6tqQzzu3fxv0m62psTzNShHU";
            string senderId = "569894012488";
            //string deviceId = "cWdUFCNCZfk:APA91bHWatdXGvKavvssnDlxfX0qny6-52AIT9uYifkqgvyuDwa-ljeKkubOXEBRM_q_yHOmhoms6y1ZiV1sCXCJEZFj8bFI1Lhcc1zG8NlZlo8upBFq2Tq-KN_JCtvjhTtyTNOG16x_";
            //string txtmsg = "Aise Kaise ho jiyaagaaa .... Hourly Notifications aaenge yaha se, Stoppage and bagging ke";
            //string txttitle = "Bslis";


            string deviceId = deviceToken;
            string txtmsg = messageBody;
            string txttitle = messageTitle;

            tRequest.Headers.Add(string.Format("Authorization: key={0}", serverKey));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", senderId));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = deviceId,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = txtmsg,
                    title = txttitle.Replace(":", ""),
                    sound = "bslis-message.mp3",
                    //badge = badgeCounter
                },
            };
            var serializer = new JavaScriptSerializer();
            var json = serializer.Serialize(payload);

            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                sResponseFromServer = tReader.ReadToEnd();

                                //result.Response = sResponseFromServer;

                            }
                    }
                }
            }

            var response = new { json, sResponseFromServer };

            return serializer.Serialize(response);

            //return sResponseFromServer;
            //return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
