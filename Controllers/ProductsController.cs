using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using Supermarket.API.Domain.Models;
using Supermarket.API.Domain.Services;
using Supermarket.API.Resources;
using Supermarket.API.Extensions;

namespace Supermarket.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductResource>> GetAllAsync(){
            var products = await productService.ListAsync();
            var resources = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);
            return resources;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveProductResource resource){
            if(!ModelState.IsValid){
                return BadRequest(ModelState.GetErrorMessages());
            }

            var product = mapper.Map<SaveProductResource, Product>(resource);
            var result = await productService.SaveAsync(product);

            if(!result.Success){
                return BadRequest(result.Message);
            }
            var productResource = mapper.Map<Product, SaveProductResource>(result.Product);

            return Ok(productResource);
        }
    }
}