﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace CompanyName_LoanFileUpload.BusinessEntities.Interfaces
{
    public interface IConnectionFactory
    {
       SqlConnection  GetHotelManagementSqlConnection();
    }
}
