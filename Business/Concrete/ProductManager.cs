using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Core.Utilities.Results;
using Entities.DTOs;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now()==22)
            {
                return new ErrorDataResult();
            }

            return new SuccessDataResult<List<Product>>( _productDal.GetAll(),true,"Ürünler Listelendi");
        }

        public List<Product> GetAllByCategory(int id)
        {
            return _productDal.GetAll(p => p.CategoryId == id);
        }

        public List<Product> GetByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p => p.ProductId == productId);
        }

        public IResult Add(Product product)
        {
            //business codes
            if (product.ProductName.Length<2)
            {
                //magic strings
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAdded);
        }
    }
}
