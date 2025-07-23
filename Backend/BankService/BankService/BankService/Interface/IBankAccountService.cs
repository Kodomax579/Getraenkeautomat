using BankService.DTO;

namespace BankService.Interface
{
    public interface IBankAccountService
    {
        public double GetMoney(int userId);

        public double CreateBankAccount(int userId);

        public double EarnMoney(BankModelDTO model);
        public double SpendMoney(BankModelDTO model);


    }
}
