using LootboxService.Models;
using LootboxService.Services;
using Microsoft.AspNetCore.Mvc;

namespace LootboxService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LootboxController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Lootbox>> GetAll() => LootBoxService.GetAll();

        [HttpGet("{id}")]
        public ActionResult<int> Start(int id)
        {
            var lootbox = LootBoxService.Start(id);

            if (lootbox == -1)
                return NotFound("Lootbox not found!!!!!");

            return lootbox;
        }
    }
}
