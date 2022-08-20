using CoreGuide.BLL.Models.FileUploadResult;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Business.Utilities.FileUploaderService
{
    internal interface IFileUploaderService
    {
        FileUploadResult DeleteFile(string fileRoute, string directoryName);
        Task<FileUploadResult> DownloadFileFromURL(string url, string directoryName, string extension);
        Task<FileUploadResult> EditFile(IFormFile file, string oldFileRoute, string directoryName);
        Task<FileUploadResult> SaveFile(IFormFile file, string directoryName);
    }
}
