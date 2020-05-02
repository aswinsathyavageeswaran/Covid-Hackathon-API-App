using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HackCovidAPICore.DataAccess
{
    public interface IFiles
    {
          Task<bool> UploadFileToBlob(IFormFile file);
    }
}