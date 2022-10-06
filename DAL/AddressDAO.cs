using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class AddressDAO : PostContext
    {
        public int AddAddress(Address adrs)
        {
            try
            {
                db.Addresses.Add(adrs);
                db.SaveChanges();
                return adrs.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<AddressDTO> GetAddresses()
        {
            List<Address> list = db.Addresses.Where(x => x.isDeleted == false).OrderBy(x => x.AddDate).ToList();
            List<AddressDTO> dtolist = new List<AddressDTO>();
            foreach (var item in list)
            {
                AddressDTO dto = new AddressDTO();
                dto.ID = item.ID;
                dto.AddressContent = item.Address1;
                dto.Email = item.Email;
                dto.Fax = item.Fax;
                dto.Phone = item.Phone;
                dto.Phone2 = item.Phone2;
                dto.LargeMapPath = item.MapPathLarge;
                dto.SmallMapPath = item.MapPathSmall;
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public void UpdateAddress(AddressDTO model)
        {
            try
            {
                Address adrs = db.Addresses.First(x => x.ID == model.ID);
                adrs.Address1 = model.AddressContent;
                adrs.Email = model.Email;
                adrs.Fax = model.Fax;
                adrs.Phone = model.Phone;
                adrs.Phone2 = model.Phone2;
                adrs.MapPathLarge = model.LargeMapPath;
                adrs.MapPathSmall = model.SmallMapPath;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
