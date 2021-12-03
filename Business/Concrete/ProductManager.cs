using Business.Abstract;
using Business.Aspects.Autofac.Auth;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Business.Concrete {
    public class ProductManager : IProductService {
        IProductDal _productDal;
        ICategoryService _categoryManager;

        public ProductManager(IProductDal productDal, ICategoryService categoryManager) {
            _productDal = productDal;
            _categoryManager = categoryManager;
        }

        //[SecuredOperation("products.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product) {
            var errorResult = BusinessEngine.Run(
                //CheckIfProductCountOfCategoryExceeded(product.CategoryId),
                CheckIfProductWithSameNameExists(product.ProductName),
                CheckIfThereAreTooManyCategories()
            );
            if (errorResult != null) {
                return errorResult;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAll() {
            //Thread.Sleep(2000);
            return new SuccessDataResult<List<Product>>(_productDal.GetAll());
        }

        [CacheAspect]
        public IDataResult<List<Product>> GetAllByCategoryId(int categoryId) {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.CategoryId == categoryId));
        }
        
        public IDataResult<List<Product>> GetAllByUnitPriceRange(decimal min, decimal max) {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max));
        }

        [CacheAspect]
        public IDataResult<Product> GetById(int id) {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == id));
        }

        [CacheAspect]
        public IDataResult<List<ProductDetailDto>> GetProductsDetails() {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product) {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        // For testing the transaction aspect
        [SecuredOperation("products.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [TransactionScopeAspect]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult AddTwice(Product product, string nameSuffix1, string nameSuffix2) {
            var result1 = Add(new Product {
                CategoryId = product.CategoryId,
                ProductName = product.ProductName + " " + nameSuffix1,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice
            });
            var result2 = Add(new Product {
                CategoryId = product.CategoryId,
                ProductName = product.ProductName + " " + nameSuffix2,
                UnitsInStock = product.UnitsInStock,
                UnitPrice = product.UnitPrice
            });
            
            if (!result1.Success || !result2.Success) {
                throw new Exception("Transaction failed.");
            }

            return new SuccessResult("Transaction was successful.");
        }

        private IResult CheckIfProductCountOfCategoryExceeded(int categoryId) {
            if (_productDal.GetAll(p => p.CategoryId == categoryId).Count >= 10) {
                return new ErrorResult(Messages.ProductCountOfCategoryExceeded);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductWithSameNameExists(string productName) {
            if (_productDal.GetAll(p => p.ProductName == productName).Any()) {
                return new ErrorResult(Messages.ProductWithSameNameExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfThereAreTooManyCategories() {
            if (_categoryManager.GetAll().Data.Count > 15) {
                return new ErrorResult(Messages.ThereAreTooManyCategories);
            }
            return new SuccessResult();
        }
    }
}
