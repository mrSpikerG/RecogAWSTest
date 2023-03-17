using Microsoft.AspNetCore.Mvc;
using RecogAWSTest.Helpers;
using rekognition;
using System.Xml.Linq;

namespace RecogAWSTest.Controllers {

    [ApiController]
    public class AmazonController : ControllerBase {

        [HttpPost]
        [Route("/api/getTextByPic")]
        public IActionResult Get(IFormFile file) {

            string uploads = Environment.CurrentDirectory;

            if (file.Length > 0) {
                string filePath = Path.Combine(uploads, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
                    file.CopyToAsync(fileStream);
                }
            }



            AWS.sendMyFileToS3(file.FileName, "spikrecognition", "", file.FileName);

            return Ok(DetectText.Example(file.FileName));
        }

        [HttpGet]
        [Route("/api/getTextByName")]
        public IActionResult Get(string name) {
            return Ok(DetectText.Example(name));
        }
    }
}
