using Microsoft.AspNetCore.Mvc;
using BLL;
using DTO;
using System.Net;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        UserBLL userbll = new UserBLL();
        // GET: Admin/Login
        public IActionResult Index()
        {
            UserDTO dto = new UserDTO();
            return View(dto);
        }
        [HttpPost]
        public IActionResult Index(UserDTO model)
        {
            if (ModelState.IsValid)
            {
                UserDTO user = userbll.GetUserWithUsernameAndPassword(model);
                if(user.ID!=0)
                {
                    UserStatic.UserID = user.ID;
                    UserStatic.isAdmin = user.isAdmin;
                    UserStatic.NameSurname = user.Name;
                    UserStatic.ImagePath = user.ImagePath;
                    // var addlist = Dns.GetHostEntry(Dns.GetHostName());
                    string IPAddress = "127.0.0.1"; // addlist.AddressList[1].ToString(); // addlist.AddressList[3].ToString();
                    LogBLL.AddLog(General.ProcessType.Login, General.TableName.Login, 12, IPAddress);
                    return RedirectToAction("Index", "Post");
                }
                else
                    return View(model);
            }
            else
                return View(model);
            
        }
    }
}
