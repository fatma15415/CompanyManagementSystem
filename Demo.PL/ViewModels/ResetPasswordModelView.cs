using System.ComponentModel.DataAnnotations;

namespace Demo.PL.ViewModels
{
    public class ResetPasswordModelView
    {
        [Required(ErrorMessage = "Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required")]
        [Compare(nameof(NewPassword), ErrorMessage = "ConfirmPassword does not password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }



    }
}
