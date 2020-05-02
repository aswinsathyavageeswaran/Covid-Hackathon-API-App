using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace HackCovidAPICore.DataAccess
{
    public class BlobStorageService
    {
        private string primaryKey;
        private string containerName;
        CloudStorageAccount cloudStorageAccount;  
        CloudBlobClient cloudBlobClient;  
        CloudBlobContainer cloudBlobContainer;

        public BlobStorageService(string primaryKey, string containerName) 
        {
            this.primaryKey = primaryKey;
            this.containerName = containerName;

            cloudStorageAccount = CloudStorageAccount.Parse(primaryKey);  
            cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();  
            cloudBlobContainer = cloudBlobClient.GetContainerReference(containerName);
        }

        public async Task<bool> UploadAsync(IFormFile file)  
        {  
            try  
            {  
                if (file != null)  
                {  
                    if (await cloudBlobContainer.CreateIfNotExistsAsync())  
                        await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });  

                    Stream stream = file.OpenReadStream();
                    BinaryReader binaryReader = new BinaryReader(stream);
                    byte[] bytes = binaryReader.ReadBytes((Int32)stream.Length);

                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(file.Name);  
                    cloudBlockBlob.Properties.ContentType = file.ContentType;  
                    await cloudBlockBlob.UploadFromByteArrayAsync(bytes, 0, bytes.Length);  
                    return true;  
                }  
                return false;  
            }  
            catch { }
            return false;
        }
    }
}