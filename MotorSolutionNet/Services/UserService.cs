using System;
using MotorSolutionNet.Data;
using MotorSolutionNet.DTO;
using MotorSolutionNet.Models;
using MotorSolutionNet.Utilities;


namespace MotorSolutionNet.Services
{
    public class UserService 
    {
        private readonly UserData _userData;
        private readonly PaginationHelper _userPagination;
        public UserService()
        {
            _userData = new UserData();
            _userPagination = new PaginationHelper();
        }
        public bool AddUser(User user)
        {
            bool isValid = ConfigManager.IsObjectValid(user);
            if (!isValid)
            {
                throw new Exception("El usuario tiene campos Vacios");

            }
            return _userData.AddUser(user);
        }
        public User GetUser(int? userId = null, string userEmail = null, string userIdentification = null, int? companyCode = null)
        { return _userData.GetUser(userId, userEmail, userIdentification, companyCode); }

        public bool UpdateUser(UserDTO user)
        {
            bool isValid = ConfigManager.IsObjectValid(user);
           
            if (!isValid)
            {
                throw new Exception("El usuario tiene campos Vacios");

            }
             return _userData.UpdateUser(user);
        }

        public bool DeleteUser(int userId)
        { return _userData.DeleteUser(userId); }

        public User GetUserById(int? userId)
        { return _userData.GetUserById(userId); }
        public Object GetUserByCompany(int companyCode, int pageIndex, int pageSize)
        { var users = _userData.GetUserByCompany(companyCode);
          return _userPagination.Paginate(users, pageIndex, pageSize);
        }
        public RoleOptionsResult GetRoleOptions()
        { return _userData.GetRoleOptions(); }

       
    }
    
}