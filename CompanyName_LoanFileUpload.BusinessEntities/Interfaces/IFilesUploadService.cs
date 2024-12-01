using CompanyName_LoanFileUpload.BusinessEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName_LoanFileUpload.BusinessEntities.Interfaces
{
    public interface IFilesUploadService
    {
        Task<FileUploadResponse> AddFileUpload(FileUploadDTO fileUploadDTO);
        Task<List<FileUploadDTO>> GetFileUploadList();
        Task<FileUploadDTO> GetFileUploadDetailsById(int Id);
    }
}
