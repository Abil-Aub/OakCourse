using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AdsDTO
    {
        public int ID { get; set; }
        [Required(ErrorMessage ="Please fill the Name area")]
        public string Name { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please fill the Link area")]
        public string Link { get; set; }
        [Required(ErrorMessage = "Please fill the Imagesize area")]
        public string Imagesize { get; set; }
        [Display(Name ="Ads Image")]
        public IFormFile AdsImage { get; set; }
    }
}
