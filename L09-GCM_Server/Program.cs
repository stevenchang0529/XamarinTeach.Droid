using PushSharp;
using PushSharp.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L09_GCM_Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var push = new PushBroker();
            var reg = "APA91bFDhRX2tdstLGVb_-u6OROIBgu_PTIaTaqdxUJRKYgoqA-ULAVtKtbmK4K9qiD_-j2i9KKI8dy9YvpdfrAYGWUj-cUUgrre5TWD81ruB9DBRQ8T5xrw2202UZeOO5aDIAH6ypv_pVyoVpaydSPeNCA8ROQJLA";
            var devices = new[] { reg };
            push.RegisterGcmService(new GcmPushChannelSettings("AIzaSyBGNhVHynqaZoUNT7D4xO9rZFmvMOQJvHk"));
            push.QueueNotification(new GcmNotification().ForDeviceRegistrationId(devices)// (Devices)
                        .WithJson("{\"alert\":\"測試推播2!\",\"badge\":7,\"sound\":\"sound.caf\"}"));
            push.StopAllServices();
            Console.WriteLine("Queue Finished, press return to exit...");
            Console.ReadLine();	
        }
    }
}
