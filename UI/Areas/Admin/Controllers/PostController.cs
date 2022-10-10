using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly IHostEnvironment _host;

        public PostController(IHostEnvironment host)
        {
            _host = host;
        }
        PostBLL bll = new PostBLL();
        // GET: Admin/Post
        public IActionResult PostList()
        {
            List<PostDTO> list = new List<PostDTO>();
            list = bll.GetPosts();
            return View(list);
        }
        public IActionResult AddPost()
        {
            PostDTO model = new PostDTO();
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }
        [HttpPost]
        public IActionResult AddPost(PostDTO model)
        {
            if(model.PostImage == null)
            {
                ViewBag.processState = General.Messages.ImageMissing;
            }
            else if(ModelState.IsValid)
            {
                foreach (var item in model.PostImage)
                {
                    Bitmap image = new Bitmap(item.OpenReadStream());
                    string ext = Path.GetExtension(item.FileName);
                    if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".gif")
                    {
                        ViewBag.ProcessState = General.Messages.ExtensionError;
                        model.Categories = CategoryBLL.GetCategoriesForDropdown();
                        return View(model);
                    }
                }
                List<PostImageDTO> imagelist = new List<PostImageDTO>();
                foreach (var postedfile in model.PostImage)
                {
                    Bitmap image = new Bitmap(postedfile.OpenReadStream());
                    Bitmap resizeimage = new Bitmap(image, 750, 422);
                    string filename = "";
                    string uniqueNumber = Guid.NewGuid().ToString();
                    filename = uniqueNumber + postedfile.FileName;
                    resizeimage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/PostImages/" + filename));
                    PostImageDTO dto = new PostImageDTO();
                    dto.ImagePath = filename;
                    imagelist.Add(dto);
                }
                model.PostImages = imagelist;
                if (bll.AddPost(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new PostDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            return View(model);
        }

        public IActionResult UpdatePost(int ID)
        {
            PostDTO model = new PostDTO();
            model = bll.GetPostWithID(ID);
            model.Categories = CategoryBLL.GetCategoriesForDropdown();
            model.isUpdate = true;
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdatePost(PostDTO model)
        {
            IEnumerable<SelectListItem> selectlist = CategoryBLL.GetCategoriesForDropdown();
            if (ModelState.IsValid)
            {
                if(model.PostImage != null)
                {
                    foreach (var item in model.PostImage)
                    {
                        Bitmap image = new Bitmap(item.OpenReadStream());
                        string ext = Path.GetExtension(item.FileName);
                        if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".gif")
                        {
                            ViewBag.ProcessState = General.Messages.ExtensionError;
                            model.Categories = CategoryBLL.GetCategoriesForDropdown();
                            return View(model);
                        }
                    }
                    List<PostImageDTO> imagelist = new List<PostImageDTO>();
                    foreach (var postedfile in model.PostImage)
                    {
                        Bitmap image = new Bitmap(postedfile.OpenReadStream());
                        Bitmap resizeimage = new Bitmap(image, 750, 422);
                        string filename = "";
                        string uniqueNumber = Guid.NewGuid().ToString();
                        filename = uniqueNumber + postedfile.FileName;
                        resizeimage.Save(Path.Combine(_host.ContentRootPath, "wwwroot/PostImages/" + filename));
                        PostImageDTO dto = new PostImageDTO();
                        dto.ImagePath = filename;
                        imagelist.Add(dto);
                    }
                    model.PostImages = imagelist;
                }

                if (bll.UpdatePost(model))
                {
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
                ViewBag.ProcessState = General.Messages.EmptyArea;
            model = bll.GetPostWithID(model.ID);
            model.Categories = selectlist;
            model.isUpdate=true;
            return View(model);
        }
    }
}
