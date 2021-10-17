using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete {
    public class ProductManager : IProductService {
        IProductDal _productDal;
        ICategoryService _categoryManager;

        public ProductManager(IProductDal productDal, ICategoryService categoryManager) {
            _productDal = productDal;
            _categoryManager = categoryManager;
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product) {
            var errorResult = BusinessEngine.Run(
                CheckIfProductCountOfCategoryExceeded(product.CategoryId),
                CheckIfProductWithSameNameExists(product.ProductName),
                CheckIfThereAreTooManyCategories()
            );
            if (errorResult != null) {
                return errorResult;
            }

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

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product) {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
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
