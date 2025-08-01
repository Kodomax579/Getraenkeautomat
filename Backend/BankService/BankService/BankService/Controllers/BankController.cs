using BankService.DTO;
using BankService.Interface;
using BankService.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace BankService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BankController : ControllerBase, IBankController
    {
        private BankAccountService _bankAccountService;

        public BankController(BankAccountService bankAccountService)
        {
            _bankAccountService = bankAccountService;
        }

        [HttpGet("GetMoney")]
        public ActionResult<BankModelDTO> GetMoney(int userId)
        {
            Log.Information("Method GetMoney started. UserId: {@UserId}", userId);

            if (userId == 0)
            {
                Log.Error("Invalid request: Invalid UserId. UserId: {@UserId}", "UserId is 0", userId);
                return BadRequest("Invalid UserId");
            }

            var money = _bankAccountService.GetMoney(userId);
            if (money == -1)
            {
                Log.Warning("User not found for GetMoney. UserId: {@UserId}", userId);
                return BadRequest("No user found");
            }

            Log.Information("Retrieved money for user. UserId: {@UserId}, Money: {@Money}", userId, money);
            Log.Information("Method GetMoney finished successfully. UserId: {@UserId}", userId);

            return Ok(new BankModelDTO
            {
                Money = money,
                UserId = userId
            });
        }

        [HttpPost("CreateBankAccount/{userId}")]
        public ActionResult<BankModelDTO> CreateBankAccount(int userId)
        {
            Log.Information("Method CreateBankAccount started. UserId: {@UserId}", userId);

            if (userId == 0)
            {
                Log.Error("Invalid request: Invalid UserId. UserId: {@UserId}", "UserId is 0", userId);
                return BadRequest(new { message = "Invalid UserId" });
            }

            var startMoney = _bankAccountService.CreateBankAccount(userId);
            if (startMoney == -1)
            {
                Log.Warning("Bank account already exists for UserId: {@UserId}", userId);
                return BadRequest(new { message = "User already exists" });
            }

            Log.Information("Bank account created. UserId: {@UserId}, StartMoney: {@StartMoney}", userId, startMoney);
            Log.Information("Method CreateBankAccount finished successfully. UserId: {@UserId}", userId);

            return Ok(new BankModelDTO
            {
                Money = startMoney,
                UserId = userId
            });
        }

        [HttpPut("EarnMoney")]
        public ActionResult<BankModelDTO> EarnMoney(BankModelDTO bankModelDTO)
        {
            Log.Information("Method EarnMoney started. DTO: {@DTO}", bankModelDTO);

            if (bankModelDTO == null)
            {
                Log.Error("Request body is null");
                return BadRequest(new { message = "No body" });
            }

            if (bankModelDTO.UserId == 0)
            {
                Log.Error("Invalid UserId in request body: {@DTO}", bankModelDTO);
                return BadRequest(new { message = "UserId is required" });
            }

            var money = _bankAccountService.EarnMoney(bankModelDTO);
            if (money == -1)
            {
                Log.Warning("User not found during EarnMoney. UserId: {@UserId}", bankModelDTO.UserId);
                return BadRequest(new { message = "User not found" });
            }

            Log.Information("User earned money. UserId: {@UserId}, Amount: {@Amount}, NewBalance: {@NewBalance}",
                bankModelDTO.UserId, bankModelDTO.Money, money);
            Log.Information("Method EarnMoney finished successfully. UserId: {@UserId}", bankModelDTO.UserId);

            return Ok(new BankModelDTO
            {
                Money = money,
                UserId = bankModelDTO.UserId
            });
        }

        [HttpPut("SpendMoney")]
        public ActionResult<BankModelDTO> SpendMoney(BankModelDTO bankModelDTO)
        {
            Log.Information("Method SpendMoney started. DTO: {@DTO}", bankModelDTO);

            if (bankModelDTO == null)
            {
                Log.Error("Request body is null");
                return BadRequest(new { message = "No body" });
            }

            var money = _bankAccountService.SpendMoney(bankModelDTO);
            if (money == -1)
            {
                Log.Warning("User not found during SpendMoney. UserId: {@UserId}", bankModelDTO.UserId);
                return BadRequest(new { message = "User not found" });
            }

            if (money == -2)
            {
                Log.Warning("Not enough money for transaction. UserId: {@UserId}, RequestedAmount: {@Amount}",
                    bankModelDTO.UserId, bankModelDTO.Money);
                return BadRequest(new { message = "Not enough money" });
            }

            Log.Information("User spent money. UserId: {@UserId}, Amount: {@Amount}, NewBalance: {@NewBalance}",
                bankModelDTO.UserId, bankModelDTO.Money, money);
            Log.Information("Method SpendMoney finished successfully. UserId: {UserId}", bankModelDTO.UserId);

            return Ok(new BankModelDTO
            {
                Money = money,
                UserId = bankModelDTO.UserId
            });
        }
    }
}
