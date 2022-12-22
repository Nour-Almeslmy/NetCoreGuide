using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using System;

namespace CoreGuide.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FilesController(
            IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor
            )
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileInput input)
        {
            var url = await SaveFileIntoDirectory(input.File, "Test");
            return Created(url, null);
        }
        public class FileInput
        {
            public IFormFile File { get; set; }
        }
        #region Upload file
        private async Task<string> SaveFileIntoDirectory(IFormFile file, string directoryName)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = GetNewFileName(extension);
            string savingPath = GetSavingPath(directoryName, fileName);
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                await System.IO.File.WriteAllBytesAsync(savingPath, content);
            }

            return GetPublicPath(directoryName, fileName);
        }

        private string GetNewFileName(string extension)
        {
            return $"{Guid.NewGuid()}{extension}";
        }

        private string GetSavingPath(string directoryName, string fileName)
        {
            string folder = Path.Combine(_env.WebRootPath, directoryName);
            //_logger.Warning($"file path: {folder}");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            string savingPath = Path.Combine(folder, fileName);
            return savingPath;
        }

        private string GetPublicPath(string directoryName, string fileName)
        {
            var currentURL = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}";
            return Path.Combine(currentURL, directoryName, fileName).Replace("\\", "/");
        }
        #endregion
    }
}
