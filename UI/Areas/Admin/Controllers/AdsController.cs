using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdsController : Controller
    {
        private readonly IHostEnvironment _host;

        public AdsController(IHostEnvironment host)
        {
            _host = host;
        }

        AdsBLL bll = new AdsBLL();
        // GET: Admin/Ads
        public IActionResult AddAds()
        {
            AdsDTO dto = new AdsDTO();
            return View(dto);
        }
        [HttpPost]
        public IActionResult AddAds(AdsDTO model)
        {
            if (model.AdsImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if (ModelState.IsValid)
            {
                IFormFile postedfile = model.AdsImage;
                Bitmap AdsImage = new Bitmap(postedfile.OpenReadStream());
                Bitmap resizeImage = new Bitmap(AdsImage, 128, 128);
                string ext = Path.GetExtension(postedfile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedfile.FileName;
                    resizeImage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/AdsImages/" + filename));
                    model.ImagePath = filename;
                    bll.AddAds(model);
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    model = new AdsDTO();
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

        public IActionResult AdsList()
        {
            List<AdsDTO> Adslist = new List<AdsDTO>();
            Adslist = bll.GetAds();
            return View(Adslist);
        }

        public IActionResult UpdateAds(int ID)
        {
            AdsDTO dto = bll.GetAdsWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public IActionResult UpdateAds(AdsDTO model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.AdsImage != null)
                {
                    string filename = "";
                    IFormFile postedfile = model.AdsImage;
                    Bitmap AdsImage = new Bitmap(postedfile.OpenReadStream());
                    Bitmap resizeImage = new Bitmap(AdsImage, 128, 128);
                    string ext = Path.GetExtension(postedfile.FileName);
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;
                        resizeImage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/AdsImages/" + filename));
                        model.ImagePath = filename;
                    }
                    else
                        ViewBag.ProcessState = General.Messages.ExtensionError;

                }
                string oldImagePath = bll.UpdateAds(model);
                if(model.AdsImage != null)
                {
                    var oldfile = Path.Combine(_host.ContentRootPath, "wwwroot/AdsImages/" + oldImagePath);
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
