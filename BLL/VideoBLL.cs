using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VideoBLL
    {
        VideoDAO dao = new VideoDAO();
        public bool AddVideo(VideoDTO model)
        {
            Video video = new Video();
            video.Ttitle = model.Title;
            video.VideaPath = model.VideoPath;
            video.OriginalVideoPath = model.OriginalVideoPath;
            video.AddDate = DateTime.Now;
            video.LastUpdateDate = DateTime.Now;
            video.LastUpdateUserID = UserStatic.UserID;
            int ID = dao.AddVideo(video);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.VideoAdd, General.TableName.Video, ID, IPAddress);

            return true;
        }

        public List<VideoDTO> GetVideos()
        {
            return dao.GetVideos();
        }

        public VideoDTO GetVideoWithID(int ID)
        {
            return dao.GetVideoWithID(ID);
        }

        public bool UpdateVideo(VideoDTO model)
        {
            dao.UpdateVideo(model);
            string IPAddress = "127.0.0.1";
            LogDAO.AddLog(General.ProcessType.UserUpdate, General.TableName.User, model.ID, IPAddress);
            return true;
        }
    }
}
