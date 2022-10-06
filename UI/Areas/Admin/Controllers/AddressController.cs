using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AddressController : Controller
    {
        AddressBLL bll = new AddressBLL();
        // GET Admin/Address
        public IActionResult AddressList()
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddresses();
            return View(list);
        }
        public IActionResult AddAddress()
        {
            AddressDTO dto = new AddressDTO();
            return View(dto);
        }
        [HttpPost]
        public IActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
        public IActionResult UpdateAddress(int ID)
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddresses();
            AddressDTO dto = list.First(x => x.ID == ID);
            return View(dto);
        }
        [HttpPost]
        public IActionResult UpdateAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateAddress(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }
    }
}
