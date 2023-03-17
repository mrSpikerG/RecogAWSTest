using System;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;

namespace rekognition
{
    public class DetectText
    {
        public static string Example(string pic)
        {
            string accessKey = "AKIA6HUTAZ765CVFD4FA";
            string secretKey = "gTnjUxMXFp3zdgu8/o2H2gvgfR6ZZHGKLi2u6akj";
            string fileInfo = "";

            //AmazonS3Config config = new AmazonS3Config();
            //config.ServiceURL = "";

            String photo = pic;
            String bucket = "spikrecognition";

            AmazonRekognitionClient rekognitionClient = new AmazonRekognitionClient(
            accessKey,
                    secretKey,
                    Amazon.RegionEndpoint.EUWest2
                    );
            DetectTextRequest detectTextRequest = new DetectTextRequest()
            {
                Image = new Image()
                {
                    S3Object = new S3Object()
                    {
                        Name = photo,
                        Bucket = bucket
                    }
                }
            };

            try
            {
                DetectTextResponse detectTextResponse = rekognitionClient.DetectTextAsync(detectTextRequest).GetAwaiter().GetResult();
                Console.WriteLine("Detected lines and words for " + photo);
                foreach (TextDetection text in detectTextResponse.TextDetections)
                {
                    if (text.DetectedText.Contains(';')) {
                        text.DetectedText += "\n";
                    }
                    fileInfo += text.DetectedText + " ";
                   // fileinfo += text.DetectedText;
                   // Console.WriteLine(text.DetectedText);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            //Console.WriteLine(fileInfo);
            return fileInfo;
        }
    }
}

