using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using freeclimb.Api;
using freeclimb.Client;
using freeclimb.Model;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MakeARecording
{

    public class Program
    {
        public static void Main(string[] args)
        {
            // Set up App Credentails
            string accountId = getAcctId();
            string apiKey = getApiKey();

            // Set up Call Details
            string applicationId = getAppID();
            string phoneNumber = getToNumber();
            string freeClimbPhoneNumber = getFromNumber();

            Configuration config = new Configuration();
            config.Username = accountId;
            config.Password = apiKey;

            DefaultApi client = new DefaultApi(config);
            // Create a Call
            MakeCallRequest request = new MakeCallRequest(freeClimbPhoneNumber, phoneNumber, applicationId);
            CallResult call = client.MakeACall(request);

            // try
            // {

            // }
            // catch (Exception ex)
            // {
            //     System.Console.Write(ex);
            // }
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        private static string getAcctId()
        {
            return System.Environment.GetEnvironmentVariable("ACCOUNT_ID");
        }

        private static string getApiKey()
        {
            return System.Environment.GetEnvironmentVariable("API_KEY");
        }
        private static string getFromNumber()
        {
            return System.Environment.GetEnvironmentVariable("FROM_NUMBER");
        }
        private static string getToNumber()
        {
            return System.Environment.GetEnvironmentVariable("TO_NUMBER");
        }
        private static string getAppID()
        {
            return System.Environment.GetEnvironmentVariable("APP_ID");
        }
        private static string getCallConnectURL()
        {
            return System.Environment.GetEnvironmentVariable("CALL_CONNECT_URL");
        }
    }
}
