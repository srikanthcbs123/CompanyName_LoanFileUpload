using CompanyName_LoanFileUpload.BusinessEntities.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace CompanyName_LoanFileUpload_DBConectivity
{
    public class ConnectionFactory : IConnectionFactory
    {
        private readonly IConfiguration _config;

        public ConnectionFactory(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection GetHotelManagementSqlConnection()
        {
            SqlConnection con = new SqlConnection(Convert.ToString(_config.GetSection("ConnectionStrings:hotelmanagementSqlConnectionString").Value));
            return con;
        }
    }
}
