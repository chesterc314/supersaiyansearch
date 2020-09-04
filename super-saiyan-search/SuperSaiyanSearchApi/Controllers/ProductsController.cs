using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SuperSaiyanSearch.Api;
using SuperSaiyanSearch.Api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Caching.Memory;

namespace SuperSaiyanSearchApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IProductApi _productApi;
        private readonly IMemoryCache _cache;
        public ProductsController(ILogger<ProductsController> logger, IProductApi productApi, IMemoryCache cache)
        {
            _logger = logger;
            _productApi = productApi;
            _cache = cache;
        }
        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<ProductsReadDto> Get([FromQuery] string q)
        {
            var results = new ProductsReadDto(new List<ProductReadDto>());
            try
            {
                results = _cache.GetOrCreate(q, keyword => {
                    keyword.SlidingExpiration = TimeSpan.FromHours(1);
                    return _productApi.Search(q);
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(new EventId(results.GetHashCode(), Guid.NewGuid().ToString()), ex, $"Error occurred proccessing the request. Query parameter: {q}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
            if (results.TotalResults == 0)
            {
                var notFoundMessage = $"No products found for your search query: {q}";
                _logger.LogInformation(notFoundMessage);
                return NotFound(notFoundMessage);
            }
            _logger.LogInformation($"Total results returned: {results.TotalResults} for search query: {q}");
            return Ok(results);
        }
    }
}
