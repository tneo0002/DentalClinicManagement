using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DentalClinicManagementSystem.Models
{
    public class Image
    {
        public int ImageId { get; set; }

        [Required(ErrorMessage = "Image required. Upload a .png, .gif, .jpg, or .pdf file.")]
        public string ImagePath { get; set; }

        [Required(ErrorMessage = "Image name required.")]
        public string ImageName { get; set; }

        public string UserId { get; set; }
    }
}