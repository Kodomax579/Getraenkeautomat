using LootboxService.Models;
using LootboxService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LootboxService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LootboxController : ControllerBase
    {
        [HttpGet("GetLootboxes")]
        public ActionResult<List<Lootbox>> GetAll() => LootBoxService.GetAll();

        [HttpGet("GetResult/{id}")]
        public ActionResult<int> Start(int id)
        {
            var lootbox = LootBoxService.Start(id);

            if (lootbox == -1)
                return NotFound("Lootbox not found!!!!!");

            return lootbox;
        }
    }
}
