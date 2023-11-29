using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

namespace WisdomWave.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FileController : ControllerBase
    {
        private readonly string storageConnectionString = "https://wisdomwaveblob.blob.core.windows.net/main";
        private readonly string containerName = "main";

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // Підключення до Azure Blob Storage
            var blobServiceClient = new BlobServiceClient(storageConnectionString);
            var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            // Генеруємо унік. назв файла
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            // Лінк на Blob
            var blobClient = blobContainerClient.GetBlobClient(fileName);

            // Загрузка в Blob Storage
            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, true);
            }

            // Возврат URL для доступу до файлу
            return Ok(blobClient.Uri.ToString());
        }
    }
}