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
            _logger.LogInformation("Method GetMoney started. UserId: {UserId}", userId);

            if (userId == 0)
            {
                _logger.LogError("Invalid request: {Reason}. UserId: {UserId}", "UserId is 0", userId);
                return BadRequest("Invalid UserId");
            }

            var money = _bankAccountService.GetMoney(userId);
            if (money == -1)
            {
                _logger.LogWarning("User not found for GetMoney. UserId: {UserId}", userId);
                return BadRequest("No user found");
            }

            _logger.LogInformation("Retrieved money for user. UserId: {UserId}, Money: {Money}", userId, money);
            _logger.LogInformation("Method GetMoney finished successfully. UserId: {UserId}", userId);

            return Ok(new BankModelDTO
            {
                Money = money,
                UserId = userId
            });
        }

        [HttpPost("CreateBankAccount/{userId}")]
        public ActionResult<BankModelDTO> CreateBankAccount(int userId)
        {
            _logger.LogInformation("Method CreateBankAccount started. UserId: {UserId}", userId);

            if (userId == 0)
            {
                _logger.LogError("Invalid request: {Reason}. UserId: {UserId}", "UserId is 0", userId);
                return BadRequest("Invalid UserId");
            }

            var startMoney = _bankAccountService.CreateBankAccount(userId);
            if (startMoney == -1)
            {
                _logger.LogWarning("Bank account already exists for UserId: {UserId}", userId);
                return BadRequest("User already exists");
            }

            _logger.LogInformation("Bank account created. UserId: {UserId}, StartMoney: {StartMoney}", userId, startMoney);
            _logger.LogInformation("Method CreateBankAccount finished successfully. UserId: {UserId}", userId);

            return Ok(new BankModelDTO
            {
                Money = startMoney,
                UserId = userId
            });
        }

        [HttpPut("EarnMoney")]
        public ActionResult<BankModelDTO> EarnMoney(BankModelDTO bankModelDTO)
        {
            _logger.LogInformation("Method EarnMoney started. DTO: {@DTO}", bankModelDTO);

            if (bankModelDTO == null)
            {
                _logger.LogError("Request body is null");
                return BadRequest("No body");
            }

            if (bankModelDTO.UserId == 0)
            {
                _logger.LogError("Invalid UserId in request body: {@DTO}", bankModelDTO);
                return BadRequest("UserId is required");
            }

            var money = _bankAccountService.EarnMoney(bankModelDTO);
            if (money == -1)
            {
                _logger.LogWarning("User not found during EarnMoney. UserId: {UserId}", bankModelDTO.UserId);
                return BadRequest("User not found");
            }

            _logger.LogInformation("User earned money. UserId: {UserId}, Amount: {Amount}, NewBalance: {NewBalance}",
                bankModelDTO.UserId, bankModelDTO.Money, money);
            _logger.LogInformation("Method EarnMoney finished successfully. UserId: {UserId}", bankModelDTO.UserId);

            return Ok(new BankModelDTO
            {
                Money = money,
                UserId = bankModelDTO.UserId
            });
        }

        [HttpPut("SpendMoney")]
        public ActionResult<BankModelDTO> SpendMoney(BankModelDTO bankModelDTO)
        {
            _logger.LogInformation("Method SpendMoney started. DTO: {@DTO}", bankModelDTO);

            if (bankModelDTO == null)
            {
                _logger.LogError("Request body is null");
                return BadRequest("No body");
            }

            var money = _bankAccountService.SpendMoney(bankModelDTO);
            if (money == -1)
            {
                _logger.LogWarning("User not found during SpendMoney. UserId: {UserId}", bankModelDTO.UserId);
                return BadRequest("User not found");
            }

            if (money == -2)
            {
                _logger.LogWarning("Not enough money for transaction. UserId: {UserId}, RequestedAmount: {Amount}",
                    bankModelDTO.UserId, bankModelDTO.Money);
                return BadRequest("Not enough money");
            }

            _logger.LogInformation("User spent money. UserId: {UserId}, Amount: {Amount}, NewBalance: {NewBalance}",
                bankModelDTO.UserId, bankModelDTO.Money, money);
            _logger.LogInformation("Method SpendMoney finished successfully. UserId: {UserId}", bankModelDTO.UserId);

            return Ok(new BankModelDTO
            {
                Money = money,
                UserId = bankModelDTO.UserId
            });
        }
    }
}
