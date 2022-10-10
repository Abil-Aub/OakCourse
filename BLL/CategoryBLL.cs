using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CategoryBLL
    {
        CategoryDAO dao = new CategoryDAO();
        public bool AddCategory(CategoryDTO model)
        {
            Category cat = new Category();
            cat.CategoryName = model.CategoryName;
            cat.AddDate = DateTime.Now;
            cat.LastUpdateUserID = UserStatic.UserID;
            cat.LastUpdateDate = DateTime.Now;
            int CatID = dao.AddCategory(cat);

            string IPAddress = "127.0.0.1"; ////////// TO-DO ////////////
            LogDAO.AddLog(General.ProcessType.CategoryAdd, General.TableName.Category, CatID, IPAddress);
            return true;
        }

        public List<CategoryDTO> GetCategoryList()
        {
            return dao.GetCategoryList();
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            return dao.GetCategoryWithID(ID);
        }

        public bool UpdateCategory(CategoryDTO model)
        {
            dao.UpdateCategory(model);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.CategoryUpdate, General.TableName.Category, model.ID, IPAddress);
            return true;
        }
    }
}
