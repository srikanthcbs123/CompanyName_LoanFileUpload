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
        //Dependency Injection is used to develop loosly coupled architecture.
        //(By using interfaces and interface implanted classe we can achieve loosly coupled architecture.)
        //Depency injection used to avoid tightly coupling between the classes.
        //Dependency injection TYPES we can implement in 3 ways at controller level
        //1.constructor injection(Realtime used 99%)
        //2.property injection
        //3.method injection
        #region 1.ConstructorInjection
        //IN the  constructor Injection, inject/pass the dependecies to the constructor 
        private readonly IFilesUploadService _filesUploadService;//constructor injection.
        public FilesUploadController(IFilesUploadService filesUploadService)
        {
            this._filesUploadService = filesUploadService;
        }
        #endregion

        #region 2.PropertyInjection
        //In the property injection, the dependency is provided through a public property.
      //  public IFilesUploadService FileUploadServiceAccess { get; set; }


        #endregion

        #region 3.MethodInjection

        //public interface ICustomerDataAccess
        //{
        //    string GetCustomerName(int id);
        //}

        //public class CustomerDataAccess : ICustomerDataAccess
        //{
        //    public string GetCustomerName(int id)
        //    {
        //        //Write the logic to get the customer bame based on given id from the database in real time application

        //        return "Name of the Customer: John Smith";
        //    }
        //}

        //interface IDataAccessDependency
        //{
        //    void SetDependency(ICustomerDataAccess customerDataAccess);
        //}

        //public class CustomerBusinessLogic : IDataAccessDependency
        //{
        //    ICustomerDataAccess _customerDataAccess;

        //    public void SetDependency(ICustomerDataAccess customerDataAccess)
        //    {
        //        _customerDataAccess = customerDataAccess;
        //    }

        //    public string GetCustomerName(int id)
        //    {
        //        return _customerDataAccess.GetCustomerName(id);
        //    }
        //}


        #endregion 


        [HttpGet]
        [Route("GetAllFileUploadList")]
        public async Task<IActionResult> GetAllFileUploadList()

        {
            try
            {   //1.Constructor Injection to access the fileuploadservice Method.
                var fileUploadList = await this._filesUploadService.GetFileUploadList();

                //2.property Injection to access the fileuploadservice Method.
                //var result2=await this.FileUploadServiceAccess.GetFileUploadList();


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
