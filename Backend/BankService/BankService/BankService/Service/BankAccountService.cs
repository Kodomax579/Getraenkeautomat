using BankService.Data;
using BankService.DTO;
using BankService.Interface;
using BankService.Model;

namespace BankService.Service
{
    public class BankAccountService : IBankAccountService
    {
        private BankContext _context;
        public BankAccountService(BankContext bankContext) 
        {
            this._context = bankContext;
        }

        public double GetMoney(int userId)
        {
            var user = GetOneUser(userId);

            if (user == null)
                return -1;

            return user.Money;
        }

        public double CreateBankAccount(int userId)
        {
            double startMoney = 5;
            var user = GetOneUser(userId);

            if (user != null)
                return -1;

            BankAccountModel model = new BankAccountModel()
            {
                Money = startMoney,
                UserId = userId,
            };

            _context.BankAccounts.Add(model);
            _context.SaveChanges();
            return startMoney;
        }

        public double EarnMoney(BankModelDTO model)
         {
            var user = GetOneUser(model.UserId);

            if (user == null)
                return -1;

            user.Money = user.Money + model.Money;

            _context.SaveChanges();

            return user.Money;
        }
        public double SpendMoney(BankModelDTO model)
        {
            var user = GetOneUser(model.UserId);

            if (user == null)
                return -1;

            if(user.Money - model.Money < 0)
            {
                return -2;
            }
            user.Money = user.Money - model.Money;

            _context.SaveChanges();

            return user.Money;
        }

        private BankAccountModel GetOneUser(int userId)
        {
            try
            {
                var user = _context.BankAccounts.Where(user => user.UserId == userId).First();
                
                return user;
            }
            catch
            {
                return null!;
            }

        }
    }
}
