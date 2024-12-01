using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName_LoanFileUpload.BusinessEntities.DTOs;
using CompanyName_LoanFileUpload.BusinessEntities.Models;
namespace CompanyName_LoanFileUpload_ServiceLayer.AutoMapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {//        1) creating mapping
            //syntax: mapper.createmap < sourcemodelclass ,destination model class >();
            CreateMap<FileUploadDTO, FileUpload>();//dml performs[inser,update,delete]//from dto to table loki inserting
            CreateMap<FileUpload, FileUploadDTO>();//get methods // from table to picking data

            //CreateMap<PgAccountDTO, PgAccount>();
            //CreateMap<PgAccount, PgAccountDTO>();
        }
    }
}
