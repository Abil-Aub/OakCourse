using DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public int AddCategory(Category cat)
        {
            try
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return cat.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<CategoryDTO> GetCategoryList()
        {
            try
            {
                List<Category> list = db.Categories.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
                List<CategoryDTO> dtolist = new List<CategoryDTO>();
                foreach (var item in list)
                {
                    CategoryDTO dto = new CategoryDTO();
                    dto.ID = item.ID;
                    dto.CategoryName = item.CategoryName;
                    dtolist.Add(dto);
                }
                return dtolist;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static IEnumerable<SelectListItem> GetCategoriesForDropdown()
        {
            IEnumerable<SelectListItem> categoryList = db.Categories.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).Select(x => new SelectListItem()
            {
                Text = x.CategoryName,
                Value = SqlFunctions.StringConvert((double)x.ID)
            }).ToList();
            return categoryList;
        }

        public void UpdateCategory(CategoryDTO model)
        {
            try
            {
                Category cat = db.Categories.First(x => x.ID == model.ID);
                cat.CategoryName = model.CategoryName;
                cat.LastUpdateDate = DateTime.Now;
                cat.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            try
            {
                Category cat = db.Categories.First(x => x.ID == ID);
                CategoryDTO dto = new CategoryDTO();
                dto.ID = cat.ID;
                dto.CategoryName= cat.CategoryName;
                return dto;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
