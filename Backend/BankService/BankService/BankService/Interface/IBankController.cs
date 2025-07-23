using BankService.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankService.Interface
{
    public interface IBankController
    {
        [HttpGet("/GetMoney/{userId}")]
        public ActionResult<BankModelDTO> GetMoney(int userId);

        [HttpPost("/CreateBankAccount/{userId}")]
        public ActionResult<BankModelDTO> CreateBankAccount(int userId);

        [HttpPut("/EarnMoney")]
        public ActionResult<BankModelDTO> EarnMoney(BankModelDTO bankModelDTO);

        [HttpPut("/SpendMoney")]
        public ActionResult<BankModelDTO> SpendMoney(BankModelDTO bankModelDTO);
    }
}
