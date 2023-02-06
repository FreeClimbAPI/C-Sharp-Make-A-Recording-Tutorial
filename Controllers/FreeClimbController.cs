using Microsoft.AspNetCore.Mvc;
using freeclimb.Api;
using freeclimb.Model;
using freeclimb.Enums;
using System;
using System.Collections.Generic;
using freeclimb.Client;

namespace MakeARecording.Controllers
{
    [Route("voice")]
    [ApiController]
    public class FreeClimbController : ControllerBase
    {
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
        public string AppUrl { get { return System.Environment.GetEnvironmentVariable("HOST"); } }

        // POST /voice/
        [HttpPost]
        public ActionResult Post(CallResult request)
        {
            PerclScript script = new PerclScript(new List<PerclCommand>());
            // Verify call is in the InProgress state
            if (request.Status == CallStatus.IN_PROGRESS)
            {
                // Create PerCL say script with US English as the language
                Say say = new Say("Hello. Please leave a message after the beep, then press one or hangup");
                // say.setLanguage(ELanguage.EnglishUS);
                // // Set prompt to record message
                // say.setText();
                // Add PerCL say script to PerCL container
                script.Commands.Add(say);
                // Create PerCL record utterance script
                string messageDoneUrl = AppUrl + "/voice/MakeRecordMessageDone";
                RecordUtterance recordUtterance = new RecordUtterance(messageDoneUrl, 0, "1", 2, true, false, false);
                // Set indication that audible 'beep' should be used to signal start of recording
                // recordUtterance.setPlayBeep(EBool.True);
                // // Set indication that end of recording is touch tone key 0ne
                // recordUtterance.setFinishOnKey(EFinishOnKey.One);

                // Add PerCL record utterance script to PerCL container
                script.Commands.Add(recordUtterance);
            }
            // Convert PerCL container to JSON and append to response
            return Content(script.ToJson(), "application/json");
        }

        // PUT api/values/5
        // RPC call To start a call
        [HttpPut]
        public void Put([FromBody] string value)
        {
            // // Set up App Credentails
            // string accountId = System.Environment.GetEnvironmentVariable("ACCOUNT_ID");
            // string apiKey = System.Environment.GetEnvironmentVariable("API_KEY");

            // // Set up Call Details
            // string applicationId = System.Environment.GetEnvironmentVariable("APPLICATION_ID");
            // string phoneNumber = "+" + value;
            // string freeClimbPhoneNumber = "+Your FreeClimb Number";

            // try
            // {
            //     // Create the PersyClient
            //     FreeClimbClient client = new FreeClimbClient(accountId, apiKey);
            //     // Create a Call
            //     Call call = client.getCallsRequester.create(phoneNumber, // To
            //                                                 freeClimbPhoneNumber, // From,
            //                                                 applicationId); // Application to Handle the call
            // }
            // catch (FreeClimbException ex)
            // {
            //     System.Console.Write(ex.Message);
            // }

            Configuration config = new Configuration();
            config.BasePath = "https://www.freeclimb.com/apiserver";
            // Configure HTTP basic authorization: fc
            config.Username = getAcctId();
            config.Password = getApiKey();

            var apiInstance = new DefaultApi(config);
            string to = getToNumber();
            string from = getFromNumber();
            string appId = getAppID();
            string callConnectUrl = getCallConnectURL();
            MakeCallRequest makeCallRequest = new MakeCallRequest(from, to, appId);
        }


        [HttpPost("MakeRecordMessageDone")]
        public ActionResult MakeRecordMessageDone(RecordingResult recordingUtteranceStatusCallback)
        {
            // Create an empty PerCL script container
            PerclScript script = new PerclScript(new List<PerclCommand>());

            if (Request != null)
            {
                // Check if recording was successful by checking if a recording identifier was provided
                if (recordingUtteranceStatusCallback.RecordingId != null)
                {
                    // Recording was successful as recording identifier present in response

                    // Create PerCL say script with US English as the language
                    Say say = new Say("Thanks. The message has been recorded.");
                    // say.setLanguage(ELanguage.EnglishUS);
                    // // Set prompt to indicate message has been recorded
                    // say.setText("Thanks. The message has been recorded.");

                    // Add PerCL say script to PerCL container
                    script.Commands.Add(say);
                }
                else
                {
                    // Recording was failed as there is no recording identifier present in response

                    // Create PerCL say script with US English as the language
                    Say say = new Say("Sorry we weren't able to record the message.");
                    // say.setLanguage(ELanguage.EnglishUS);
                    // // Set prompt to indicate message recording failed
                    // say.setText("Sorry we weren't able to record the message.");

                    // Add PerCL say script to PerCL container
                    script.Commands.Add(say);
                }

                // Create PerCL pause script with a duration of 100 milliseconds
                Pause pause = new Pause(100);

                // Add PerCL pause script to PerCL container
                script.Commands.Add(pause);

                // Create PerCL say script with US English as the language
                Say sayGoodbye = new Say("Goodbye");
                // sayGoodbye.setLanguage(ELanguage.EnglishUS);
                // Set prompt sayGoodbye.setText("Goodbye");

                // Add PerCL say script to PerCL container
                script.Commands.Add(sayGoodbye);

                // Create PerCL hangup script
                Hangup hangup = new Hangup();

                // Add PerCL hangup script to PerCL container
                script.Commands.Add(new Hangup());
            }

            // Convert PerCL container to JSON and append to response
            return Content(script.ToJson(), "application/json");
        }
    }
}
