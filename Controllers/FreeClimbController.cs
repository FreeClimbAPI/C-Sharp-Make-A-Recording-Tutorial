using Microsoft.AspNetCore.Mvc;
using freeclimb.Api;
using freeclimb.Model;
using System;
using System.Collections.Generic;
using freeclimb.Client;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using freeclimb.Enums;
//using freeclimb.Enums;

namespace MakeARecording.Controllers
{
    [Route("/voice")]
    [ApiController]
    public class FreeClimbController : ControllerBase
    {
        public string AppUrl { get { return System.Environment.GetEnvironmentVariable("WEB_HOST"); } }

        // POST /voice/
        [HttpPost]
        public string Post([FromBody] CallResult request)
        {
            System.Console.WriteLine("Request Value: " + request);
            // Create a PerCl script
            PerclScript script = new PerclScript(new List<PerclCommand>());
            if (request.CallStatus == CallStatus.IN_PROGRESS)
            {
                // Create a Say Command
                // Say sayHello = new Say("hello, freeclimb!");
                // Console.WriteLine(sayHello.ToJson());
                // // Add the command
                // helloScript.Commands.Add(sayHello);

                // Console.WriteLine(helloScript.ToJson());

                // Respond to FreeClimb with your script
                Say say = new Say("Hello. Please leave a message after the beep, then press the pound symbol or hangup to finish recording");
                // Add PerCL say script to PerCL container
                script.Commands.Add(say);
                // Create PerCL record utterance script
                string messageDoneUrl = AppUrl + "/voice/MakeRecordMessageDone";
                System.Console.WriteLine("AppUrl: " + AppUrl);
                RecordUtterance recordUtterance = new RecordUtterance(messageDoneUrl, 60, "#", 6000000, true, true);

                // Add PerCL record utterance script to PerCL container
                script.Commands.Add(recordUtterance);
            }
            else
            {
                System.Console.WriteLine("Request Value: " + request);
            }
            return script.ToJson();
        }

        [HttpPost("MakeRecordMessageDone")]
        public ActionResult MakeRecordMessageDone([FromBody] RecordingResult request)
        {
            System.Console.WriteLine("Request Value: " + request);
            // Create an empty PerCL script container
            PerclScript script = new PerclScript(new List<PerclCommand>());

            if (request != null)
            {
                // Check if recording was successful by checking if a recording identifier was provided
                if (request.RecordingId != null)
                {
                    // Recording was successful as recording identifier present in response

                    // Create PerCL say script with US English as the language
                    Say say = new Say("Thanks. The message has been recorded.");

                    // Add PerCL say script to PerCL container
                    script.Commands.Add(say);
                }
                else
                {
                    // Recording was failed as there is no recording identifier present in response

                    // Create PerCL say script with US English as the language
                    Say say = new Say("Sorry we weren't able to record the message.");

                    // Add PerCL say script to PerCL container
                    script.Commands.Add(say);
                }

                // Create PerCL pause script with a duration of 100 milliseconds
                Pause pause = new Pause(100);

                // Add PerCL pause script to PerCL container
                script.Commands.Add(pause);

                // Create PerCL say script with US English as the language
                Say sayGoodbye = new Say("Goodbye");
                // Set prompt sayGoodbye.setText("Goodbye");

                // Add PerCL say script to PerCL container
                script.Commands.Add(sayGoodbye);

                // Create PerCL hangup script
                Hangup hangup = new Hangup();

                // Add PerCL hangup script to PerCL container
                script.Commands.Add(hangup);
            }

            // Convert PerCL container to JSON and append to response
            return Content(script.ToJson(), "application/json");
        }
    }
}
