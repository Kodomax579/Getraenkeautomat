using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Xml.Linq;
using User.Data;
using User.DTO;
using User.Models;
using UserService.DTO;
using UserService.Services;

namespace User.Services
{
    public class Service
    {
        private UserContext _dbContext;
        private RequestService _requestService;

        public Service(UserContext userContext, RequestService requestService)
        {
            this._dbContext = userContext;
            this._requestService = requestService;
        }

        public async Task<int> CreateUser(CreateUserDTO userDTO)
        {
            if (userDTO == null)
                throw new ArgumentNullException("user is Null");

            try
            {
                var user = _dbContext.Users.First(user => user.Name == userDTO.name);
                if (user != null)
                    return -1;
            }
            catch { }

            UserModel model = new UserModel()
            {
                Name = userDTO.name,
                Email = userDTO.email,
                Password = userDTO.password,
                Level = 1,
            };
            if (model.Name == null)
                return -2;

            var response = await this._requestService.CreateBankAccount(model.Name);

            if (response == 0)
            {
                return -3;
            }

            _dbContext.Add(model);
            _dbContext.SaveChanges();


            return response;
        }



        public UserDTO? UpdateUser(UserDTO userDTO)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userDTO.Id);
            if (user == null) return null;

            user.Level = userDTO.level;
            _dbContext.SaveChanges();

            return new UserDTO
            {
                Id = user.Id,
                name = user.Name,
                level = user.Level
            };
        }



        public UserDTO GetUser(string uName, string uPassword)
        {
            if (uPassword == null && uName == null)
            {
                throw new ArgumentNullException("user is null");
            }
            try
            {
                var user = _dbContext.Users.First(u => u.Name == uName);

                UserDTO userDTO = new UserDTO()
                {
                    Id = user.Id,
                    level = user.Level,
                    name = user.Name,
                };

                return userDTO;
            }
            catch
            {
                return null!;
            }
        }
        public void Delete(string name)
        {
            if (name is null) return;

            var users = _dbContext.Users
                .AsNoTracking()
                .ToList();

            UserModel user = users.First(user => user.Name == name);

            _dbContext.Remove(user);
            _dbContext.SaveChanges();
        }
    }
}
