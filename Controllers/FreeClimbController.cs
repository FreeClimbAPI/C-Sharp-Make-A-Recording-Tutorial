using Microsoft.AspNetCore.Mvc;
using freeclimb.Api;
using freeclimb.Model;
using System;
using System.Collections.Generic;
using freeclimb.Client;
using freeclimb.Enums;

namespace MakeARecording.Controllers
{
    [Route("/voice")]
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
        public string Post(CallStatus request)
        {
            System.Console.WriteLine("Request Value: " + request);
            // Create a PerCl script
            PerclScript helloScript = new PerclScript(new List<PerclCommand>());

            // Create a Say Command
            Say sayHello = new Say("hello, freeclimb!");
            Console.WriteLine(sayHello.ToJson());
            // Add the command
            helloScript.Commands.Add(sayHello);

            Console.WriteLine(helloScript.ToJson());

            // Respond to FreeClimb with your script
            return helloScript.ToJson();

            // if (request == CallStatus.IN_PROGRESS)
            // {
            //     // Create a Say Command
            //     Say say = new Say("Hello. Please leave a message after the beep, then press one or hangup");
            //     Console.WriteLine(say.ToJson());
            //     // Add the command
            //     script.Commands.Add(say);

            //     string messageDoneUrl = "https://4f35-63-209-137-19.ngrok.io/voice/MakeRecordMessageDone";
            //     RecordUtterance recordUtterance = new RecordUtterance(messageDoneUrl, 0, "1", 2, true, false, false);
            //     script.Commands.Add(recordUtterance);

            //     Console.WriteLine(script.ToJson());

            //     // Respond to FreeClimb with your script
            // }
            // else
            // {
            //     System.Console.WriteLine("Request Value: " + request);
            // }
            // return script.ToJson();
        }
    }
    // public ActionResult Post(CallStatus request)
    // {
    //     PerclScript helloScript = new PerclScript(new List<PerclCommand>());

    //     // Create a Say Command
    //     Say sayHello = new Say("hello, freeclimb!");
    //     Console.WriteLine(sayHello.ToJson());
    //     // Add the command
    //     helloScript.Commands.Add(sayHello);

    //     Console.WriteLine(helloScript.ToJson());

    //     // Respond to FreeClimb with your script
    //     return helloScript.ToJson();
    //     // PerclScript script = new PerclScript(new List<PerclCommand>());
    //     // Verify call is in the InProgress state
    //     // if (request == CallDirection.OUTBOUND_API)
    //     // {
    //     //     // Create PerCL say script with US English as the language
    //     //     Say say = new Say("Hello. Please leave a message after the beep, then press one or hangup");
    //     //     // say.setLanguage(ELanguage.EnglishUS);
    //     //     // // Set prompt to record message
    //     //     // say.setText();
    //     //     // Add PerCL say script to PerCL container
    //     //     script.Commands.Add(say);
    //     //     System.Console.WriteLine("Say has been added");
    //     //     // Create PerCL record utterance script
    //     //     string messageDoneUrl = "https://4f35-63-209-137-19.ngrok.io/voice/MakeRecordMessageDone";
    //     //     RecordUtterance recordUtterance = new RecordUtterance(messageDoneUrl, 0, "1", 2, true, false, false);
    //     //     // Set indication that audible 'beep' should be used to signal start of recording
    //     //     // recordUtterance.setPlayBeep(EBool.True);
    //     //     // // Set indication that end of recording is touch tone key 0ne
    //     //     // recordUtterance.setFinishOnKey(EFinishOnKey.One);

    //     //     // Add PerCL record utterance script to PerCL container
    //     //     script.Commands.Add(recordUtterance);
    //     // }
    //     // else
    //     // {
    //     //     System.Console.WriteLine("Request Value: " + request);
    //     //     System.Console.WriteLine("Didn't add commands since it skipped condition");
    //     // }
    //     // Convert PerCL container to JSON and append to response
    //     return Content(script.ToJson(), "application/json");
    // }
}
