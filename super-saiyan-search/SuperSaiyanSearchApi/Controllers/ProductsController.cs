using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperSaiyanSearch.Api;
using SuperSaiyanSearch.Api.Interfaces;

namespace SuperSaiyanSearchApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ILogger<ProductsController> _logger;
        private readonly IProductApi _productApi;

        public ProductsController(ILogger<ProductsController> logger, IProductApi productApi)
        {
            _logger = logger;
            _productApi = productApi;
        }

        [HttpGet]
        public ProductsReadDto Get([FromQuery] string q)
        {
            return _productApi.Search(q);
        }
    }
}
