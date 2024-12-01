using CompanyName_LoanFileUpload.BusinessEntities.DTOs;
using CompanyName_LoanFileUpload.BusinessEntities.Interfaces;
using CompanyName_LoanFileUpload.BusinessEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CompanyName_LoanFileUpload.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesUploadController : ControllerBase
    {
        private readonly IFilesUploadService _filesUploadService;
        public FilesUploadController(IFilesUploadService filesUploadService)
        {
            this._filesUploadService = filesUploadService;
        }

        [HttpGet]
        [Route("GetAllFileUploadList")]
        public async Task<IActionResult> GetAllFileUploadList()

        {
            try
            {
                var fileUploadList = await this._filesUploadService.GetFileUploadList();
                if (fileUploadList != null)
                {
                    return StatusCode(StatusCodes.Status200OK, fileUploadList);
                    // return Ok(new { response = fileUploadList });
                }
                else
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Bad input request");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System or Server Error");
            }
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file, [FromForm] string ImageCaption, CancellationToken cancellationtoken)
        {
            var obj = ImageCaption;
            var result = await WriteFile(file);
            return Ok(new { response = result });
        }

        [HttpGet]
        [Route("DownloadFile")]
        public async Task<IActionResult> DownloadFile([FromQuery] int Id)
        {
            var data = await this._filesUploadService.GetFileUploadDetailsById(Id);
            var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", data.ModifiedFilename);
            // var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

            var provider = new FileExtensionContentTypeProvider();
            if (!provider.TryGetContentType(filepath, out var contenttype))
            {
                contenttype = "application/octet-stream";
            }
            var bytes = await System.IO.File.ReadAllBytesAsync(filepath);
            return File(bytes, contenttype, Path.GetFileName(filepath));
        }


        private async Task<FileUploadResponse> WriteFile(IFormFile file)
        {
            var dateformat = DateTime.Now.Ticks.ToString();
            FileUploadResponse fileUploadResponse = new();
            string filename = "";
            try
            {
                var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                filename = DateTime.Now.Ticks.ToString() + extension;

                var filepath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files");

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }

                var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "Upload\\Files", filename);
                using (var stream = new FileStream(exactpath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                FileUploadDTO fileUploadDTO = new();
                fileUploadDTO.FilePath = exactpath;
                fileUploadDTO.FileName = file.FileName;
                fileUploadDTO.ModifiedFilename = filename;
                fileUploadDTO.Createdby = "srikanth";//Read this one from your token in realtime.

                //Service Logic
                fileUploadResponse = await this._filesUploadService.AddFileUpload(fileUploadDTO);
            }

            catch (Exception ex)
            {
            }
            return fileUploadResponse;
        }
    }
}
