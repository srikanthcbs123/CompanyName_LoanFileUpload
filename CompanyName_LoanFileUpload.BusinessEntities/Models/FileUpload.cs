using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName_LoanFileUpload.BusinessEntities.Models
{
    [ExcludeFromCodeCoverage]//it is used to ingnore the test case creation for  this class.
    public class FileUpload
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ModifiedFilename { get; set; }
        public string Createdby { get; set; }
        public DateTime? CreatedDatetTime { get; set; }
    }
}
