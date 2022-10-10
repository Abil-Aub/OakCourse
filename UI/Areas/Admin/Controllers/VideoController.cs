using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VideoController : Controller
    {
        VideoBLL bll = new VideoBLL();
        // GET: Admin/Video
        public IActionResult VideoList()
        {
            List<VideoDTO> list = new List<VideoDTO>();
            list = bll.GetVideos();
            return View(list);
        }
        public IActionResult AddVideo()
        {
            VideoDTO dto = new VideoDTO();
            return View(dto);
        }
        [HttpPost]
        public IActionResult AddVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = String.Format(@"<iframe width=""300"" height=""200"" src=""{0}"" frameborder = ""0"" allowfullscreen ></iframe>", mergelink);
                if (bll.AddVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
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

        public IActionResult UpdateVideo(int ID)
        {
            VideoDTO dto = new VideoDTO();
            dto = bll.GetVideoWithID(ID);
            return View(dto);
        }
        [HttpPost]
        public IActionResult UpdateVideo(VideoDTO model)
        {
            if (ModelState.IsValid)
            {
                string path = model.OriginalVideoPath.Substring(32);
                string mergelink = "https://www.youtube.com/embed/";
                mergelink += path;
                model.VideoPath = String.Format(@"<iframe width=""300"" height=""200"" src=""{0}"" frameborder = ""0"" allowfullscreen ></iframe>", mergelink);
                if (bll.UpdateVideo(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                    ModelState.Clear();
                    model = new VideoDTO();
                }
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            return View(model);
        }
    }
}
