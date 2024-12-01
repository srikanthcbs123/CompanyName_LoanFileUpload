using CompanyName_LoanFileUpload.BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName_LoanFileUpload.BusinessEntities.Interfaces
{
    public interface IFilesUploadRepository
    {
        Task<FileUploadResponse> AddFileUpload(FileUpload fileUpload);
        Task<List<FileUpload>> GetFileUploadList();
        Task<FileUpload> GetFileUploadDetailsById(int Id);
    }
}
