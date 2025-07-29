using BankService.DTO;
using BankService.Interface;
using BankService.Service;
using Microsoft.AspNetCore.Mvc;

namespace BankService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase, IBankController
    {
        private readonly ILogger<BankController> _logger;
        private BankAccountService _bankAccountService;

        public BankController(ILogger<BankController> logger, BankAccountService bankAccountService)
        {
            _logger = logger;
            _bankAccountService = bankAccountService;
        }

        [HttpGet("GetMoney/{userId}")]
        public ActionResult<BankModelDTO> GetMoney(int userId)
        {
            double money;

            if (userId == 0)
            {
                _logger.LogError("Wrong UserId");
                return BadRequest();
            }

            money = this._bankAccountService.GetMoney(userId);
            if (money == -1)
            {
                _logger.LogError("No user found");
                return BadRequest("No user found");
            }
            _logger.LogInformation("User found");

            var moneyDTO = new BankModelDTO()
            {
                Money = money,
                UserId = userId
            };

            return Ok(moneyDTO);
        }

        [HttpPost("CreateBankAccount/{userId}")]
        public ActionResult<BankModelDTO> CreateBankAccount(int userId)
        {
            double startMoney;
            if (userId == 0)
            {
                _logger.LogError("Wrong userId");
                return BadRequest();
            }

            startMoney = this._bankAccountService.CreateBankAccount(userId);

            if (startMoney == -1)
            {
                _logger.LogError("User already exists");
                return BadRequest();
            }

            var bankAccountDTO = new BankModelDTO() 
            { 
                Money = startMoney ,
            };
            return Ok(bankAccountDTO);
        }

        [HttpPut("EarnMoney")]
        public ActionResult<BankModelDTO> EarnMoney(BankModelDTO bankModelDTO)
        {
            double money;
            if(bankModelDTO == null)
            {
                _logger.LogError("No body");
                return BadRequest("No body");
            }
            if (bankModelDTO.UserId == 0)
            {
                _logger.LogError("UserId is required");
                return BadRequest("UserId is required");
            }

            money = this._bankAccountService.EarnMoney(bankModelDTO);

            if (money == -1)
            {
                _logger.LogError("User not found");
                return BadRequest("User not found");
            }

            var bankAccountDTO = new BankModelDTO()
            {
                Money = money,
                UserId = bankModelDTO.UserId
            };
            return Ok(bankAccountDTO);
        }

        [HttpPut("SpendMoney")]
        public ActionResult<BankModelDTO> SpendMoney(BankModelDTO bankModelDTO)
        {
            double money;
            if (bankModelDTO == null)
            {
                _logger.LogError("No body");
                return BadRequest("No body");
            }

            money = this._bankAccountService.SpendMoney(bankModelDTO);

            if (money == -1)
            {
                _logger.LogError("User not found");
                return BadRequest("User not found");
            }
            if(money == -2)
            {
                _logger.LogError("not enough money");
                return BadRequest("Not enough money");
            }

            var bankAccountDTO = new BankModelDTO()
            {
                Money = money,
                UserId = bankModelDTO.UserId
            };
            return Ok(bankAccountDTO);
        }

    }
}
