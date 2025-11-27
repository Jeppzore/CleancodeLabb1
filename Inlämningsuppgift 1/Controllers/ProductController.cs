using Inlämningsuppgift_1.Dtos.Products;
using Inlämningsuppgift_1.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inlämningsuppgift_1.Controllers
{
    [Route("api/products")]
    [ApiController]
    public partial class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAll());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var p = await _service.GetById(id);
            if (p == null) return NotFound();

            return Ok(p);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string q, [FromQuery] decimal? maxPrice)
            => Ok(await _service.Search(q, maxPrice));

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest req)
        {
            if (string.IsNullOrEmpty(req.Name))
                return BadRequest("Name is required");

            var created = await _service.Create(req);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPost("{id}/stock/increase")]
        public async Task <IActionResult> IncreaseStock(int id, [FromQuery] int amount)
        {           
            if (amount <= 0)
                return BadRequest("Amount must be greater than zero");

            var ok = await _service.IncreaseStock(id, amount);
            if (!ok) return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto product)
        {
            if (id != product.Id)
                return BadRequest("Product ID mismatch");

            await _service.Update(product);
            return NoContent();
        }
    }
}
