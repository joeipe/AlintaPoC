using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Logging;
using VideoProcessor.Models;

namespace VideoProcessor
{
    public static class OrchestratorFunctions
    {
        [FunctionName(nameof(ProcessVideoOrchestrator))]
        public static async Task<object> ProcessVideoOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context, ILogger log)
        {
            log = context.CreateReplaySafeLogger(log);
            var videoLocation = context.GetInput<string>();

            string transcodedLocation = null, thumbnailLocation = null, withIntroLocation = null, approvalResult = "Unknown";

            try
            {
                log.LogInformation("about to call transcode video activity");
                var transcodeResults = await context.CallSubOrchestratorAsync<VideoFileInfo[]>(nameof(TranscodeVideoOrchestrator), videoLocation);
                transcodedLocation = transcodeResults
                    .OrderByDescending(r => r.BitRate)
                    .Select(r => r.Location)
                    .First();

                //log.LogInformation("about to call transcode video activity");
                //transcodedLocation = await context.CallActivityAsync<string>("TranscodedVideo", videoLocation);

                log.LogInformation("about to call extract thumbnail activity");
                thumbnailLocation = await context.CallActivityAsync<string>("ExtractThumbnail", transcodedLocation);

                log.LogInformation("about to call prepend intro activity");
                withIntroLocation = await context.CallActivityAsync<string>("PrependIntro", transcodedLocation);

                await context.CallActivityAsync<string>("SendApprovalRequestEmail", withIntroLocation);
                approvalResult = await context.WaitForExternalEvent<string>("ApprovalResult");
                if (approvalResult == "Approved")
                {
                    await context.CallActivityAsync<string>("PublishVideo", withIntroLocation);
                }
                else
                {
                    await context.CallActivityAsync<string>("RejectVideo", withIntroLocation);
                }
            }
            catch (System.Exception ex)
            {
                log.LogError(ex, $"Exception occured: {ex.Message}");
                await context.CallActivityAsync<string>("Cleanup", new[] { transcodedLocation, thumbnailLocation, withIntroLocation });

                return new
                {
                    Error = "Failed to process video",
                    Message = ex.Message
                };
            }

            return new
            {
                Transcoded = transcodedLocation,
                Thumbnail = thumbnailLocation,
                WithIntro = withIntroLocation,
                ApprovalResult = approvalResult
            };
        }

        [FunctionName(nameof(TranscodeVideoOrchestrator))]
        public static async Task<VideoFileInfo[]> TranscodeVideoOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var videoLocation = context.GetInput<string>();
            //var bitRates = new[] { 1000, 2000, 3000, 4000 };
            var bitRates = await context.CallActivityAsync<int[]>("GetTranscodeBitrates", null);
            var transcodeTask = new List<Task<VideoFileInfo>>();
            foreach (var bitRate in bitRates)
            {
                var info = new VideoFileInfo() { Location = videoLocation, BitRate = bitRate };
                var task = context.CallActivityAsync<VideoFileInfo>("TranscodedVideo", info);
                transcodeTask.Add(task);
            }
            var transcodeResults = await Task.WhenAll(transcodeTask);
            return transcodeResults;
        }
    }
}