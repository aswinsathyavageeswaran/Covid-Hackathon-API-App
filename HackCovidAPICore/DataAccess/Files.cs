using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HackCovidAPICore.DataAccess
{
    public class Files : BlobStorageService, IFiles
    {
        public Files(string primaryKey, string containerName) : base(primaryKey, containerName) {}

        public async Task<bool> UploadFileToBlob(IFormFile file)
        {
            if (await UploadAsync(file))
                return true;
            return false;
        }
    }
}