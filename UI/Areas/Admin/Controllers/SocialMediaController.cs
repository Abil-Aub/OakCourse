using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
        private readonly IHostEnvironment _host;

        public SocialMediaController(IHostEnvironment host)
        {
            _host = host;
        }

        SocialMediaBLL bll = new SocialMediaBLL();
        // GET: Admin/SocialMedia
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddSocialMedia()
        {
            SocialMediaDTO model = new SocialMediaDTO();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddSocialMedia(SocialMediaDTO model)
        {
            if(model.SocialImage == null)
            {
                ViewBag.ProcessState = General.Messages.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                IFormFile postedfile = model.SocialImage;
                Bitmap SocialMedia = new Bitmap(postedfile.OpenReadStream());
                string ext = Path.GetExtension(postedfile.FileName);
                string filename = "";
                if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                {
                    string uniquenumber = Guid.NewGuid().ToString();
                    filename = uniquenumber + postedfile.FileName;
                    SocialMedia.Save(Path.Combine(_host.ContentRootPath, "wwwroot/SocialMediaImages/" +  filename));
                    model.ImagePath = filename;
                    if (bll.AddSocialMedia(model))
                    {
                        ViewBag.ProcessState = General.Messages.AddSuccess;
                        model = new SocialMediaDTO();
                        ModelState.Clear();
                    }
                    else
                        ViewBag.ProcessState = General.Messages.GeneralError;
                }
                else
                    ViewBag.ProcessState = General.Messages.ExtensionError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            // SocialMediaDTO newmodel = new SocialMediaDTO();
            return View(model);
        }

        public IActionResult SocialMediaList()
        {
            List<SocialMediaDTO> dtolist = new List<SocialMediaDTO>();
            dtolist = bll.GetSocialMedias();

            return View(dtolist);
        }
        public IActionResult UpdateSocialMedia(int ID)
        {
            SocialMediaDTO dto = bll.GetSocialMediaWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public IActionResult UpdateSocialMedia(SocialMediaDTO model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            else
            {
                if (model.SocialImage != null)
                {
                    IFormFile postedfile = model.SocialImage;
                    Bitmap SocialMedia = new Bitmap(postedfile.OpenReadStream());
                    string ext = Path.GetExtension(postedfile.FileName);
                    string filename = "";
                    if (ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".gif")
                    {
                        string uniquenumber = Guid.NewGuid().ToString();
                        filename = uniquenumber + postedfile.FileName;
                        SocialMedia.Save(Path.Combine(_host.ContentRootPath, "wwwroot/SocialMediaImages/" + filename));
                        model.ImagePath = filename;
                    }
                }
                string oldImagePath = bll.UpdateSocialMedia(model);
                if(model.SocialImage != null)
                {
                    var oldfile = Path.Combine(_host.ContentRootPath, "wwwroot/SocialMediaImages/" + oldImagePath);
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
