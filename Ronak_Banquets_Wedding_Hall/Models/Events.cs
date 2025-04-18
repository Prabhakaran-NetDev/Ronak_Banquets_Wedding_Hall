using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ronak_Banquets_Wedding_Hall.Models
{
    public partial class tbl_events
    {
        [Key]
        public int event_id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Event Name")]
        public string event_name { get; set; }

        [Required]
        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime event_date { get; set; }

        [StringLength(100)]
        [Display(Name = "Event Venue")]
        public string event_venue { get; set; }

        public int? created_by { get; set; }

        [StringLength(30)]
        public string payment { get; set; }

        [StringLength(30)]
        [Display(Name = "Event Status")]
        public string event_status { get; set; }
    }
}