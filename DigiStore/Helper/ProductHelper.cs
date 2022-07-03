using Dapper;
using DigiStore.Models;
using DigiStore.Utilities;
using System.Data;

namespace DigiStore.Helper
{
    public interface IProductHelper
    {
        Task<List<ProductModel>> GetAllAsync(int count = 0);
        Task<List<ProductModel>> GetAllWithSPAsync(int count = 0);
        Task<ProductModel> GetByIdWithSPAsync(int id);
        Task<(List<ProductModel>, List<CategoryModel>)> GetMultipleProducts_CategoriesAsync();
        Task AddAsync(ProductModel model);
        Task<ProductModel> AddWithSPAsync(ProductModel model);
        Task UpdateAsync(ProductModel model);
        Task UpdateWithSPAsync(ProductModel model);
        Task DeleteAsync(int id);
        Task DeleteWithSPAsync(int id);
    }
    public class ProductHelper : IProductHelper
    {
        ISQLUtitily _sqlUtitily;

        public ProductHelper(ISQLUtitily sqlUtitily)
        {
            _sqlUtitily = sqlUtitily;
        }

        public async Task<List<ProductModel>> GetAllAsync(int count = 0)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
            //    var query = @"Select ProductId, ProductNAme, CategoryId, Price, Email 
            //                    From Products
            //                    OFFSET @index ROWS
            //                    ORDER BY Price
	           //                 FETCH NEXT @PageSize ROWS ONLY";
                var query = @$"Select Top ({count}) ProductId, ProductNAme, CategoryId, Price, Email From Products ORDER BY Price Desc";
                var result = await dbConnection.QueryAsync<ProductModel>(query);
                return result.ToList();
            }
        }
        
        public async Task<List<ProductModel>> GetAllWithSPAsync(int count = 0)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var querySP = "usp_ProductsSelect";
                var parameters = new DynamicParameters();
                var result = await dbConnection.QueryAsync<ProductModel>(sql: querySP, parameters, commandType: CommandType.StoredProcedure);
                if (count > 0) {
                    result = result.Take(count).OrderByDescending(x => x.Price);
                }
                return result.ToList();
            }
        }

        public async Task<ProductModel> GetByIdWithSPAsync(int id)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var querySP = "usp_ProductsSelect";
                var parameters = new DynamicParameters();
                parameters.Add("productId", id);
                var result = await dbConnection.QuerySingleOrDefaultAsync<ProductModel>(querySP, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<(List<ProductModel>, List<CategoryModel>)> GetMultipleProducts_CategoriesAsync()
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var query = "Select * From Products; Select * From Categories";
                //var query = "Select * From Products, Categories";
                //var query = "Select * From Products P Inner Join Categories C On C.CategoryId = P.CategoryId";

                var result = await dbConnection.QueryMultipleAsync(query);
                var products = await result.ReadAsync<ProductModel>();
                var categories = await result.ReadAsync<CategoryModel>();

                return (products.ToList(), categories.ToList());
            }
        }

        public async Task AddAsync(ProductModel model)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var query = "Insert Into Products(ProductName, CategoryId, Price, Email) Values(@ProductName, @CategoryId, @Price, @Email)";

                var parameters = new DynamicParameters();
                parameters.Add("ProductName", model.ProductName);
                parameters.Add("CategoryId", model.CategoryId);
                parameters.Add("Price", model.Price);
                parameters.Add("Email", model.Email);
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
        public async Task<ProductModel> AddWithSPAsync(ProductModel model)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var querySP = "usp_ProductsInsert";
                var parameters = new DynamicParameters();
                parameters.Add("ProductName", model.ProductName);
                parameters.Add("CategoryId", model.CategoryId);
                parameters.Add("Price", model.Price);
                parameters.Add("Email", model.Email);
                parameters.Add("Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await dbConnection.ExecuteAsync(querySP, parameters, commandType: CommandType.StoredProcedure);
                model.ProductId = parameters.Get<int>("Id");
                return model;
            }
        }

        public async Task UpdateAsync(ProductModel model)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var query = "Update Products Set ProductName=@ProductName, CategoryId=@CategoryId, Price=@Price, Email=@Email Where ProductId=@ProductId";

                var parameters = new DynamicParameters();
                parameters.Add("ProductId", model.ProductId);
                parameters.Add("ProductName", model.ProductName);
                parameters.Add("CategoryId", model.CategoryId);
                parameters.Add("Price", model.Price);
                parameters.Add("Email", model.Email);
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
        
        public async Task UpdateWithSPAsync(ProductModel model)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var querySP = "usp_ProductsUpdate";

                var parameters = new DynamicParameters();
                parameters.Add("ProductId", model.ProductId);
                parameters.Add("ProductName", model.ProductName);
                parameters.Add("CategoryId", model.CategoryId);
                parameters.Add("Price", model.Price);
                parameters.Add("Email", model.Email);
                await dbConnection.ExecuteAsync(querySP, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var query = "Delete From Products Where ProductId=@id";
                var parameters = new DynamicParameters();
                parameters.Add("productId", id);
                await dbConnection.ExecuteAsync(query, parameters);
            }
        }
        
        public async Task DeleteWithSPAsync(int id)
        {
            using (IDbConnection dbConnection = _sqlUtitily.Connection())
            {
                var querySP = "usp_ProductsDelete";
                var parameters = new DynamicParameters();
                parameters.Add("productId", id);
                await dbConnection.ExecuteAsync(querySP, parameters, commandType: CommandType.StoredProcedure) ;
            }
        }
    }
}