using Microsoft.AspNetCore.Mvc;
using GetraenkeautomatVorrat.Models;
using GetraenkeautomatVorrat.Services;
using GetraenkeautomatVorrat.DTO;
using Serilog;

namespace GetraenkeautomatVorrat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VorratController : ControllerBase
    {
        private readonly VorratService _service;

        public VorratController(VorratService service)
        {
            _service = service;
        }

        [HttpGet("GetProducts")]
        public ActionResult<List<VorratDTO>> GetAll()
        {
            Log.Information("Method GetAll started");

            var vorrats = _service.GetAll();
            if (vorrats == null)
            {
                Log.Error("No products found in inventory");
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

            Log.Information("Method GetAll finished successfully. ProductCount: {Count}", productList.Count);
            return productList;
        }

        [HttpPost("CreateProduct")]
        public ActionResult<VorratDTO> Post(VorratDTO item)
        {
            Log.Information("Method Post started. Payload: {@Item}", item);

            if (item == null)
            {
                Log.Error("Post failed. Request body is null");
                return BadRequest("No body");
            }

            var added = _service.Add(item);

            switch (added)
            {
                case 0:
                    Log.Information("Product created successfully. Name: {Name}, Size: {Size}", item.Name, item.Size);
                    Log.Information("Method Post finished successfully.");
                    return Ok(item);

                case 1:
                    Log.Warning("Product already exists. Name: {Name}, Size: {Size}", item.Name, item.Size);
                    return BadRequest("Product already exist");

                default:
                    Log.Error("Unknown error occurred during product creation.");
                    return BadRequest();
            }
        }

        [HttpDelete("DeleteProduct/{id}")]
        public ActionResult<bool> Delete(int id)
        {
            Log.Information("Method Delete started. Id: {Id}", id);

            if (id == 0)
            {
                Log.Error("Delete failed. Id cannot be zero.");
                return BadRequest("Id can't be zero");
            }

            var isDeleted = _service.Delete(id);

            if (!isDeleted)
            {
                Log.Warning("Delete failed. No product found for Id: {Id}", id);
                return BadRequest("No product found");
            }

            Log.Information("Product deleted successfully. Id: {Id}", id);
            Log.Information("Method Delete finished successfully.");
            return true;
        }

        [HttpGet("GetProduct/{id}")]
        public ActionResult<Vorrat> Get(int id)
        {
            Log.Information("Method Get started. Id: {Id}", id);

            var vorrat = _service.Get(id);

            if (vorrat == null)
            {
                Log.Warning("Product not found. Id: {Id}", id);
                return NotFound();
            }

            Log.Information("Product retrieved successfully. Id: {Id}, Name: {Name}", vorrat.Id, vorrat.Name);
            Log.Information("Method Get finished successfully.");
            return Ok(vorrat);
        }

        [HttpPut("UpdateProductAmount")]
        public async Task<ActionResult<VorratDTO>> UpdateAmount(int amount, string name)
        {
            Log.Information("Method Update started. Amount: {@Amount}, TargetName: {TargetName}", amount, name);

            if (amount < 0)
            {
                Log.Error("Update failed. Amount is less then 0");
                return BadRequest("Amount is less then 0");
            }

            var updated = await _service.UpdateAmount(amount, name);

            if (updated == null)
            {
                Log.Warning("Update failed. No product found with name: {Name}", name);
                return BadRequest("No product found");
            }

            Log.Information("Product updated successfully. Name: {Name}, NewAmount: {Amount}, NewSize: {Size}",
                updated.Name, updated.Amount, updated.Size);
            Log.Information("Method Update finished successfully.");
            return Ok(updated);
        }
        [HttpPut("UpdateProductPrice")]
        public ActionResult<VorratDTO> UpdatePrice(double price, string name)
        {
            Log.Information("Method Update started. Price: {@Price}, TargetName: {TargetName}", price, name);

            if (price < 0)
            {
                Log.Error("Update failed. Amount is less then 0");
                return BadRequest("Amount is less then 0");
            }

            var updated = _service.UpdatePrice(price, name);

            if (updated == null)
            {
                Log.Warning("Update failed. No product found with name: {Name}", name);
                return BadRequest("No product found");
            }

            Log.Information("Product updated successfully. Name: {Name}, NewAmount: {Amount}, NewSize: {Size}",
                updated.Name, updated.Amount, updated.Size);
            Log.Information("Method Update finished successfully.");
            return Ok(updated);
        }
    }
}
