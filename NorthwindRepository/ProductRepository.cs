using NorthwindRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using System.Data;

namespace NorthwindRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DbConnection _dbConnection;
        public ProductRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public bool CreateProduct(ProductInputModelForRepo productInputModelForRepo)
        {
            var sql = @"INSERT INTO [Products]
                        ([ProductName]
                       ,[SupplierID]
                       ,[CategoryID]
                       ,[QuantityPerUnit]
                       ,[UnitPrice] 
                       ,[UnitsInStock]
                       ,[UnitsOnOrder]
                       ,[ReorderLevel]
                       ,[Discontinued])
                        Values
                        (@productname,
                        @supplierid,
                        @categoryid,
                        @quantityperunit,
                        @unitprice,
                        @unitsinstock,
                        @unitsonorder,
                        @reorderlevel,
                        @discontinued)";
            var parameters = new DynamicParameters();
            parameters.Add("@productname", productInputModelForRepo.ProductName, DbType.String);
            parameters.Add("@supplierid", productInputModelForRepo.SupplierID, DbType.Int32);
            parameters.Add("@categoryid", productInputModelForRepo.CategoryID, DbType.Int32);
            parameters.Add("@quantityperunit", productInputModelForRepo.QuantityPerUnit, DbType.String);
            parameters.Add("@unitprice", productInputModelForRepo.UnitPrice, DbType.Decimal);
            parameters.Add("@unitsinstock", productInputModelForRepo.UnitsInStock, DbType.Int16);
            parameters.Add("@unitsonorder", productInputModelForRepo.UnitsOnOrder, DbType.Int16);
            parameters.Add("@reorderlevel", productInputModelForRepo.ReorderLevel, DbType.Int16);
            parameters.Add("@discontinued", productInputModelForRepo.Discontinued, DbType.Boolean);
            int result = _dbConnection.Execute(sql, parameters);
            return result > 0;

        }

        public bool DeleteProduct(int productId)
        {
            var sql = @"delete from [products]
                        where productId = @productId";
            var parameter = new DynamicParameters();
            parameter.Add("@productId", productId);
            int result = _dbConnection.Execute(sql, parameter);
            return result > 0;

        }

        public ProductOutputModelForRepo GetProduct(int productId)
        {
            var sql = @"select productid,
                               productname,
                               supplierid,
                               categoryid,
                               quantityperunit,
                               unitprice,
                               unitsinstock,
                               unitsonorder,
                               reorderlevel,
                               discontinued 
                        FROM products
                        WHERE productid = @productId";
            var parameter = new DynamicParameters();
            parameter.Add("@productId", productId);
            var result = _dbConnection.QueryFirstOrDefault<ProductOutputModelForRepo>(sql, parameter);
            return result;
        }

        public IEnumerable<ProductOutputModelForRepo> GetProducts()
        {
            var sql = @"select productid,
                               productname,
                               supplierid,
                               categoryid,
                               quantityperunit,
                               unitprice,
                               unitsinstock,
                               unitsonorder,
                               reorderlevel,
                               discontinued 
                        FROM products";
            var result = _dbConnection.Query<ProductOutputModelForRepo>(sql);
            return result;
        }

        public bool UpdateProduct(int productId, ProductInputModelForRepo productInputModelForRepo)
        {
            var sql = @"UPDATE [Products]
                       SET [ProductName] = @productName
                       ,[SupplierID] = @supplierID
                       ,[CategoryID] = @categoryID
                       ,[QuantityPerUnit] = @quantityPerUnit
                       ,[UnitPrice] = @unitPrice
                       ,[UnitsInStock] = @unitsInStock
                       ,[UnitsOnOrder] = @unitsOnOrder
                       ,[ReorderLevel] = @reorderLevel
                       ,[Discontinued] = @discontinued
                 WHERE [ProductId] = @productId";
            var parameters = new DynamicParameters();
            parameters.Add("@productName", productInputModelForRepo.ProductName, DbType.String);
            parameters.Add("@supplierID", productInputModelForRepo.SupplierID, DbType.Int32);
            parameters.Add("@categoryID", productInputModelForRepo.CategoryID, DbType.Int32);
            parameters.Add("@quantityPerUnit", productInputModelForRepo.QuantityPerUnit, DbType.String);
            parameters.Add("@unitPrice", productInputModelForRepo.UnitPrice, DbType.Decimal);
            parameters.Add("@unitsInStock", productInputModelForRepo.UnitsInStock, DbType.Int16);
            parameters.Add("@unitsOnOrder", productInputModelForRepo.UnitsOnOrder, DbType.Int16);
            parameters.Add("@reorderLevel", productInputModelForRepo.ReorderLevel, DbType.Int16);
            parameters.Add("@discontinued", productInputModelForRepo.Discontinued, DbType.Boolean);
            parameters.Add(@"productId", productId);

            int result = _dbConnection.Execute(sql, parameters);
            return result > 0;
        }
    }
}
