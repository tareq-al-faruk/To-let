using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fast_To_let.Models
{
    public class adminlogin
    {
        [Display(Name = "username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Display(Name = "password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]

        public string password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}