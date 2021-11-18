using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        IProductService _productService;

        public ProductsController(IProductService productService) {
            _productService = productService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() {
            var result = _productService.GetAll();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _productService.GetById(id);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpPost("add")]
        public IActionResult Add(Product product) {
            var result = _productService.Add(product);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        // For testing the transaction aspect
        [HttpPost("addTwice")]
        public IActionResult AddTwice(Product product, string nameSuffix1, string nameSuffix2) {
            var result = _productService.AddTwice(product, nameSuffix1, nameSuffix2);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }
    }
}
