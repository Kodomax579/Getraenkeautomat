using LootboxService.Models;
using LootboxService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LootboxService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LootboxController : ControllerBase
    {
        private ILogger<LootboxController> _logger;
        public LootboxController(ILogger<LootboxController> logger) 
        {
            _logger = logger;
        }

        [HttpGet("GetLootboxes")]
        public ActionResult<List<Lootbox>> GetAll()
        {
            _logger.LogInformation("Request all lootboxes");

            var lootboxes = LootBoxService.GetAll();

            if(lootboxes == null)
            {
                _logger.LogError("No lootboxes found");
                return BadRequest("No lootboxes found");
            }
            _logger.LogInformation("Response: {lootboxes}", lootboxes);
            return lootboxes;
        }

        [HttpGet("GetResult")]
        public ActionResult<int> Start(int id)
        {
            _logger.LogInformation("Request Lootbox");
            var lootbox = LootBoxService.Start(id);

            if (lootbox == -1)
            {
                _logger.LogError("Wrong Lootbox Id");
                return NotFound("Lootbox not found!!!!!");
            }
            _logger.LogInformation("Response: {lootbox}", lootbox);
            return lootbox;
        }
    }
}
