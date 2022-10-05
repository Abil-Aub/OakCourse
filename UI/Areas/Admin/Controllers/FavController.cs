using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FavController : Controller
    {
        private readonly IHostEnvironment _host;

        public FavController(IHostEnvironment host)
        {
            _host = host;
        }

        // GET: Admin/Fav
        FavBLL bll = new FavBLL();
        public IActionResult UpdateFav()
        {
            FavDTO dto = new FavDTO();
            dto = bll.GetFav();
            return View(dto);
        }
        [HttpPost]
        public IActionResult UpdateFav(FavDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.FavImage != null)
                {
                    string favname = "";
                    IFormFile postedfilefav = model.FavImage;
                    Bitmap FavImage = new Bitmap(postedfilefav.OpenReadStream());
                    Bitmap resizefavImage = new Bitmap(FavImage, 100, 100);
                    string ext = Path.GetExtension(postedfilefav.FileName);
                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string favunique = Guid.NewGuid().ToString();
                        favname = favunique + postedfilefav.FileName;
                        resizefavImage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/FavImages/" + favname));
                        model.Fav = favname;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;

                }
                if (model.LogoImage != null)
                {
                    string logoname = "";
                    IFormFile postedfilelogo = model.LogoImage;
                    Bitmap LogoImage = new Bitmap(postedfilelogo.OpenReadStream());
                    Bitmap resizelogoImage = new Bitmap(LogoImage, 100, 100);
                    string ext = Path.GetExtension(postedfilelogo.FileName);
                    if (ext == ".ico" || ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string logounique = Guid.NewGuid().ToString();
                        logoname = logounique + postedfilelogo.FileName;
                        resizelogoImage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/FavImages/" + logoname));
                        model.Logo = logoname;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;

                }

                FavDTO returndto = new FavDTO();
                returndto = bll.UpdateFav(model);

                if(model.FavImage != null)
                {
                    var oldfile = Path.Combine(_host.ContentRootPath, "wwwroot/FavImages/" + returndto.Fav);
                    if (System.IO.File.Exists(oldfile))
                    {
                        System.IO.File.Delete(oldfile);
                    }
                }
                if (model.LogoImage != null)
                {
                    var oldfile = Path.Combine(_host.ContentRootPath, "wwwroot/FavImages/" + returndto.Logo);
                    if (System.IO.File.Exists(oldfile))
                    {
                        System.IO.File.Delete(oldfile);
                    }
                }
                ViewBag.ProcessState = General.Messages.UpdateSuccess;
            }
            return View(model);
        }
    }
}
