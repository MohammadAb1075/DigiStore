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
        Task<CategoryModel> GetByIdAsync(int id);
        Task<PaginationModel<CategoryModel>> GetWithPaginationAsync(int page = 1, int limit = 5);
        Task AddWithSPAsync(CategoryModel model);
        Task UpdateWithSPAsync(int id, CategoryModel model);
        Task DeleteWithSPAsync(int id);
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
        public async Task<CategoryModel> GetByIdAsync(int id)
        {
            string connectionString = _configuration.GetConnectionString("DigiStoreDB");
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var query = "usp_CategoriesSelect";
                var parameters = new DynamicParameters();
                parameters.Add("CategoryId", id);

                var result = await dbConnection.QuerySingleOrDefaultAsync<CategoryModel>(query, parameters, commandType: CommandType.StoredProcedure);
                return result;
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
        public async Task AddWithSPAsync(CategoryModel model)
        {
            string connectionString = _configuration.GetConnectionString("DigiStoreDB");
            using (IDbConnection dappCon = new SqlConnection(connectionString))
            {
                var querySP = "usp_CategoriesInsert";

                var parameters = new DynamicParameters();
                //parameters.Add("CategoryId", model.CategoryId);
                parameters.Add("CategoryName", model.CategoryName);

                await dappCon.ExecuteAsync(querySP, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task UpdateWithSPAsync(int id, CategoryModel model)
        {
            
            string connectionString = _configuration.GetConnectionString("DigiStoreDB");
            using (IDbConnection dbConnection = new SqlConnection(connectionString))
            {
                var querySP = "usp_CategoriesUpdate";
                var parameters = new DynamicParameters();
                parameters.Add("CategoryId", id);
                //parameters.Add("CategoryId", model.CategoryId);
                parameters.Add("CategoryName", model.CategoryName);
                await dbConnection.ExecuteAsync(querySP, parameters, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task DeleteWithSPAsync(int id)
        {
            string connectionString = _configuration.GetConnectionString("DigiStoreDB");
            using (IDbConnection dappCon = new SqlConnection(connectionString))
            {
                var querySP = "usp_CategoriesDelete";

                var parameters = new DynamicParameters();
                parameters.Add("CategoryId", id);

                await dappCon.ExecuteAsync(querySP, parameters, commandType: CommandType.StoredProcedure);
            }
        }

    }
}