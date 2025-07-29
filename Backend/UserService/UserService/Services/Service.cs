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

            if (_dbContext.Users.Any(u => u.Name == userDTO.name))
                return -1;

            var model = new UserModel
            {
                Name = userDTO.name,
                Email = userDTO.email,
                Password = userDTO.password,
                Level = 1
            };

            if (string.IsNullOrEmpty(model.Name))
                return -2;

            _dbContext.Users.Add(model);
            _dbContext.SaveChanges(); // Jetzt hat model.Id einen echten Wert

            var response = await _requestService.CreateBankAccount(model.Id);

            if (response == 0)
                return -3;

            return model.Id;
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
                var user = _dbContext.Users.FirstOrDefault(u =>
                    (u.Name == uName || u.Email == uName) && u.Password == uPassword);
                if (user == null) return null!;

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
