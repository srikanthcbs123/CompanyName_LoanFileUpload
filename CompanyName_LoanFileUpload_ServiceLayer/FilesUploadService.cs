using AutoMapper;
using CompanyName_LoanFileUpload.BusinessEntities.DTOs;
using CompanyName_LoanFileUpload.BusinessEntities;
using CompanyName_LoanFileUpload.BusinessEntities.Interfaces;
using CompanyName_LoanFileUpload.BusinessEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName_LoanFileUpload_ServiceLayer
{
    public class FilesUploadService:IFilesUploadService
    {
        public readonly IFilesUploadRepository _filesUploadRepository;
        private readonly IMapper _mapper;
        public FilesUploadService(IFilesUploadRepository filesUploadRepository, IMapper mapper)
        {
            this._filesUploadRepository = filesUploadRepository;
            this._mapper = mapper;
        }
        public async Task<List<FileUploadDTO>> GetFileUploadList()
        {
            var fileUploadList = await _filesUploadRepository.GetFileUploadList();
            return _mapper.Map<List<FileUploadDTO>>(fileUploadList);
        }
        public async Task<FileUploadResponse> AddFileUpload(FileUploadDTO fileUploadDTO)//sourcemodelclass object
        {
            // 1)Auto mapper is used to create a mapping between  to source model object to destination model object
            FileUpload obj = new FileUpload();//destinationmodelclass object
            _mapper.Map(fileUploadDTO, obj);
            //Converting source modelobject to destination modelobject
            //Syntax:   _mapper.Map(SourceModelObject,DestinationModelObject)
            //once mapping is created, source model object can be converted to destination model object with less code and easy way.
            #region Automapperbeforecode
            //This Code was replaced by above Automapper concept.
            //obj.FileName = fileUploadDTO.FileName;
            //obj.FilePath = fileUploadDTO.FilePath;
            //obj.CreatedDatetTime = fileUploadDTO.CreatedDatetTime;
            //obj.Createdby = fileUploadDTO.Createdby;
            //obj.ModifiedFilename = fileUploadDTO.ModifiedFilename;
            //obj.Id = fileUploadDTO.Id;

            #endregion
            var result = await _filesUploadRepository.AddFileUpload(obj);
            return result;

        }

        public async Task<FileUploadDTO> GetFileUploadDetailsById(int Id)
        {
            var result = await _filesUploadRepository.GetFileUploadDetailsById(Id);
            FileUploadDTO fileUploadDTO = new FileUploadDTO();
            fileUploadDTO.ModifiedFilename = result.ModifiedFilename;
            return fileUploadDTO;
        }
    }
}
