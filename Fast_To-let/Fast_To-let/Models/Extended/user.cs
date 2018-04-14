using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fast_To_let.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class user
    {
        public string Confirm_password { get; set; }
    }
    public class UserMetadata
    {
        [Display(Name = "username")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        public string username { get; set; }

        [Display(Name = "email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Display(Name = "phone")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        public string phone { get; set; }

        [Display(Name = "gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender is required")]

        public string gender { get; set; }

        [Display(Name = "password")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 charecters is required")]
        public string password { get; set; }

        [Display(Name = "Confirmpassword")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Con_password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 charecters is required ")]
        [Compare("password", ErrorMessage = "Password does not match")]


        public string Confirm_password { get; set; }



    }
}