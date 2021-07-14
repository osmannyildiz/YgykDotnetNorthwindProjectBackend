using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete {
    public class ProductManager : IProductService {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal) {
            _productDal = productDal;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product) {
            //if (product.ProductName.Length < 2) {
            //    return new ErrorResult(Messages.ProductNameMustBeAtLeastTwoCharactersLong);
            //}
            
            //ValidationTool.Validate(new ProductValidator(), product);

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IDataResult<List<Product>> GetAll() {
            //if (DateTime.Now.Hour == 23) {
            //    return new ErrorDataResult<List<Product>>(_productDal.GetAll(), Messages.MaintenanceHour);
            //} else {
                return new SuccessDataResult<List<Product>>(_productDal.GetAll());
            //}
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId) {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        }
        
        public IDataResult<List<Product>> GetAllByUnitPriceRange(decimal min, decimal max) {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        public IDataResult<Product> GetById(int id) {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id));
        }

        public IDataResult<List<ProductDetailDto>> GetProductsDetails() {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsDetails());
        }
    }
}
