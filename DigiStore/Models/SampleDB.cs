namespace DigiStore.Models
{
    public static class SampleDB
    {
        public static List<ProductModel> Products = new List<ProductModel>
        {
            new ProductModel{ ProductId =1,CategoryId=10,ProductName="Product 1",Price=1000},
            new ProductModel{ ProductId =2,CategoryId=10,ProductName="Product 2",Price=2000},
            new ProductModel{ ProductId =3,CategoryId=10,ProductName="Product 3",Price=3000},
            new ProductModel{ ProductId =4,CategoryId=11,ProductName="Product 4",Price=4000},
            new ProductModel{ ProductId =5,CategoryId=11,ProductName="Product 5",Price=5000},
            new ProductModel{ ProductId =6,CategoryId=11,ProductName="Product 6",Price=6000},
            new ProductModel{ ProductId =7,CategoryId=12,ProductName="Product 7",Price=7000},
            new ProductModel{ ProductId =8,CategoryId=12,ProductName="Product 8",Price=8000},
            new ProductModel{ ProductId =9,CategoryId=12,ProductName="Product 9",Price=9000},
            new ProductModel{ ProductId =10,CategoryId=13,ProductName="Product 10",Price=10000},
        };

        public static List<CategoryModel> Categories = new List<CategoryModel>
        {
            new CategoryModel{ CategoryId=10,CategoryName="Mobile"},
            new CategoryModel{ CategoryId=11,CategoryName="Tablet"},
            new CategoryModel{ CategoryId=12,CategoryName="Laptop"},
            new CategoryModel{ CategoryId=13,CategoryName="PC"},
        };
    }
}