using System.Data.SqlClient;

namespace DigiStore.Utilities
{
    //Connection
    public interface ISQLUtitily
    {
        SqlConnection Connection(); 
    }
    public class SQLUtitily:ISQLUtitily
    {
        private IConfiguration _configuration;
        public SQLUtitily(IConfiguration configuration)
        {
            _configuration = configuration;   
        }

        public SqlConnection Connection()
        {
            string connectionString = _configuration.GetConnectionString("DBDigiStore");
            return new SqlConnection(connectionString); 
        }
    }
}
