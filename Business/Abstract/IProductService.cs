using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract {
    public interface IProductService {
        IDataResult<List<Product>> GetAll();
        IDataResult<List<Product>> GetAllByCategoryId(int categoryId);
        IDataResult<List<Product>> GetAllByUnitPriceRange(decimal min, decimal max);
        IDataResult<Product> GetById(int id);
        IDataResult<List<ProductDetailDto>> GetProductsDetails();
        IResult Add(Product product);
        IResult Update(Product product);
        // For testing the transaction aspect
        IResult AddTwice(Product product, string nameSuffix1, string nameSuffix2);
    }
}
