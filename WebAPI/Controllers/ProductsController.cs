using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] // web adresi yerine api/products yazarsan buraya gelir
    [ApiController] //Bu class bir controller diyoruz.
    //controller isimleri çoğul verilir
    //Controller'da, client'ın bizden neler isteyebileceğini yazıyoruz.
    public class ProductsController : ControllerBase
    {
        IProductService _productService; //Asla bir sınıfın içinde başka bir sınıfı new'leme
        //prensibi gereği productmanager new'lemedik onun yerine onun yerine IProductService'e
        //bir bağımlılık verdik.

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")] //Http yani internet üzerinden haberleşiyor
        public IActionResult GetAll()
        {
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

        [HttpPost("add")]//Ürün ekleme yöntemi
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


    }
}
