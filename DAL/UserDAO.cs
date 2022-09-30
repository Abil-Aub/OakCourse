using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL
{
    public class UserDAO : PostContext
    {
        public UserDTO GetUserWithUsernameAndPassword(UserDTO model)
        {
            UserDTO dto = new UserDTO();
            try
            {
                T_User user = db.T_User.FirstOrDefault(x => x.UserName == model.Username && x.Password == model.Password);
                if (user != null && user.ID != 0)
                {
                    dto.ID = user.ID;
                    dto.Username = user.UserName;
                    dto.Name = user.NameSurname;
                    dto.ImagePath = user.ImagePath;
                    dto.isAdmin = user.isAdmin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            
            return dto;
        }
    }
}
