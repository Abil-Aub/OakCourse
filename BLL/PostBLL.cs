using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class PostBLL
    {
        PostDAO dao = new PostDAO();
        public bool AddPost(PostDTO model)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.PostContent = model.PostContent;
            post.ShortContent = model.ShortContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;
            post.SeoLink = SeoLink.GenerateUrl(model.Title);
            post.LanguageName = model.Language;
            post.AddDate = DateTime.Now;
            post.AddUserID = UserStatic.UserID;
            post.LastUpdateUserID = UserStatic.UserID;
            post.LastUpdateDate = DateTime.Now;
            int ID = dao.AddPost(post);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.PostAdd, General.TableName.Post, ID, IPAddress) ;
            SavePostImage(model.PostImages, ID);
            AddTag(model.TagText, ID);
            return true;
        }

        public List<PostDTO> GetPosts()
        {
            return dao.GetPosts();
        }

        private void AddTag(string tagText, int PostID)
        {
            string[] tags;
            tags = tagText.Split(',');
            List<PostTag> taglist = new List<PostTag>();
            foreach (var item in tags)
            {
                PostTag tag = new PostTag();
                tag.PostID = PostID;
                tag.TagContent = item;
                tag.AddDate = DateTime.Now;
                tag.LastUpdateDate = DateTime.Now;
                tag.LastUpdateUserID= UserStatic.UserID;
                taglist.Add(tag);
            }
            foreach (var item in taglist)
            {
                int tagID = dao.AddTag(item);
                string IPAddress = "127.0.0.1";
                LogDAO.AddLog(General.ProcessType.TagAdd, General.TableName.Tag, tagID, IPAddress);
            }
        }
        void SavePostImage(List<PostImageDTO> list, int PostID)
        {
            List<PostImage> imagelist = new List<PostImage>();
            foreach (var item in list)
            {
                PostImage image = new PostImage();
                image.PostID = PostID;
                image.ImagePath = item.ImagePath;
                image.AddDate = DateTime.Now;
                image.LastUpdateDate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserID;
                imagelist.Add(image);
            }
            foreach (var item in imagelist)
            {
                int imageID = dao.AddImage(item);
                string IPAddress = "127.0.0.1";
                LogDAO.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID, IPAddress);
            }
        }

        public PostDTO GetPostWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            dto = dao.GetPostWithID(ID);
            dto.PostImages = dao.GetPostImagesWithPostID(ID);
            List<PostTag> taglist = dao.GetPostTagsWithPostID(ID);
            string tagvalue = "";
            foreach (var item in taglist)
            {
                tagvalue += item.TagContent;
                tagvalue += ",";
            }
            dto.TagText = tagvalue;
            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.PostUpdate, General.TableName.Post, model.ID, IPAddress);
            if (model.PostImages != null)
                SavePostImage(model.PostImages, model.ID);
            dao.DeleteTags(model.ID);
            AddTag(model.TagText, model.ID);
            return true;
        }
    }
}
