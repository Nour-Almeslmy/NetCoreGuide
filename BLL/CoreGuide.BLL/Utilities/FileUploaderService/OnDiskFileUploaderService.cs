using CoreGuide.BLL.Business.Utilities.ContextAccessor;
using CoreGuide.BLL.Business.Utilities.GuideFillerService;
using CoreGuide.BLL.Models.Enums;
using CoreGuide.BLL.Models.FileUploadResult;
using CoreGuide.Common.Entities.Output;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.FileUploaderService
{
    internal class OnDiskFileUploaderService : IFileUploaderService
    {

        private readonly IWebHostEnvironment _env;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IContextAccessor _contextAccessor;


        public OnDiskFileUploaderService(
            IWebHostEnvironment env,
            IHttpClientFactory httpClientFactory,
            IContextAccessor contextAccessor
            )
        {
            _env = env;
            _httpClientFactory = httpClientFactory;
            _contextAccessor = contextAccessor;
        }

        public FileUploadResult DeleteFile(string fileRoute, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(fileRoute) || string.IsNullOrWhiteSpace(directoryName))
            {
                //_logger.Warning("File route and directory name are not specified");
                return new FileUploadResult(false, BusinessStrings.Resources.ErrorMessagesKeys.FileNameNotSpecified);
            }

            var fileName = Path.GetFileName(fileRoute);
            string fileDirectory = Path.Combine(_env.WebRootPath, directoryName, fileName);
            if (!File.Exists(fileDirectory))
            {
                //_logger.Warning("File is not found");
                return new FileUploadResult(false, BusinessStrings.Resources.ErrorMessagesKeys.FileNotFound);

            }
            File.Delete(fileDirectory);
            return new FileUploadResult(true);

        }

        public Task<FileUploadResult> EditFile(IFormFile file, string oldFileRoute, string directoryName)
        {
            if (!string.IsNullOrWhiteSpace(oldFileRoute))
            {
                DeleteFile(oldFileRoute, directoryName);
            }
            return SaveFile(file, directoryName);
        }

        public async Task<FileUploadResult> SaveFile(IFormFile file, string directoryName)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
            {
                //_logger.Warning("Directory name is not specified");
                return new FileUploadResult(false, BusinessStrings.Resources.ErrorMessagesKeys.DirectoryNameNotSpecified);
            }

            if (file == null || file.Length == 0)
            {
                //_logger.Warning("File is not specified");
                return new FileUploadResult(false, BusinessStrings.Resources.ErrorMessagesKeys.FileNotFound);
            }

            string savingPath = await SaveFileIntoDirectory(file, directoryName);
            return new FileUploadResult(true, filePath: savingPath);
        }

        public async Task<FileUploadResult> DownloadFileFromURL(string url, string directoryName, string extension)
        {
            if (string.IsNullOrWhiteSpace(directoryName))
            {
                //_logger.Warning("Directory name is not specified");
                return new FileUploadResult(false, BusinessStrings.Resources.ErrorMessagesKeys.DirectoryNameNotSpecified);
            }

            if (string.IsNullOrWhiteSpace(url))
            {
                //_logger.Warning("URL is not specified");
                return new FileUploadResult(false, BusinessStrings.Resources.ErrorMessagesKeys.URLNameNotSpecified);
            }

            if (string.IsNullOrWhiteSpace(extension))
            {
                //_logger.Warning("Extension is not specified");
                return new FileUploadResult(false, BusinessStrings.Resources.ErrorMessagesKeys.FileExtensionInvalid);
            }

            var fileName = GetNewFileName(extension);
            string savingPath = GetSavingPath(directoryName, fileName);
            var httpClient = _httpClientFactory.CreateClient();
            var result = await httpClient.GetAsync(url);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(savingPath, content);
                return new FileUploadResult(true, filePath: GetPublicPath(directoryName,fileName));
            }
            //_logger.Warning($"Can't get the file from url: {url}");
            return new FileUploadResult(false, string.Format(BusinessStrings.Resources.ErrorMessagesKeys.FileDownloadFailed, url));
        }

        private async Task<string> SaveFileIntoDirectory(IFormFile file, string directoryName)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = GetNewFileName(extension);
            string savingPath = GetSavingPath(directoryName, fileName);
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                await File.WriteAllBytesAsync(savingPath, content);
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

        private string GetPublicPath(string directoryName,string fileName)
        {
            var currentURL =  $"{_contextAccessor.Scheme}://{_contextAccessor.Host}";
            return Path.Combine(currentURL, directoryName, fileName).Replace("\\", "/");
        }
    }
}
