using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class UserBLL
    {
        UserDAO userdao = new UserDAO();
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            dto = userdao.GetUserWithUsernameAndPassword(model);
            return dto;
        }

        public void AddUser(UserDTO model)
        {
            T_User user = new T_User();
            user.UserName = model.Username;
            user.Password = model.Password;
            user.Email = model.Email;
            user.ImagePath = model.ImagePath;
            user.NameSurname = model.Name;
            user.isAdmin = model.isAdmin;
            user.AddDate = DateTime.Now;
            user.LastUpdateDate = DateTime.Now;
            user.LastUpdateUserID = UserStatic.UserID;
            user.isDeleted = false;
            int ID = userdao.AddUser(user);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.UserAdd, General.TableName.User, ID, IPAddress);
        }
    }
}
