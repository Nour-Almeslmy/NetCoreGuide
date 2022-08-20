using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreGuide.BLL.Models.FileUploadResult
{
    public class FileUploadResult
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }
        public string FilePath { get; set; }
        public FileUploadResult(bool isSuccess, string errorMessage = null,string filePath = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            FilePath = filePath;
        }

    }
}
