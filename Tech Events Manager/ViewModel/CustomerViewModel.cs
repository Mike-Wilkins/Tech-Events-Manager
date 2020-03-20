using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Web;
using Tech_Events_Manager.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tech_Events_Manager.ViewModel
{
    public class CustomerViewModel
    {
       
        [Required(ErrorMessage = "Enter a valid UK postcode")]
       // [RegularExpression("(^gir\\s?0aa$)|(^[a-z-[qvx]](\\d{1,2}|[a-hk-y]\\d{1,2}|\\d[a-hjks-uw]|[a-hk-y]\\d[abehmnprv-y])\\s?\\d[a-z-[cikmov]]{2}$)", ErrorMessage = "Postcode must be UK")]
        [RegularExpression("^([A-Z]{1,2})([0-9][0-9A-Z]?) ([0-9])([ABDEFGHJLNPQRSTUWXYZ]{2})$", ErrorMessage ="Uk Postcode")]
        
        public string UserPostcode { get; set; }
        public double Distance { get; set; }

        public double UserLat { get; set; }
        public double UserLng { get; set; }
        public IEnumerable<Event> Event { get; set; }

      
       
    }
   
 
}
