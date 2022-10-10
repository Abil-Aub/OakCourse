using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class VideoDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please fill title area")]
        public string Title { get; set; }
        public string VideoPath { get; set; }
        [Required(ErrorMessage ="Please fill path area")]
        public string OriginalVideoPath { get; set; }
        public DateTime AddDate { get; set; }
    }
}
