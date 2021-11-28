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
    public class CategoriesController : ControllerBase {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll() {
            var result = _categoryService.GetAll();
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id) {
            var result = _categoryService.GetById(id);
            if (result.Success) {
                return Ok(result);
            } else {
                return BadRequest(result);
            }
        }

        //[HttpPost("add")]
        //public IActionResult Add(Category category) {
        //    var result = _categoryService.Add(category);
        //    if (result.Success) {
        //        return Ok(result);
        //    } else {
        //        return BadRequest(result);
        //    }
        //}
    }
}
