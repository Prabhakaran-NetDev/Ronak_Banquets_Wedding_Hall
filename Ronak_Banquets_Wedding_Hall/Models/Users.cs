using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Ronak_Banquets_Wedding_Hall.Models
{
    public partial class tbl_users
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "User Name")]
        public string user_name { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "User Password")]
        public string user_password { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "User Email")]
        public string user_email { get; set; }

        [Display(Name = "User Phone")]
        public long? user_phone { get; set; }

        [StringLength(30)]
        [Display(Name = "User Role")]
        public string user_role { get; set; }

        [Required]
        [Compare("user_password", ErrorMessage = "Passwords Mismatched.")]
        [Display(Name = "Confirm Password")]
        public string confirm_password { get; set; }

        [Required]
        [Display(Name = "Old Password")]
        public string old_password { get; set; }

        [Required]
        public int? OTP { get; set; }
    }
}
