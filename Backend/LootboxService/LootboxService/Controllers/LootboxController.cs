using LootboxService.Models;
using LootboxService.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace LootboxService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LootboxController : ControllerBase
    {

        [HttpGet("GetLootboxes")]
        public ActionResult<List<Lootbox>> GetAll()
        {
            Log.Information("Request all lootboxes");

            var lootboxes = LootBoxService.GetAll();

            if(lootboxes == null)
            {
                Log.Error("No lootboxes found");
                return BadRequest("No lootboxes found");
            }
            Log.Information("Response: {@lootboxes}", lootboxes);
            return lootboxes;
        }

        [HttpGet("GetResult")]
        public ActionResult<int> Start(int id)
        {
            Log.Information("Request Lootbox");
            var lootbox = LootBoxService.Start(id);

            if (lootbox == -1)
            {
                Log.Error("Wrong Lootbox Id");
                return NotFound("Lootbox not found!!!!!");
            }
            Log.Information("Response: {@lootbox}", lootbox);
            return lootbox;
        }
    }
}
