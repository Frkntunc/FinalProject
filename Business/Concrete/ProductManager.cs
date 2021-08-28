using Business.Abstract;
using Business.BusinessAspect.Autofact;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal; //bu yarın başka bir şey de olabilir o yüzden buraya somut bişey vermedik 
        ICategoryService _categoryService;
        public ProductManager(IProductDal productDal,ICategoryService categoryService) //bir entitymanager kendisi hariç başka bir dal'ı enjekte edemez
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))] //Add metodunu doğrula productvalidatordeki kurallara göre
        //validation sadece yapısal olarak girilen ürün bilgisinin bizim kurallarımıza uygun olup olmamasıdır
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductNameExists(product.ProductName),CheckIfCategoryLimitExceded());

            if (result !=null) //kurala uymayan bir durum oluşmuşsa
            {
                return result;
            }

            _productDal.Add(product);
            //return new result(true, "ürün eklendi");//result üstüne tıklayıp generate constructor dedik
            return new SuccessResult(Messages.ProductAdded); //burayı artık boş da gönderebiliriz çünkü succesresultta çalıştırıyoruz kodları
        }

        public IDataResult<List<Product>> GetAll()
        {
            if (DateTime.Now.Hour == 15)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }

            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int Id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==Id));
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId==productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        public IResult Update(Product product)
        {
            throw new NotImplementedException();
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId) //Bu iş kodunu hem add'de hem de update'de kullanacağımız için ayrı bir metod yaptık
            //private yapma nedeni sadece bu class'ta kullanacağım
        {
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count; //categoryıd tarayıp sayısını bulduk
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string ProductName)
        {
            var result = _productDal.GetAll(p => p.ProductName == ProductName).Any(); //Any var mı demek
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded() //Bu tek başına bir servis olsaydı categorymanager'a yazardık
            //Ancak bu sadece categoryservice'i kullanan bir araç
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }
    }
}
