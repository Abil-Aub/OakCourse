using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IHostEnvironment _host;

        public UserController(IHostEnvironment host)
        {
            _host = host;
        }

        UserBLL bll = new UserBLL();
        
        // Get: Admin/User
        public IActionResult AddUser()
        {
            UserDTO model = new UserDTO();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddUser(UserDTO model)
        {
            if(model.UserImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                string filename = "";
                IFormFile postedfile = model.UserImage;
                Bitmap UserImage = new Bitmap(postedfile.OpenReadStream());
                Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedfile.FileName;
                    resizeImage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/UserImages/" + filename));
                    model.ImagePath = filename;
                    bll.AddUser(model);
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    model = new UserDTO();
                    ModelState.Clear();
                }
                else
                    ViewBag.ProcessState = General.Messages.ExtensionError;

            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }

        public IActionResult UserList()
        {
            List<UserDTO> model = new List<UserDTO>();
            model = bll.GetUsers();
            return View(model);
        }

        public IActionResult UpdateUser(int ID)
        {
            UserDTO dto = new UserDTO();
            dto = bll.GetUserWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public IActionResult UpdateUser(UserDTO model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.UserImage != null)
                {
                    string filename = "";
                    IFormFile postedfile = model.UserImage;
                    Bitmap UserImage = new Bitmap(postedfile.OpenReadStream());
                    Bitmap resizeImage = new Bitmap(UserImage, 128, 128);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;
                        resizeImage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/UserImages/" + filename));
                        model.ImagePath = filename;
                    }
                    
                }
                string oldImagePath = bll.UpdateUser(model);
                if(model.UserImage != null)
                {
                    var oldfile = Path.Combine(_host.ContentRootPath, "wwwroot/UserImages/" + oldImagePath);
                    if (System.IO.File.Exists(oldfile))
                    {
                        System.IO.File.Delete(oldfile);
                    }
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
            }
            return View(model);
        }
    }
}
