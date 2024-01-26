using Aspose.OCR.Cloud.SDK.Api;
using Aspose.OCR.Cloud.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose.OCR.Cloud.CommandLineTool.API
{
    public static class Utilities
    {

        /// <summary>
        /// Uses Utilities API to get task status until it become completed or failed
        /// </summary>
        public static void WaitTaskCompletion(string taskid)
        {
            Config config = Config.GetConfig();
            int getTaskStatusRetriesCount = 0;
            OCRResponse getTaskStatusResponse = new OCRResponse();
            UtilitiesApi api = new UtilitiesApi(config.ClientId, config.ClientSecret);
            while (getTaskStatusRetriesCount <= 10
                & getTaskStatusResponse.TaskStatus != OCRTaskStatus.Completed
                & getTaskStatusResponse.TaskStatus != OCRTaskStatus.Error)
            {
                getTaskStatusRetriesCount += 1;
                // Console.WriteLine($"Getting task status attempt {getTaskStatusRetriesCount}");
                Thread.Sleep((int)(5000 + getTaskStatusRetriesCount * 1000));
                getTaskStatusResponse = api.GetTaskStatus(taskid);
                switch (getTaskStatusResponse.TaskStatus)
                {
                    case OCRTaskStatus.Pending:
                        break;
                    case OCRTaskStatus.Error:
                        throw new Exception("Task completed with error. Please, try again later.");
                }
            };
        }
    }
}
