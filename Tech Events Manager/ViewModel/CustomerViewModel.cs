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
        [Required(ErrorMessage = "Enter a valid postcode")]
        public string UserPostcode { get; set; }
        public double Distance { get; set; }

        public double UserLat { get; set; }
        public double UserLng { get; set; }
        public IEnumerable<Event> Event { get; set; }

    }
}
