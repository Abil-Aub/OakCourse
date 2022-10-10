using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        CategoryBLL bll = new CategoryBLL();
        // Get: Admin/Category
        public IActionResult CategoryList()
        {
            List<CategoryDTO> model = new List<CategoryDTO>();
            model = bll.GetCategoryList();
            return View(model);
        }
        public IActionResult AddCategory()
        {
            CategoryDTO dto = new CategoryDTO();
            return View(dto);
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddCategory(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new CategoryDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            
            return View(model);
        }
        public IActionResult UpdateCategory(int ID)
        {
            CategoryDTO model = new CategoryDTO();
            model = bll.GetCategoryWithID(ID);
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateCategory(CategoryDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateCategory(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new CategoryDTO();
                }
                else
                {
                    ViewBag.ProcessState = General.Messages.GeneralError;
                }
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }

            return View(model);
        }
    }
}
