using Aspose.OCR.Cloud;
using Aspose.OCR.Cloud.SDK.Api;
using Aspose.OCR.Cloud.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspose.OCR.Cloud.CommandLineTool.API
{
    public static class RecognizeImage
    {
        public static void Run(string srcFilePath, 
            string dstFilePath, 
            Language language = Language.English, 
            bool makeBinarization = true,
            bool makeSkewCorrect = false,
            bool makeUpsampling = false,
            DsrMode dsrMode = DsrMode.DsrPlusDetector,
            ResultType resultType = ResultType.Text)
        {
            try
            {

                Config config = Config.GetConfig();

                RecognizeImageApi api = new RecognizeImageApi(config.ClientId, config.ClientSecret);

                OCRRecognizeImageBody requestBody = new OCRRecognizeImageBody(
                        image: File.ReadAllBytes(srcFilePath),
                        settings: new OCRSettingsRecognizeImage(
                            language: language,
                            makeBinarization: makeBinarization,
                            makeSkewCorrect: makeSkewCorrect,
                            makeUpsampling: makeUpsampling,
                            dsrMode: dsrMode,
                            resultType: resultType
                            ));

                Console.Write($"Sending file {srcFilePath} to API...");
                string taskId = api.PostRecognizeImage(requestBody);
                Console.WriteLine("done.");

                Console.Write($"Waiting for completion of task ID {taskId}...");
                Utilities.WaitTaskCompletion(taskId);
                Console.WriteLine("done.");

                Console.Write($"Saving result to {dstFilePath}...");
                OCRResponse response = api.GetRecognizeImage(taskId);

                if (response == null) throw new Exception("API response is empty");
                if (response.ResponseStatusCode != ResponseStatusCode.Ok)
                    throw new Exception($"Response status code is {response.ResponseStatusCode.Value}");
                if (response.Error != null) throw new Exception($"API response contains error:{response.Error.Messages.First()}");
                if (response.TaskStatus != OCRTaskStatus.Completed) throw new Exception($"OCR task returned status {response.TaskStatus.Value}");
                if (response.Results == null) throw new Exception($"Results is null");


                string? directoryPath = Path.GetDirectoryName(dstFilePath);
                if (!string.IsNullOrEmpty(directoryPath) & !Directory.Exists(directoryPath)) Directory.CreateDirectory(directoryPath);

                    var result = response.Results.First();
                switch (result.Type)
                {
                    case "Text":
                        File.WriteAllText($"{dstFilePath}{(dstFilePath.ToLower().EndsWith(".txt")? "" : ".txt")}",
                            Encoding.UTF8.GetString(result.Data));
                        break;
                    case "Pdf":
                        File.WriteAllBytes($"{dstFilePath}{(dstFilePath.ToLower().EndsWith(".pdf") ? "" : ".pdf")}", 
                            result.Data);
                        break;
                    case "Hocr":
                        File.WriteAllBytes(
                            $"{dstFilePath}{(dstFilePath.ToLower().EndsWith(".html") ? "" : ".html")}", result.Data);
                        break;
                }


                Console.WriteLine("done.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while processing image recognition: {ex.Message}");
            }
        }

    }
}
