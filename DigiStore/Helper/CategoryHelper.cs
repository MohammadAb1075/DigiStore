using DigiStore.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace DigiStore.Helper
{
    public interface ICategoryHelper
    {
        Task<List<CategoryModel>> GetAllAsync();
        Task<List<CategoryModel>> GetAllWithSPAsync();
        Task<PaginationModel<CategoryModel>> GetWithPaginationAsync(int page = 1, int limit = 5);
    }
    public class CategoryHelper : ICategoryHelper
    {
        IConfiguration _configuration;
        public CategoryHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task<List<CategoryModel>> GetAllAsync()
        {
            string connectionString = _configuration.GetConnectionString("DigiStoreDB");
            using (IDbConnection dappCon = new SqlConnection(connectionString))
            {
                var query = "select CategoryId,CategoryName from Categories";
                var result = await dappCon.QueryAsync<CategoryModel>(query);
                return result.ToList();
            }
        }
         
        public async Task<List<CategoryModel>> GetAllWithSPAsync()
        {
            string connectionString = _configuration.GetConnectionString("DigiStoreDB");
            using (IDbConnection dappCon = new SqlConnection(connectionString))
            {
                var querySP = "usp_CategoriesSelect";
                var result = await dappCon.QueryAsync<CategoryModel>(querySP, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<PaginationModel<CategoryModel>> GetWithPaginationAsync(int page = 1, int limit = 5)
        {
            var result = new PaginationModel<CategoryModel>();
            string connectionString = _configuration.GetConnectionString("DigiStoreDB");
            using (IDbConnection dappCon = new SqlConnection(connectionString))
            {
                var querySP = "SP_Categories_GetAll_WithPagination";
                int skip = (page - 1) * limit;

                var parameters = new DynamicParameters();
                parameters.Add("skip", skip);
                parameters.Add("take", limit);
                parameters.Add("total", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var queryResult = await dappCon.QueryAsync<CategoryModel>(querySP, parameters, commandType: CommandType.StoredProcedure);
                var total = parameters.Get<int>("total");

                result.Data = queryResult.ToList();
                result.Page = page; ;
                result.Limit = limit;
                result.Total = total;
                result.Pages = Convert.ToInt32(Math.Ceiling((decimal)total / limit));

                return result;
            }
        }
    }
}