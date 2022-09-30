using DTO;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SocialMediaController : Controller
    {
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
            SocialMediaDTO newmodel = new SocialMediaDTO();
            return View(newmodel);
        }
    }
}
