using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AdsBLL
    {
        AdsDAO dao = new AdsDAO();
        public void AddAds(AdsDTO model)
        {
            Ad ads = new Ad();
            ads.Name = model.Name;
            ads.Link = model.Link;
            ads.ImagePath = model.ImagePath;
            ads.Size = model.Imagesize;
            ads.AddDate = DateTime.Now;
            ads.LastUpdateUserID = UserStatic.UserID;
            ads.LastUpdateDate = DateTime.Now;
            int ID = dao.AddAds(ads);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.AdsAdd, General.TableName.Ads, ID, IPAddress);
        }

        public List<AdsDTO> GetAds()
        {
            return dao.GetAds();
        }

        public AdsDTO GetAdsWithID(int ID)
        {
            return dao.GetAdsWithID(ID);
        }

        public string UpdateAds(AdsDTO model)
        {
            string oldImagePath = dao.UpdateAds(model);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID, IPAddress);
            return oldImagePath;
        }
    }
}
