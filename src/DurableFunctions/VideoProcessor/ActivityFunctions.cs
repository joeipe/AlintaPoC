using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VideoProcessor.Models;

namespace VideoProcessor
{

    public static class ActivityFunctions
    {
        [FunctionName(nameof(GetTranscodeBitrates))]
        public static int[] GetTranscodeBitrates([ActivityTrigger] object input)
        {
            return Environment.GetEnvironmentVariable("TranscodeBitRates")
                .Split(',')
                .Select(int.Parse)
                .ToArray();
        }

        [FunctionName(nameof(TranscodedVideo))]
        public static async Task<VideoFileInfo> TranscodedVideo([ActivityTrigger] VideoFileInfo inputVideo, ILogger log)
        {
            log.LogInformation($"Transcoding {inputVideo.Location} to {inputVideo.BitRate}.");

            //Simulating doing the activity
            await Task.Delay(5000);
            var transcodedLocation = $"{Path.GetFileNameWithoutExtension(inputVideo.Location)}-{inputVideo.BitRate}kbps.mp4";
            return new VideoFileInfo
            {
                Location = transcodedLocation,
                BitRate = inputVideo.BitRate
            };
        }

        [FunctionName(nameof(ExtractThumbnail))]
        public static async Task<string> ExtractThumbnail([ActivityTrigger] string inputVideo, ILogger log)
        {
            log.LogInformation($"Extracting Thumbnail {inputVideo}.");

            //Simulating doing the activity
            await Task.Delay(5000);
            return $"{Path.GetFileNameWithoutExtension(inputVideo)}-thumbnail.png";
        }

        [FunctionName(nameof(PrependIntro))]
        public static async Task<string> PrependIntro([ActivityTrigger] string inputVideo, ILogger log)
        {
            var introLocation = Environment.GetEnvironmentVariable("IntroLocation");

            log.LogInformation($"Prepending Intro {introLocation} to {inputVideo}.");

            //Simulating doing the activity
            await Task.Delay(5000);
            return $"{Path.GetFileNameWithoutExtension(inputVideo)}-withintro.mp4";
        }

        [FunctionName(nameof(SendApprovalRequestEmail))]
        public static async Task SendApprovalRequestEmail([ActivityTrigger] string inputVideo, ILogger log)
        {
            log.LogInformation($"Request approval for {inputVideo}.");

            //Simulating doing the activity
            await Task.Delay(5000);
        }

        [FunctionName(nameof(PublishVideo))]
        public static async Task PublishVideo([ActivityTrigger] string inputVideo, ILogger log)
        {
            log.LogInformation($"Publishing {inputVideo}.");

            //Simulating doing the activity
            await Task.Delay(5000);
        }

        [FunctionName(nameof(RejectVideo))]
        public static async Task RejectVideo([ActivityTrigger] string inputVideo, ILogger log)
        {
            log.LogInformation($"Rejecting {inputVideo}.");

            //Simulating doing the activity
            await Task.Delay(5000);
        }

        [FunctionName(nameof(Cleanup))]
        public static async Task<string> Cleanup([ActivityTrigger] string[] filesToCleanUp, ILogger log)
        {
            foreach (var file in filesToCleanUp.Where(f => f != null))
            {
                log.LogInformation($"Deleting {file}");

                //Simulating doing the activity
                await Task.Delay(5000);
            }
            return $"Cleaned up successfully";
        }
    }
}