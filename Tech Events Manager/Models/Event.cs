using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech_Events_Manager.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter event organiser")]
        [StringLength(EventConstants.MAX_ORGANISER_NAME_LENGTH)]
        public string Organiser { get; set; }

        [Required(ErrorMessage = "Enter event city")]
        [StringLength(EventConstants.MAX_CITY_LENGTH)]
        public string City { get; set; }

        [Required(ErrorMessage ="Enter event date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MMM/yyyy}", ApplyFormatInEditMode =true)]
        public DateTime Date { get; set; }

        [DisplayName("Image Title")]
        [Required(ErrorMessage ="Enter image title")]
        [StringLength(EventConstants.MAX_IMAGE_TITLE_LENGTH)]
        public string ImageTitle { get; set; }

        [DisplayName("Upload Image")]
        [Required(ErrorMessage ="Choose an image to upload")]
        [StringLength(EventConstants.MAX_IMAGE_PATH_LENGTH)]
        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [DataType(DataType.Url)]
        [Required(ErrorMessage ="Enter a valid website address")]
        public string Website { get; set; }

        [Required(ErrorMessage ="Enter a valid postcode")]
        [RegularExpression("^([A-Z]{1,2})([0-9][0-9A-Z]?) ([0-9])([ABDEFGHJLNPQRSTUWXYZ]{2})$")]
        public string Postcode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }


    }
}
