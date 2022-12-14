using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SocialMediaBLL
    {
        SocialMediaDAO dao = new SocialMediaDAO();
        public bool AddSocialMedia(SocialMediaDTO model)
        {
            SocialMedia social = new SocialMedia();
            social.Name = model.Name;
            social.Link = model.Link;
            social.ImagePath = model.ImagePath;
            social.AddDate = DateTime.Now;
            social.LastUpdateUserID = UserStatic.UserID;
            social.LastUpdateDate = DateTime.Now;
            int ID = dao.AddSocialMedia(social);
            string IPAddress = "127.0.0.1"; //////////////////////// TO-DO /////////////////
            LogDAO.AddLog(General.ProcessType.SocialAdd, General.TableName.Social, ID, IPAddress);

            return true;
        }

        public List<SocialMediaDTO> GetSocialMedias()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = dao.GetSocialMedias();
            return dtolist;
        }

        public SocialMediaDTO GetSocialMediaWithID(int ID)
        {
            SocialMediaDTO dto = dao.GetSocialMediaWithID(ID);
            return dto;
        }

        public string UpdateSocialMedia(SocialMediaDTO model)
        {
            string oldImagePath = dao.UpdateSocialMedia(model);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.SocialUpdate, General.TableName.Social, model.ID, IPAddress);
            return oldImagePath;
        }
    }
}
