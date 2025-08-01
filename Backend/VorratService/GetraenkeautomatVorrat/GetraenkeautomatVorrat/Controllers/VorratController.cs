using Microsoft.AspNetCore.Mvc;
using GetraenkeautomatVorrat.Models;
using GetraenkeautomatVorrat.Services;
using GetraenkeautomatVorrat.DTO;

namespace GetraenkeautomatVorrat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VorratController : ControllerBase
    {
        private readonly VorratService _service;
        private readonly ILogger<VorratController> _logger;

        public VorratController(VorratService service, ILogger<VorratController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet("GetProducts")]
        public ActionResult<List<VorratDTO>> GetAll()
        {
            _logger.LogInformation("Method GetAll started");

            var vorrats = _service.GetAll();
            if (vorrats == null)
            {
                _logger.LogError("No products found in inventory");
                return BadRequest("No products found");
            }

            var productList = vorrats.Select(vorrat => new VorratDTO
            {
                Id = vorrat.Id,
                Amount = vorrat.Anzahl,
                Size = vorrat.Groesse,
                Name = vorrat.Name,
                Price = vorrat.Preis,
                Picture = vorrat.Picture
            }).ToList();

            _logger.LogInformation("Method GetAll finished successfully. ProductCount: {Count}", productList.Count);
            return productList;
        }

        [HttpPost("CreateProduct")]
        public ActionResult<VorratDTO> Post(VorratDTO item)
        {
            _logger.LogInformation("Method Post started. Payload: {@Item}", item);

            if (item == null)
            {
                _logger.LogError("Post failed. Request body is null");
                return BadRequest("No body");
            }

            var added = _service.Add(item);

            switch (added)
            {
                case 0:
                    _logger.LogInformation("Product created successfully. Name: {Name}, Size: {Size}", item.Name, item.Size);
                    _logger.LogInformation("Method Post finished successfully.");
                    return Ok(item);

                case 1:
                    _logger.LogWarning("Product already exists. Name: {Name}, Size: {Size}", item.Name, item.Size);
                    return BadRequest("Product already exist");

                default:
                    _logger.LogError("Unknown error occurred during product creation.");
                    return BadRequest();
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            _logger.LogInformation("Method Delete started. Id: {Id}", id);

            if (id == 0)
            {
                _logger.LogError("Delete failed. Id cannot be zero.");
                return BadRequest("Id can't be zero");
            }

            var isDeleted = _service.Delete(id);

            if (!isDeleted)
            {
                _logger.LogWarning("Delete failed. No product found for Id: {Id}", id);
                return BadRequest("No product found");
            }

            _logger.LogInformation("Product deleted successfully. Id: {Id}", id);
            _logger.LogInformation("Method Delete finished successfully.");
            return true;
        }

        [HttpGet("GetProduct/{id}")]
        public ActionResult<Vorrat> Get(int id)
        {
            _logger.LogInformation("Method Get started. Id: {Id}", id);

            var vorrat = _service.Get(id);

            if (vorrat == null)
            {
                _logger.LogWarning("Product not found. Id: {Id}", id);
                return NotFound();
            }

            _logger.LogInformation("Product retrieved successfully. Id: {Id}, Name: {Name}", vorrat.Id, vorrat.Name);
            _logger.LogInformation("Method Get finished successfully.");
            return Ok(vorrat);
        }

        [HttpPut("UpdateProductAmount")]
        public async Task<ActionResult<VorratDTO>> UpdateAmount(int amount, string name)
        {
            _logger.LogInformation("Method Update started. Amount: {@Amount}, TargetName: {TargetName}", amount, name);

            if (amount < 0)
            {
                _logger.LogError("Update failed. Amount is less then 0");
                return BadRequest("Amount is less then 0");
            }

            var updated = await _service.UpdateAmount(amount, name);

            if (updated == null)
            {
                _logger.LogWarning("Update failed. No product found with name: {Name}", name);
                return BadRequest("No product found");
            }

            _logger.LogInformation("Product updated successfully. Name: {Name}, NewAmount: {Amount}, NewSize: {Size}",
                updated.Name, updated.Amount, updated.Size);
            _logger.LogInformation("Method Update finished successfully.");
            return Ok(updated);
        }
        [HttpPut("UpdateProductPrice")]
        public ActionResult<VorratDTO> UpdatePrice(double price, string name)
        {
            _logger.LogInformation("Method Update started. Price: {@Price}, TargetName: {TargetName}", price, name);

            if (price < 0)
            {
                _logger.LogError("Update failed. Amount is less then 0");
                return BadRequest("Amount is less then 0");
            }

            var updated = _service.UpdatePrice(price, name);

            if (updated == null)
            {
                _logger.LogWarning("Update failed. No product found with name: {Name}", name);
                return BadRequest("No product found");
            }

            _logger.LogInformation("Product updated successfully. Name: {Name}, NewAmount: {Amount}, NewSize: {Size}",
                updated.Name, updated.Amount, updated.Size);
            _logger.LogInformation("Method Update finished successfully.");
            return Ok(updated);
        }
    }
}
