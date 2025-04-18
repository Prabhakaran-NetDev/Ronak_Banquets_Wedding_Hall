using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;
using System.Web.Mvc;

namespace Ronak_Banquets_Wedding_Hall.Models
{
    public class adminViewModel 
    {
        public IEnumerable<tbl_events> Events { get; set; }
        public IEnumerable<tbl_bookings> Bookings { get; set; }
        public IEnumerable<tbl_resources> Resources { get; set; }
        public IEnumerable<tbl_event_resources> Events_Resources { get; set; }
        public IEnumerable<tbl_reports> Reports { get; set; }
        public IEnumerable<tbl_notifications> Notifications { get; set; }
        public IEnumerable<SP_events_bookings> Events_bookings { get; set; }
        public IEnumerable<SP_events_bookings_users> Events_bookings_users { get; set; }
        public IEnumerable<SP_event_resources> Events_Resources_Select { get; set; }
    }
    
    public partial class tbl_bookings
    {
        [Key]
        public int booking_id { get; set; }

        public int? event_id { get; set; }

        public int? user_id { get; set; }

        [Column(TypeName = "date")]
        [DataType(DataType.Date)]
        public DateTime booking_date { get; set; }

        [StringLength(30)]
        public string booking_status { get; set; }
    }
    public partial class tbl_resources
    {
        [Key]
        public int resource_id { get; set; }

        [Required]
        [StringLength(100)]
        public string resource_name { get; set; }

        public bool resource_availability { get; set; }
    }
    public partial class tbl_event_resources
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int event_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int resource_id { get; set; }

        public virtual tbl_resources tbl_resources { get; set; }
    }
    public partial class tbl_reports
    {
        [Key]
        public int report_id { get; set; }

        public int? event_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime? generated_at { get; set; }

        [Required]
        public string details { get; set; }

        public int? attentess_count { get; set; }
    }
    public partial class tbl_notifications
    {
        [Key]
        public int notification_id { get; set; }

        public int? user_id { get; set; }

        [Required]
        public string message { get; set; }

        public bool is_read { get; set; }
    }
    public partial class SP_events_bookings
    {
        [Key]
        public int event_id { get; set; }

        public string event_name { get; set; }

        [DataType(DataType.Date)]
        public DateTime event_date { get; set; }

        public string event_venue { get; set; }

        public int created_by { get; set; }

        public string payment { get; set; }

        public string event_status { get; set; }

        public int booking_id { get; set; }

        [DataType(DataType.Date)]
        public DateTime booking_date { get; set; }
    }
    public partial class SP_events_bookings_users
    {
        [Key]
        public int created_by { get; set; }
        public string event_name { get; set; }

        [DataType(DataType.Date)]
        public DateTime event_date { get; set; }

        public string event_venue { get; set; }

        public string payment { get; set; }

        public string event_status { get; set; }

        [DataType(DataType.Date)]
        public DateTime booking_date { get; set; }
    }
    public partial class SP_event_resources
    {
        public int resource_id { get; set; }

        public string resource_name { get; set; }

        public bool resource_availability { get; set; }

        public int event_id { get; set; }

        public string event_name { get; set; }

        [DataType(DataType.Date)]
        public DateTime event_date { get; set; }
    }
}