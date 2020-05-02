using System.Threading.Tasks;
using HackCovidAPICore.DataAccess;
using Microsoft.AspNetCore.Mvc;
namespace HackCovidAPICore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class FilesController : ControllerBase
    {
        private IFiles files;

        public FilesController(IFiles files)
        {
            this.files = files;
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<ActionResult> Upload()
        {
            if(Request.Form.Files.Count <= 0)
                return StatusCode(500, "Please choose a file to upload");
            var file = Request.Form.Files[0];
            if (await this.files.UploadFileToBlob(file))
                return Ok("File uploaded successfully");
            return StatusCode(500, "Unable to upload the file");
        }
    }
}