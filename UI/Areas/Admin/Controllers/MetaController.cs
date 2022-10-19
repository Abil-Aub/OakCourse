using BLL;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MetaController : Controller
    {
        // GET: Admin/Meta
        MetaBLL bll = new MetaBLL();
        public IActionResult AddMeta()
        {
            MetaDTO dto = new MetaDTO();
            return View(dto);
        }
        [HttpPost]
        public IActionResult AddMeta(MetaDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.AddMeta(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
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
            MetaDTO newmodel = new MetaDTO();
            return View(newmodel);
        }

        public IActionResult MetaList()
        {
            List<MetaDTO> model = new List<MetaDTO>();
            model = bll.GetMetaData();
            return View(model);
        }

        public IActionResult UpdateMeta(int ID)
        {
            MetaDTO model = new MetaDTO();
            model = bll.GetMetaWithID(ID);
            return View(model);
        }
        [HttpPost]
        public IActionResult UpdateMeta(MetaDTO model)
        {
            if(ModelState.IsValid)
            {
                if (bll.UpdateMeta(model))
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

        public JsonResult DeleteMeta(int ID)
        {
            bll.DeleteMeta(ID);
            return Json("");
        }
    }
}
