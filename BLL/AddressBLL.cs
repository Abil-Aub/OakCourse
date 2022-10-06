using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class AddressBLL
    {
        AddressDAO dao = new AddressDAO();
        public bool AddAddress(AddressDTO model)
        {
            Address adrs = new Address();
            adrs.Address1 = model.AddressContent;
            adrs.Email = model.Email;
            adrs.Phone = model.Phone;
            adrs.Phone2 = model.Phone2;
            adrs.Fax = model.Fax;
            adrs.MapPathLarge = model.LargeMapPath;
            adrs.MapPathSmall = model.SmallMapPath;
            adrs.AddDate = DateTime.Now;
            adrs.LastUpdateDate = DateTime.Now;
            adrs.LastUpdateUserID = UserStatic.UserID;
            int ID = dao.AddAddress(adrs);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.AddressAdd, General.TableName.Address, ID, IPAddress);
            return true;
        }

        public List<AddressDTO> GetAddresses()
        {
            return dao.GetAddresses();
        }

        public bool UpdateAddress(AddressDTO model)
        {
            dao.UpdateAddress(model);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.AddressUpdate, General.TableName.Address, model.ID, IPAddress);
            return true;
        }
    }
}
